using EntityStates;
using Guardian.Modules.Guardian;
using R2API.Utils;
using RoR2;
using RoR2.Projectile;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace GuardianPlugin.SkillStates.Special
{
    public class MantraOfFlame : BaseSkillState
    {
        public static float duration = 5f;
        public static int maxPulses = 5;

        public static string shieldSoundString = "Play_engi_shift_start";
        public static GameObject shieldPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/EngiBubbleShield");

        private GameObject shieldObject;
        private int shieldRadius;
        private int pulses;
        private float pulseTimer;
        private bool playLoop = false;

        public override void OnEnter()
        {
            base.OnEnter();

            shieldRadius = 10;
            pulses = maxPulses;
            pulseTimer = 1f;

            if (base.isAuthority)
            {
                //shieldObject = GameObject.Instantiate(ShieldOfAbsorption.shieldPrefab, transform);
                //shieldObject.GetComponent<EntityStateMachine>().SetNextState(new EntityStates.Engi.EngiBubbleShield.Deployed());
                //shieldObject.GetComponent<Rigidbody>().useGravity = false;
                //shieldObject.GetComponent<Rigidbody>().isKinematic = true;

                //shieldObject = Guardian.Modules.SkillHelpers.ShieldBubble.CreateShieldBubble();
                //shieldObject.GetComponent<Guardian.Modules.SkillHelpers.ShieldBubble>().SetupVariables(shieldRadius);
                //shieldObject.transform.SetParent(gameObject.transform);
                //shieldObject.transform.position = base.characterBody.corePosition;

                if (NetworkServer.active)
                {
                    base.characterBody.AddTimedBuff(Modules.Buffs.guardianShieldOfAbsorptionBuff, duration);
                }
            }

            base.PlayAnimation("Gesture, Override", "ShieldOfAbsorptionIn", "soaPlaybackRate", 0.5f);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!playLoop)
            {
                playLoop = true;
                base.PlayAnimation("Gesture, Override", "ShieldOfAbsorptionLoop", "soaPlaybackRate", 0.5f);
            }

            if (base.isAuthority)
            {
                if (base.fixedAge >= Sanctuary.duration)
                {
                    this.outer.SetNextStateToMain();
                    return;
                }
            }

            pulseTimer -= 1f * Time.deltaTime;

            if (pulseTimer <= 0)
            {
                if (pulses > 0)
                {
                    pulses--;
                    pulseTimer = 1f;

                    if (NetworkServer.active)
                    {
                        List<HurtBox> hurtBoxes = new List<HurtBox>();
                        hurtBoxes = new SphereSearch { radius = 10f, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
                            .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                        foreach (HurtBox hurtBox in hurtBoxes)
                        {
                            if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex == TeamIndex.Player)
                            {
                                hurtBox.healthComponent.body.AddTimedBuff(SharedPlugin.Modules.Buffs.protectionBuff, 10f, 1);
                            }
                        }
                    }
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            base.PlayAnimation("Gesture, Override", "ShieldOfAbsorptionOut", "soaPlaybackRate", 0.5f);

            //shieldObject.GetComponent<Guardian.Modules.SkillHelpers.ShieldBubble>().SetState(2);

            base.characterBody.ClearTimedBuffs(Modules.Buffs.guardianShieldOfAbsorptionBuff);

            if (NetworkServer.active && base.characterBody.GetComponent<TraitController>().ReturnTraits()[2])
            {
                base.characterBody.healthComponent.HealFraction(0.025f, new ProcChainMask());
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}