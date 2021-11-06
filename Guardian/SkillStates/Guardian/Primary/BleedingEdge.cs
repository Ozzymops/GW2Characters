using GuardianPlugin.SkillStates.BaseStates;
using GuardianPlugin.SkillStates.Primary;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace GuardianPlugin.SkillStates.Primary
{
    // a.k.a. Axe Chain 2
    public class BleedingEdge : BaseMeleeAttack
    {
        private bool hitEnemy;

        public override void OnEnter()
        {
            this.hitboxName = "Mace";

            this.damageType = DamageType.Generic;
            this.damageCoefficient = Modules.StaticValues.corePrimaryChain1Damage / 100f;
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

            this.layerString = "Gesture, Override";
            this.animString = "Mace1";
            this.playbackRateString = "macePlaybackRate";
            this.crossfadeDuration = 0.05f;

            hitEnemy = false;

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

            if (!hitEnemy)
            {
                characterBody.GetComponent<Guardian.Modules.Guardian.AttackChainController>().ProgressChain();
                hitEnemy = true;
            }
        }

        protected override void SetNextState()
        {
            int index = this.swingIndex;
            if (index == 0) index = 1;
            else index = 0;

            switch (characterBody.GetComponent<Guardian.Modules.Guardian.AttackChainController>().chainCount)
            {
                case 0:
                    this.outer.SetNextState(new TrueStrike
                    {
                        swingIndex = index
                    });
                    break;

                case 1:
                    this.outer.SetNextState(new PureStrike
                    {
                        swingIndex = index
                    });
                    break;

                case 2:
                    this.outer.SetNextState(new FaithfulStrike
                    {
                        swingIndex = index
                    });
                    break;

                default:
                    this.outer.SetNextState(new TrueStrike
                    {
                        swingIndex = index
                    });
                    break;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}