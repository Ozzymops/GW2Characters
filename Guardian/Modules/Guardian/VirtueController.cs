using BepInEx;
using GuardianPlugin;
using R2API;
using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    public class VirtueController : MonoBehaviour
    {
        private bool activateScript;

        private enum VirtueType { Core, Dragonhunter, Firebrand, Willbender };
        private VirtueType virtueType = VirtueType.Core;
        private CharacterBody characterBody;

        #region Shared
        private bool justice;
        private bool resolve;
        private bool courage;

        private float justiceTimer;
        private float justiceCooldown;
        private float resolveTimer;
        private float resolveCooldown;
        private float courageTimer;
        private float courageCooldown;
        #endregion

        #region Core Virtues
        private float core_areaRadius = 15f;

        private float core_justiceCooldown = 10f;
        private float core_hitsUntilJustice;
        private float core_hitsForJustice = 2;

        private float core_resolveCooldown = 20f;
        private float core_passiveResolveHealPercent = 2f;
        private float core_passiveResolveTimer;
        private float core_passiveResolveCooldown = 2f;
        private float core_activeResolveHealPercent = 15f;

        private float core_courageCooldown = 30f;
        private float core_passiveCourageTimer;
        private float core_passiveCourageCooldown = 10f;
        private int core_activeCourageStacksCurrent;
        private int core_activeCourageStacksMax = 4;
        #endregion

        private void Awake()
        {
            if (GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ"))
            {
                activateScript = true;
            }

            if (activateScript)
            {
                Debug.Log("GW2 VirtueController: Guardian player detected, enabling Virtues.");

                characterBody = GetComponent<CharacterBody>();

                // check Virtue type from Passive

                switch (virtueType)
                {
                    case VirtueType.Core:
                        justiceCooldown = core_justiceCooldown;
                        resolveCooldown = core_resolveCooldown;
                        courageCooldown = core_courageCooldown;

                        core_activeCourageStacksCurrent = core_activeCourageStacksMax;
                        break;

                    default:
                        // default to Core
                        justiceCooldown = core_justiceCooldown;
                        resolveCooldown = core_resolveCooldown;
                        courageCooldown = core_courageCooldown;

                        core_activeCourageStacksCurrent = core_activeCourageStacksMax;
                        break;
                }

                justice = true;
                resolve = true;
                courage = true;

                justiceTimer = justiceCooldown;
                resolveTimer = resolveCooldown;
                courageTimer = courageCooldown;

                core_hitsUntilJustice = 0;

                core_passiveResolveTimer = core_passiveResolveCooldown;
                core_passiveCourageTimer = core_passiveCourageCooldown;

                On.RoR2.HealthComponent.TakeDamage += AegisBuff;
                On.RoR2.HealthComponent.TakeDamage += JusticeBuff;
            }
        }

        private void OnDestroy()
        {
            On.RoR2.HealthComponent.TakeDamage -= AegisBuff;
            On.RoR2.HealthComponent.TakeDamage -= JusticeBuff;
        }

        private void Update()
        {
            if (activateScript)
            {
                #region Passive Effects
                if (justice)
                {
                    if (core_hitsUntilJustice > core_hitsForJustice)
                    {
                        Virtue_Justice_Core(1, false);
                        core_hitsUntilJustice = 0;
                    }
                }

                if (resolve)
                {
                    core_passiveResolveTimer -= 1f * Time.deltaTime;

                    if (core_passiveResolveTimer <= 0)
                    {
                        Virtue_Resolve_Core(core_passiveResolveHealPercent, false);
                        core_passiveResolveTimer = core_passiveResolveCooldown;
                    }
                }

                if (courage)
                {
                    core_passiveCourageTimer -= 1f * Time.deltaTime;

                    if (core_passiveCourageTimer <= 0)
                    {
                        Virtue_Courage_Core(false);
                        core_passiveCourageTimer = core_passiveCourageCooldown;
                    }
                }
                #endregion

                #region Cooldowns
                if (!justice)
                {
                    justiceTimer -= 1f * Time.deltaTime;

                    if (justiceTimer <= 0f)
                    {
                        switch (virtueType)
                        {
                            case VirtueType.Core:
                                core_hitsForJustice = 0;
                                break;

                            default:
                                break;
                        }

                        justice = true;
                        justiceTimer = justiceCooldown;
                    }
                }

                if (!resolve)
                {
                    resolveTimer -= 1f * Time.deltaTime;

                    if (resolveTimer <= 0f)
                    {
                        switch (virtueType)
                        {
                            case VirtueType.Core:
                                core_passiveResolveTimer = 0f;
                                break;

                            default:
                                break;
                        }

                        resolve = true;
                        resolveTimer = resolveCooldown;
                    }
                }

                if (!courage)
                {
                    courageTimer -= 1f * Time.deltaTime;

                    if (virtueType == VirtueType.Core)
                    {
                        if (core_activeCourageStacksCurrent > 0)
                        {
                            core_passiveCourageTimer -= 1f * Time.deltaTime;

                            if (core_passiveCourageTimer <= 0)
                            {
                                Virtue_Courage_Core(true);
                                core_passiveCourageTimer = .5f;
                                core_activeCourageStacksCurrent--;
                            }
                        }
                        else
                        {
                            courageTimer -= 1f * Time.deltaTime;
                        }
                    }
                    else
                    {
                        courageTimer -= 1f * Time.deltaTime;
                    }

                    if (courageTimer <= 0f)
                    {
                        switch (virtueType)
                        {
                            case VirtueType.Core:
                                core_passiveCourageTimer = 0f;
                                break;

                            default:
                                break;
                        }

                        courage = true;
                        courageTimer = courageCooldown;
                    }
                }
                #endregion

                #region Controls
                if (Input.GetKey(KeyCode.Alpha1) && justice)
                {
                    if (virtueType == VirtueType.Core)
                    {
                        // Play sound
                        // Play UI animation
                        Virtue_Justice_Core(5, true);
                    }

                    justice = false;
                }

                if (Input.GetKey(KeyCode.Alpha2) && resolve)
                {
                    if (virtueType == VirtueType.Core)
                    {
                        // Play sound
                        // Play UI animation
                        Virtue_Resolve_Core(core_activeResolveHealPercent, true);
                    }

                    resolve = false;
                }

                if (Input.GetKey(KeyCode.Alpha3) && courage)
                {
                    if (virtueType == VirtueType.Core)
                    {
                        // Play sound
                        // Play UI animation
                        core_passiveCourageTimer = 0f;
                        core_activeCourageStacksCurrent = core_activeCourageStacksMax;
                    }

                    courage = false;
                }
                #endregion
            }
        }

        #region Justice Functions
        public void Virtue_Justice_Core(int stacks, bool isArea)
        {
            if (isArea)
            {
                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = core_areaRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
                    .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                foreach (HurtBox hurtBox in hurtBoxes)
                {
                    if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex == TeamIndex.Player)
                    {
                        for (int i = 0; i < stacks; i++)
                        {
                            hurtBox.healthComponent.body.AddTimedBuff(GuardianPlugin.Modules.Buffs.justiceBuff, 10f, 5);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < stacks; i++)
                {
                    characterBody.AddTimedBuff(GuardianPlugin.Modules.Buffs.justiceBuff, 10f, 5);
                }
            }
        }

        public void Virtue_Justice_Core_Increment()
        {
            if (justice)
            {
                core_hitsUntilJustice++;
            }
        }

        public void Virtue_Justice_Core_Renew()
        {
            if (!justice)
            {
                justiceTimer = 0f;
            }
        }

        private void Virtue_Justice_DH()
        {
            // Spear of Justice
        }

        private void Virtue_Justice_DH_Pull()
        {
            // Hunter's Verdict
        }

        private void Virtue_Justice_FB()
        {
            // swap to Tome of Justice
        }

        private void Virtue_Justice_WB()
        {
            // Rushing Justice
        }
        #endregion

        #region Resolve Functions
        public void Virtue_Resolve_Core(float percent, bool isArea)
        {
            percent /= 100;

            if (isArea)
            {
                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = core_areaRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
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
                characterBody.healthComponent.HealFraction(percent, new ProcChainMask());
            }
        }

        private void Virtue_Resolve_DH()
        {
            // Wings of Resolve
        }

        private void Virtue_Resolve_FB()
        {
            // swap to Tome of Resolve
        }

        private void Virtue_Resolve_WB()
        {
            // Flowing Resolve
        }
        #endregion

        #region Courage Functions
        public void Virtue_Courage_Core(bool isArea)
        {
            if (SharedPluginWrapper.enabled)
            {
                if (isArea)
                {
                    List<HurtBox> hurtBoxes = new List<HurtBox>();
                    hurtBoxes = new SphereSearch { radius = core_areaRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
                        .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                    foreach (HurtBox hurtBox in hurtBoxes)
                    {
                        if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex == TeamIndex.Player)
                        {
                            hurtBox.healthComponent.body.AddTimedBuff(SharedPlugin.Modules.Buffs.aegisBuff, 10f, 5);
                        }
                    }
                }
                else
                {
                    characterBody.AddTimedBuff(SharedPlugin.Modules.Buffs.aegisBuff, 10f, 5);
                }
            }
        }

        private void Virtue_Courage_DH()
        {
            // Shield of Courage
        }

        private void Virtue_Courage_FB()
        {
            // swap to Tome of Courage
        }

        private void Virtue_Courage_WB()
        {
            // Crashing Courage
        }
        #endregion

        public bool[] GetActivatedVirtues()
        {
            return new bool[] { justice, resolve, courage };
        }

        public float[] GetCooldowns()
        {
            return new float[] { justiceTimer, justiceCooldown, resolveTimer, resolveCooldown, courageTimer, courageCooldown };
        }

        #region Hooks

        private void AegisBuff(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            if (self.body.baseNameToken.StartsWith("OZZ") && SharedPluginWrapper.enabled && self.body.GetComponent<TraitController>().GetTrait(0) &&
                self.body.HasBuff(SharedPlugin.Modules.Buffs.aegisBuff) && damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
            {
                DamageInfo shatteredAegisInfo = new DamageInfo();
                shatteredAegisInfo.damage = self.body.damage * GuardianPlugin.Modules.StaticValues.coreShatteredAegisCoefficient;
                shatteredAegisInfo.attacker = self.gameObject;
                shatteredAegisInfo.inflictor = self.gameObject;
                shatteredAegisInfo.force = Vector3.zero;
                shatteredAegisInfo.crit = false;
                shatteredAegisInfo.procCoefficient = 0f;
                shatteredAegisInfo.damageType = DamageType.BypassArmor;

                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = 5f, mask = LayerIndex.entityPrecise.mask, origin = self.transform.position }
                    .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                foreach (HurtBox hurtBox in hurtBoxes)
                {
                    if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex != TeamIndex.Player)
                    {
                        shatteredAegisInfo.position = hurtBox.transform.position;
                        GlobalEventManager.instance.OnHitEnemy(shatteredAegisInfo, hurtBox.gameObject);
                        GlobalEventManager.instance.OnHitAll(shatteredAegisInfo, hurtBox.gameObject);

                        hurtBox.healthComponent.TakeDamage(shatteredAegisInfo);
                    }
                }
            }

            orig(self, damageInfo);

            if (self.body.baseNameToken.StartsWith("OZZ") && SharedPluginWrapper.enabled && self.body.GetComponent<TraitController>().GetTrait(0) && self.body.GetComponent<TraitController>().GetConditionalTrait(0) && self.isHealthLow)
            {
                self.GetComponent<TraitController>().Core_AegisCooldown();
                self.GetComponent<VirtueController>().Virtue_Courage_Core(false);
            }
        }

        private void JusticeBuff(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(GuardianPlugin.Modules.Buffs.justiceBuff) && damageInfo.damageType != DamageType.DoT)
            {
                SharedPlugin.Modules.Buffs.HandleTimedBuffs(GuardianPlugin.Modules.Buffs.justiceBuff, damageInfo.attacker.GetComponent<CharacterBody>(), 5);

                damageInfo.damageType = DamageType.IgniteOnHit;

                if (damageInfo.attacker.GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ") && damageInfo.attacker.GetComponent<TraitController>().GetTrait(1))
                {
                    damageInfo.damage *= 1.15f;
                }
            }
            else if (damageInfo.attacker.GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ"))
            {
                if (damageInfo.damageType != DamageType.DoT)
                {
                    damageInfo.attacker.GetComponent<VirtueController>().Virtue_Justice_Core_Increment();
                }
            }

            orig(self, damageInfo);

            if (damageInfo.attacker.GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ") && damageInfo.attacker.GetComponent<TraitController>().GetTrait(1) &&
                damageInfo.attacker.GetComponent<TraitController>().GetConditionalTrait(1) && !damageInfo.attacker.GetComponent<VirtueController>().GetActivatedVirtues()[0] && !self.alive)
            {
                damageInfo.attacker.GetComponent<TraitController>().Core_JusticeCooldown();
                damageInfo.attacker.GetComponent<VirtueController>().Virtue_Justice_Core_Renew();
            }
        }

        #endregion
    }
}
