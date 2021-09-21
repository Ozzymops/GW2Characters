using GuardianPlugin.SkillStates.BaseStates;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace GuardianPlugin.SkillStates
{
    public class Mace3 : BaseMeleeAttack
    {
        private bool hitEnemy;
        private bool didHeal;
        private bool updateSkillDef = false;

        public override void FixedUpdate()
        {
            if (!updateSkillDef)
            {
                SkillDef skillDef = characterBody.skillLocator.primary.skillDef;
                skillDef.skillName = GuardianPlugin.developerPrefix + "_GUARDIAN_BODY_PRIMARY_MACE_THREE_NAME";
                skillDef.skillDescriptionToken = GuardianPlugin.developerPrefix + "_GUARDIAN_BODY_PRIMARY_MACE_THREE_DESCRIPTION";
                skillDef.icon = Modules.Assets.subAssetBundle.LoadAsset<Sprite>("skillMace3");
            }

            base.FixedUpdate();
        }

        public override void OnEnter()
        {
            this.hitboxName = "Sword";

            this.damageType = DamageType.Generic;
            this.damageCoefficient = Modules.StaticValues.maceDamageCoefficient * 1.55f;
            this.procCoefficient = 1f;
            this.pushForce = 300f;
            this.bonusForce = Vector3.zero;
            this.baseDuration = 1f;
            this.attackStartTime = 0.2f;
            this.attackEndTime = 0.4f;
            this.baseEarlyExitTime = 0.4f;
            this.hitStopDuration = 0.012f;
            this.attackRecoil = 0.5f;
            this.hitHopVelocity = 4f;

            this.swingSoundString = "HenrySwordSwing";
            this.hitSoundString = "";
            this.muzzleString = "SwingLeft";
            this.swingEffectPrefab = Modules.Assets.swordSwingEffect;
            this.hitEffectPrefab = Modules.Assets.swordHitImpactEffect;

            this.impactSound = Modules.Assets.swordHitSoundEvent.index;

            hitEnemy = false;
            didHeal = false;

            base.OnEnter();
        }

        protected override void PlayAttackAnimation()
        {
            base.PlayAttackAnimation();
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();

            hitEnemy = true;

            if (NetworkServer.active && !didHeal)
            {
                didHeal = true;
                characterBody.healthComponent.Heal(characterBody.maxHealth * 0.05f, new ProcChainMask(), false);
            }
        }

        protected override void SetNextState()
        {
            if (hitEnemy)
            {
                this.outer.SetNextState(new Mace1 { });
            }
            else
            {
                this.outer.SetNextState(new Mace3 { });
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}