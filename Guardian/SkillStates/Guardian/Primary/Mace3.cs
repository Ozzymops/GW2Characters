using GuardianPlugin.SkillStates.BaseStates;
using GuardianPlugin.SkillStates.Primary;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace GuardianPlugin.SkillStates.Primary
{
    public class Mace3 : BaseMeleeAttack
    {
        private bool hitEnemy;
        private bool didHeal;

        public override void OnEnter()
        {
            this.hitboxName = "Mace";

            this.damageType = DamageType.Generic;
            this.damageCoefficient = Modules.StaticValues.corePrimaryChain3Damage / 100f;
            this.procCoefficient = 1f;
            this.pushForce = 400f;
            this.bonusForce = Vector3.zero;
            this.baseDuration = 2f;
            this.attackStartTime = 0.2f;
            this.attackEndTime = 0.8f;
            this.baseEarlyExitTime = 0.8f;
            this.hitStopDuration = 0.024f;
            this.attackRecoil = 0.8f;
            this.hitHopVelocity = 4f;

            this.swingSoundString = "HenrySwordSwing";
            this.hitSoundString = "";
            this.muzzleString = "SwingLeft";
            this.swingEffectPrefab = Modules.Assets.swordSwingEffect;
            this.hitEffectPrefab = Modules.Assets.swordHitImpactEffect;
            this.impactSound = Modules.Assets.swordHitSoundEvent.index;

            this.layerString = "Gesture, Override";
            this.animString = "Mace3";
            this.playbackRateString = "macePlaybackRate";
            this.crossfadeDuration = 0.05f;

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

            if (!hitEnemy)
            {
                characterBody.GetComponent<Guardian.Modules.Guardian.AttackChainController>().ProgressChain();
                hitEnemy = true;
            }

            if (NetworkServer.active && !didHeal)
            {
                didHeal = true;
                characterBody.healthComponent.Heal(characterBody.maxHealth * (Modules.StaticValues.corePrimaryChain3Heal / 100f), new ProcChainMask(), true);
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
                    this.outer.SetNextState(new Mace1
                    {
                        swingIndex = index
                    });
                    break;

                case 1:
                    this.outer.SetNextState(new Mace2
                    {
                        swingIndex = index
                    });
                    break;

                case 2:
                    this.outer.SetNextState(new Mace3
                    {
                        swingIndex = index
                    });
                    break;

                default:
                    this.outer.SetNextState(new Mace1
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