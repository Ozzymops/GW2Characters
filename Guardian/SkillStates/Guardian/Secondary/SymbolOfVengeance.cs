using EntityStates;
using Guardian.Modules.Guardian;
using RoR2;
using RoR2.Projectile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace GuardianPlugin.SkillStates.Secondary
{
    public class SymbolOfVengeance : BaseSkillState
    {
        public static float damageCoefficient = Modules.StaticValues.coreSecondaryDamage / 100f;
        public static float procCoefficient = 0.5f;
        public static float baseDuration = 1.0f;
        public static float force = 800f;
        public static float recoil = 1f;
        public static float range = 1256f;
        public static float bloom = 0.5f;
        public static int baseProjectileCount = 8;
        public static GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectiles/MageLightningboltBasic");

        private float duration;
        private float fireTime;
        private string muzzleString;
        private int projectileCount;
        private int firedProjectiles = 0;
        private float timer;
        private int animIndex = 0;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = ZealotsDefense.baseDuration;
            // this.duration = Shoot.baseDuration / this.attackSpeedStat;
            this.fireTime = 0.2f * this.duration;
            base.characterBody.SetAimTimer(1f);
            this.muzzleString = "Muzzle";
            this.projectileCount = ZealotsDefense.baseProjectileCount;
            this.timer = 0.2f;
        }

        public override void OnExit()
        {
            base.OnExit();

            if (NetworkServer.active && base.characterBody.GetComponent<TraitController>().ReturnTraits()[2])
            {
                base.characterBody.healthComponent.HealFraction(0.025f, new ProcChainMask());
            }
        }

        private void Fire()
        {
            if (animIndex == 0)
            {
                animIndex = 1;
                base.PlayAnimation("Gesture, Override", "ZealotsDefenseLeft", "zdPlaybackRate", 0.5f);
            }
            else
            {
                animIndex = 0;
                base.PlayAnimation("Gesture, Override", "ZealotsDefenseRight", "zdPlaybackRate", 0.5f);
            }

            EffectManager.SimpleMuzzleFlash(Resources.Load<GameObject>("Prefabs/Effects/Muzzleflashes/MuzzleflashMageLightning"), base.gameObject, this.muzzleString, false);
            Util.PlaySound(EntityStates.Mage.Weapon.FireLaserbolt.attackString, base.gameObject);

            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                base.characterBody.AddSpreadBloom(bloom);

                ProjectileManager.instance.FireProjectile(projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, damageStat * damageCoefficient, 0f, base.RollCrit());
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            timer -= 1.0f * Time.fixedDeltaTime;

            if (this.timer <= 0)
            {
                firedProjectiles++;

                this.Fire();
                this.timer = 0.2f;
            }

            // Repeat
            List<ProjectileController> projectiles = new List<ProjectileController>();
            new RoR2.SphereSearch { radius = 5f, mask = LayerIndex.projectile.mask, origin = gameObject.transform.position }.RefreshCandidates().FilterCandidatesByProjectileControllers().GetProjectileControllers(projectiles);

            if (projectiles.Count > 0)
            {
                foreach (ProjectileController projectile in projectiles)
                {
                    if (projectile.owner && projectile.owner.GetComponent<CharacterBody>())
                    {
                        if (projectile.owner.GetComponent<CharacterBody>().teamComponent.teamIndex == GetComponent<CharacterBody>().teamComponent.teamIndex)
                        {
                            continue;
                        }
                        else
                        {
                            EntityState.Destroy(projectile.gameObject);
                        }
                    }
                }
            }

            if (firedProjectiles >= projectileCount)
            {
                if (base.fixedAge >= this.duration && base.isAuthority)
                {
                    this.outer.SetNextStateToMain(); // !!! remember for mace !
                    return;
                }
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}