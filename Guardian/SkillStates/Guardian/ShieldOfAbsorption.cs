using EntityStates;
using R2API.Utils;
using RoR2;
using RoR2.Projectile;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace GuardianPlugin.SkillStates
{
    public class ShieldOfAbsorption : BaseSkillState
    {
        public static float duration = 5f;
        public static int maxPulses = 5;

        public static string shieldSoundString = "Play_engi_shift_start";
        public static GameObject shieldPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/EngiBubbleShield");

        private GameObject shieldObject;
        private int shieldRadius;
        private int pulses;
        private float pulseTimer;

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

                shieldObject = Guardian.Modules.SkillHelpers.ShieldBubble.CreateShieldBubble();
                shieldObject.GetComponent<Guardian.Modules.SkillHelpers.ShieldBubble>().SetupVariables(shieldRadius);
                shieldObject.transform.SetParent(gameObject.transform);
                shieldObject.transform.position = base.characterBody.corePosition;

                if (NetworkServer.active)
                {
                    base.characterBody.AddTimedBuff(Modules.Buffs.shieldBuff, duration);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.isAuthority)
            {
                if (base.fixedAge >= ShieldOfAbsorption.duration)
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
                                hurtBox.healthComponent.body.AddTimedBuff(Modules.Buffs.aegisBuff, 9.9f, 5);
                            }
                        }
                    }
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            shieldObject.GetComponent<Guardian.Modules.SkillHelpers.ShieldBubble>().SetState(2);

            base.characterBody.ClearTimedBuffs(Modules.Buffs.shieldBuff);
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}