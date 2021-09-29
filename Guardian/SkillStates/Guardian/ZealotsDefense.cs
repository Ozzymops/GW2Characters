using EntityStates;
using RoR2;
using RoR2.Projectile;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianPlugin.SkillStates
{
    public class ZealotsDefense : BaseSkillState
    {
        public static float damageCoefficient = Modules.StaticValues.zealotsDefenseDamageCoefficient;
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

            base.PlayAnimation("LeftArm, Override", "ShootGun", "ShootGun.playbackRate", 1.8f);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void Fire()
        {
            EffectManager.SimpleMuzzleFlash(Resources.Load<GameObject>("Prefabs/Effects/Muzzleflashes/MuzzleflashMageLightning"), base.gameObject, this.muzzleString, false);
            Util.PlaySound(EntityStates.Mage.Weapon.FireLaserbolt.attackString, base.gameObject);

            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                base.characterBody.AddSpreadBloom(bloom);

                ProjectileManager.instance.FireProjectile(projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, damageCoefficient * damageStat, 0f, base.RollCrit());
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