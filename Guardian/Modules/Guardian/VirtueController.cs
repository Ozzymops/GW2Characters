using GuardianPlugin;
using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    public class VirtueController : MonoBehaviour
    {
        private bool scriptEnabled;

        private CharacterBody characterBody;
        private HealthComponent healthComponent;
        private TraitController traitController;

        private float virtueRadius = 15f;

        private int hitsForJustice;
        private int hitsUntilJustice = 2;
        private bool justiceActive;
        private float justiceCooldown = 15f;
        private float justiceCooldownTimer;

        private float passiveResolveInterval = 2f;
        private float passiveResolveIntervalTimer;
        private float passiveResolveHeal = 2.5f;
        private float activeResolveHeal = 15f;
        private bool resolveActive;
        private float resolveCooldown = 20f;
        private float resolveCooldownTimer;

        private float passiveCourageInterval = 10f;
        private float passiveCourageIntervalTimer;
        private int activeCourageCount;
        private int activeCourageCountMax = 4;
        private bool courageActive;
        private float courageCooldown = 30f;
        private float courageCooldownTimer;

        private void Awake()
        {
            characterBody = GetComponent<CharacterBody>();
            healthComponent = GetComponent<HealthComponent>();

            if (characterBody.baseNameToken.Contains("GUARDIAN"))
            {
                hitsForJustice = 0;
                passiveResolveIntervalTimer = passiveResolveInterval;
                passiveCourageIntervalTimer = passiveCourageInterval;
                activeCourageCount = activeCourageCountMax;

                justiceCooldownTimer = justiceCooldown;
                resolveCooldownTimer = resolveCooldown;
                courageCooldownTimer = courageCooldown;

                scriptEnabled = true;

                On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
            }
        }

        private void OnDestroy()
        {
            On.RoR2.HealthComponent.TakeDamage -= HealthComponent_TakeDamage;
        }

        private void Update()
        {
            if (scriptEnabled)
            {
                Controls();
                Cooldowns();
                PassiveEffects();
            }
        }

        private void Controls()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !justiceActive)
            {
                Util.PlaySound("PlayJusticeActivation", characterBody.gameObject);
                ApplyJustice(5, true);
                justiceActive = true;

                Debug.Log("[Active Justice]: activated.");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && !resolveActive)
            {
                Util.PlaySound("PlayResolveActivation", characterBody.gameObject);
                ApplyResolve(activeResolveHeal, true);
                resolveActive = true;

                Debug.Log("[Active Resolve]: activated.");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && !courageActive)
            {
                courageActive = true;
                passiveCourageIntervalTimer = 0f;
                Debug.Log("[Active Courage]: activated.");
            }
        }

        private void Cooldowns()
        {
            if (justiceActive)
            {
                justiceCooldownTimer -= 1f * Time.deltaTime;

                if (justiceCooldownTimer <= 0)
                {
                    hitsForJustice = 0;
                    justiceActive = false;
                    justiceCooldownTimer = justiceCooldown;
                }
            }

            if (resolveActive)
            {
                resolveCooldownTimer -= 1f * Time.deltaTime;

                if (resolveCooldownTimer <= 0)
                {
                    passiveResolveIntervalTimer = passiveResolveInterval;
                    resolveActive = false;
                    resolveCooldownTimer = resolveCooldown;
                }
            }

            if (courageActive)
            {
                courageCooldownTimer -= 1f * Time.deltaTime;

                if (courageCooldownTimer <= 0)
                {
                    passiveCourageIntervalTimer = passiveCourageInterval;
                    activeCourageCount = activeCourageCountMax;
                    courageActive = false;
                    courageCooldownTimer = courageCooldown;
                }
            }
        }

        private void PassiveEffects()
        {
            if (!justiceActive)
            {
                if (hitsForJustice + 1 > hitsUntilJustice)
                {
                    ApplyJustice(1, false);
                    hitsForJustice = 0;
                }
            }

            if (!resolveActive)
            {
                passiveResolveIntervalTimer -= 1f * Time.deltaTime;

                if (passiveResolveIntervalTimer <= 0)
                {
                    ApplyResolve(passiveResolveHeal, false);
                    passiveResolveIntervalTimer = passiveResolveInterval;
                }
            }

            if (courageActive)
            {
                passiveCourageIntervalTimer -= 1f * Time.deltaTime;

                if (passiveCourageIntervalTimer <= 0 && activeCourageCount > 0)
                {
                    ApplyCourage(false);
                    passiveCourageIntervalTimer = 1f;
                    activeCourageCount--;

                    Debug.Log("[Active Courage]: procced.");
                }
            }
            else
            {
                passiveCourageIntervalTimer -= 1f * Time.deltaTime;

                if (passiveCourageIntervalTimer <= 0)
                {
                    ApplyCourage(false);
                    passiveCourageIntervalTimer = passiveCourageInterval;

                    Debug.Log("[Passive Courage]: procced.");
                }
            }
        }

        private void ApplyJustice(int stacks, bool aoe)
        {
            if (aoe)
            {
                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = virtueRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
                    .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                foreach (HurtBox hurtBox in hurtBoxes)
                {
                    if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex == TeamIndex.Player)
                    {
                        for (int i = 0; i < stacks; i++)
                        {
                            hurtBox.healthComponent.body.AddTimedBuff(GuardianPlugin.Modules.Buffs.justiceBuff, 9.9f, 5);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < stacks; i++)
                {
                    characterBody.AddTimedBuff(GuardianPlugin.Modules.Buffs.justiceBuff, 9.9f, 5);
                }
            }
        }

        private void ApplyResolve(float percent, bool aoe)
        {
            percent /= 100;      

            if (aoe)
            {
                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = virtueRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
                    .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                foreach (HurtBox hurtBox in hurtBoxes)
                {
                    if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex == TeamIndex.Player)
                    {
                        hurtBox.healthComponent.HealFraction(percent, new ProcChainMask());
                    }
                }
            }
            else
            {
                healthComponent.HealFraction(percent, new ProcChainMask());
            }
        }

        private void ApplyCourage(bool aoe)
        {          
            if (aoe)
            {
                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = virtueRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
                    .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                foreach (HurtBox hurtBox in hurtBoxes)
                {
                    if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex == TeamIndex.Player)
                    {
                        hurtBox.healthComponent.body.AddTimedBuff(GuardianPlugin.Modules.Buffs.aegisBuff, 9.9f, 5);
                    }
                }
            }      
            else
            {
                Util.PlaySound("PlayCourageActivation", characterBody.gameObject);
                characterBody.AddTimedBuff(GuardianPlugin.Modules.Buffs.aegisBuff, 9.9f, 5);
            }
        }

        public void IncrementJustice()
        {
            if (!justiceActive)
            {
                hitsForJustice++;
            }
        }

        public void RenewJustice()
        {
            if (justiceActive)
            {
                justiceCooldownTimer = 0f;
            }
        }

        public void ReduceCooldownsAndIntervals()
        {
            justiceCooldown *= 0.66f;
            resolveCooldown *= 0.66f;
            courageCooldown *= 0.66f;

            passiveResolveInterval *= 0.66f;
            passiveCourageInterval *= 0.66f;

            justiceCooldownTimer = 0f;
            resolveCooldownTimer = 0f;
            courageCooldownTimer = 0f;
        }

        /// <summary>
        /// Get VirtueController active virtues
        /// </summary>
        /// <returns>justice, resolve, courage</returns>
        public bool[] GetBools()
        {
            return new bool[] { justiceActive, resolveActive, courageActive };
        }

        /// <summary>
        /// Get current and maximum VirtueController cooldowns
        /// </summary>
        /// <returns>justice timer, justice cooldown, resolve timer, resolve cooldown, courage timer, courage cooldown</returns>
        public float[] GetCooldowns()
        {
            return new float[] { justiceCooldownTimer, justiceCooldown, resolveCooldownTimer, resolveCooldown, courageCooldownTimer, courageCooldown };
        }

        public void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            // Aegis - block all damage except DoT and Void Reaver explosion
            if (self.body.HasBuff(GuardianPlugin.Modules.Buffs.aegisBuff) && damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
            {
                // Shattered Aegis

                // Clear and reapply stacks
                ClearAndReapplyTimedBuffs(GuardianPlugin.Modules.Buffs.aegisBuff, self.body, 5);

                // Visual effect
                EffectData effectData = new EffectData { origin = damageInfo.position, rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero ? damageInfo.force : Random.onUnitSphere)) };
                EffectManager.SpawnEffect(Resources.Load<GameObject>("prefabs/effects/BearProc"), effectData, true);

                // Block
                Util.PlaySound("PlayCourageBlock", self.gameObject);
                damageInfo.rejected = true;
            }

            // Justice - deal 15% increased damage and inflict burning
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(GuardianPlugin.Modules.Buffs.justiceBuff) && damageInfo.damageType != DamageType.DoT)
            {
                // Clear and reapply stacks
                ClearAndReapplyTimedBuffs(GuardianPlugin.Modules.Buffs.justiceBuff, damageInfo.attacker.GetComponent<CharacterBody>(), 5);

                // Visual Effect

                // Extra damage & burning
                damageInfo.damage *= 1.15f;
                damageInfo.damageType = DamageType.IgniteOnHit;
            }

            if (damageInfo.attacker.GetComponent<CharacterBody>().baseNameToken.Contains("GUARDIAN"))
            {
                // Justice stacks
                if (damageInfo.damageType != DamageType.DoT)
                {
                    damageInfo.attacker.GetComponent<VirtueController>().IncrementJustice();
                }

                // Renewed Justice
                if (!self.alive && damageInfo.attacker.GetComponent<TraitController>().GetTraits()[1])
                {
                    damageInfo.attacker.GetComponent<VirtueController>().RenewJustice();
                }
            }

            // Original code
            orig(self, damageInfo);
        }

        /// <summary>
        /// Remove and reapply all stacks minus one of a certain timed buff
        /// </summary>
        /// <param name="buffDef"></param>
        /// <param name="body"></param>
        /// <param name="maxStacks"></param>
        private void ClearAndReapplyTimedBuffs(BuffDef buffDef, CharacterBody body, int maxStacks)
        {
            int buffCount = 0;
            float buffTimer = 0f;

            foreach (CharacterBody.TimedBuff buff in body.timedBuffs)
            {
                if (buff.buffIndex == buffDef.buffIndex)
                {
                    if (buffTimer > buff.timer || buffTimer == 0f)
                    {
                        buffTimer = buff.timer;
                    }

                    buffCount++;
                }
            }

            body.ClearTimedBuffs(buffDef);

            for (int i = 1; i < buffCount; i++)
            {
                body.AddTimedBuff(buffDef, buffTimer, maxStacks);
            }
        }
    }
}
