using BepInEx;
using GuardianPlugin;
using R2API;
using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GuardianPlugin.Modules;

namespace Guardian.Modules.Guardian
{
    public class MechanicController : MonoBehaviour
    {
        private bool debugging = false;

        private bool setup = false;
        private int eliteSpecialisation = 0;
        private CharacterBody characterBody;
        private AttackChainController attackChainController;

        #region Shared
        private bool activatedJustice;
        private bool activatedResolve;
        private bool activatedCourage;

        private float justiceHits;
        private float resolveIntervalTimer;
        private float courageIntervalTimer;
        private float courageTicks;
        private bool courageTicking;
        
        private float justiceCooldownTimer;
        private float resolveCooldownTimer;
        private float courageCooldownTimer;
        #endregion

        #region Dragonhunter
        private bool spearTethered;
        private SkillDef[] projectedVirtues;
        #endregion

        #region Firebrand
        private bool usingTomeOfJustice;
        private bool usingTomeOfResolve;
        private bool usingTomeOfCourage;

        private SkillDef[] skillLoadout;
        private SkillDef[] tomeOfJustice;
        private SkillDef[] tomeOfResolve;
        private SkillDef[] tomeOfCourage;
        #endregion

        #region Willbender
        private SkillDef[] emanatingVirtues;
        #endregion

        public void Setup(int es, CharacterBody cb, AttackChainController ac)
        {
            if (!setup)
            {
                setup = true;

                eliteSpecialisation = es;
                characterBody = cb;
                attackChainController = ac;

                switch (eliteSpecialisation)
                {
                    case 0: // Core
                        SetupVirtues();
                        break;

                    case 1: // Dragonhunter
                        SetupProjectedVirtues();
                        break;

                    case 2: // Firebrand
                        SetupTomes();
                        break;

                    case 3: // Willbender
                        SetupEmanatingVirtues();
                        break;

                    default:
                        break;
                }
            }
        }

        private void SetupVirtues()
        {
            activatedJustice = false;
            activatedResolve = false;
            activatedCourage = false;
        }

        private void SetupProjectedVirtues()
        {
            spearTethered = false;

            SkillDef spearOfJusticeSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_DH_SPEAR",
                skillNameToken = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_DH_SPEAR",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PASSIVE_DESCRIPTION_DH_SPEAR",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeDH1"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Justice.SpearOfJustice)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef huntersVerdictSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_DH_PULL",
                skillNameToken = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_DH_PULL",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PASSIVE_DESCRIPTION_DH_PULL",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeDH2"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Justice.HuntersVerdict)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef wingsOfResolveSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_DH_WINGS",
                skillNameToken = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_DH_WINGS",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PASSIVE_DESCRIPTION_DH_WINGS",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texResolveDH"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Resolve.WingsOfResolve)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef shieldOfCourageSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_DH_SHIELD",
                skillNameToken = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_DH_SHIELD",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PASSIVE_DESCRIPTION_DH_SHIELD",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texCourageDH"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Courage.ShieldOfCourage)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            projectedVirtues = new SkillDef[] { spearOfJusticeSkillDef, huntersVerdictSkillDef, wingsOfResolveSkillDef, shieldOfCourageSkillDef };
        }

        private void SetupTomes()
        {
            usingTomeOfJustice = false;
            usingTomeOfResolve = false;
            usingTomeOfCourage = false;

            #region Tome of Justice
            SkillDef tomeJusticePrimarySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_JUSTICETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_JUSTICETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_FB_JUSTICETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeFB2"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Justice.SearingSpell)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeJusticeSecondarySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_SECONDARY_NAME_FB_JUSTICETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_SECONDARY_NAME_FB_JUSTICETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_SECONDARY_DESCRIPTION_FB_JUSTICETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeFB3"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Justice.IgnitingBurst)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeJusticeUtilitySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_UTILITY_NAME_FB_JUSTICETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_UTILITY_NAME_FB_JUSTICETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_UTILITY_DESCRIPTION_FB_JUSTICETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeFB4"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Justice.ScorchedAftermath)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeJusticeSpecialSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_SPECIAL_NAME_FB_JUSTICETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_SPECIAL_NAME_FB_JUSTICETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_SPECIAL_DESCRIPTION_FB_JUSTICETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeFB5"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Justice.AshesOfTheJust)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });
            #endregion

            #region Tome of Resolve
            SkillDef tomeResolvePrimarySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_RESOLVETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_RESOLVETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_FB_RESOLVETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texResolveFB2"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Resolve.DesertBloom)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeResolveSecondarySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_SECONDARY_NAME_FB_RESOLVETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_SECONDARY_NAME_FB_RESOLVETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_SECONDARY_DESCRIPTION_FB_RESOLVETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texResolveFB3"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Resolve.RadiantRecovery)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeResolveUtilitySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_UTILITY_NAME_FB_RESOLVETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_UTILITY_NAME_FB_RESOLVETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_UTILITY_DESCRIPTION_FB_RESOLVETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texResolveFB4"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Resolve.AzureSun)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeResolveSpecialSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_SPECIAL_NAME_FB_RESOLVETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_SPECIAL_NAME_FB_RESOLVETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_SPECIAL_DESCRIPTION_FB_RESOLVETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texResolveFB5"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Resolve.EternalOasis)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });
            #endregion

            #region Tome of Courage
            SkillDef tomeCouragePrimarySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_COURAGETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_COURAGETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_FB_COURAGETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texCourageFB2"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Courage.UnflinchingCharge)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeCourageSecondarySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_SECONDARY_NAME_FB_COURAGETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_SECONDARY_NAME_FB_COURAGETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_SECONDARY_DESCRIPTION_FB_COURAGETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texCourageFB3"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Courage.DaringChallenge)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeCourageUtilitySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_UTILITY_NAME_FB_COURAGETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_UTILITY_NAME_FB_COURAGETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_UTILITY_DESCRIPTION_FB_COURAGETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texCourageFB4"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Courage.ValiantBulwark)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef tomeCourageSpecialSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_SPECIAL_NAME_FB_COURAGETOME",
                skillNameToken = "OZZ_GUARDIAN_BODY_SPECIAL_NAME_FB_COURAGETOME",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_SPECIAL_DESCRIPTION_FB_COURAGETOME",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texCourageFB5"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Courage.UnbrokenLines)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });
            #endregion

            tomeOfJustice = new SkillDef[] { tomeJusticePrimarySkillDef, tomeJusticeSecondarySkillDef, tomeJusticeUtilitySkillDef, tomeJusticeSpecialSkillDef };
            tomeOfResolve = new SkillDef[] { tomeResolvePrimarySkillDef, tomeResolveSecondarySkillDef, tomeResolveUtilitySkillDef, tomeResolveSpecialSkillDef };
            tomeOfCourage = new SkillDef[] { tomeCouragePrimarySkillDef, tomeCourageSecondarySkillDef, tomeCourageUtilitySkillDef, tomeCourageSpecialSkillDef };
            
            SkillLocator skillLocator = this.GetComponent<CharacterBody>().skillLocator;
            skillLoadout = new SkillDef[] { skillLocator.primary.skillDef, skillLocator.secondary.skillDef, skillLocator.utility.skillDef, skillLocator.special.skillDef };
        }

        private void SetupEmanatingVirtues()
        {
            SkillDef rushingJusticeSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_WB_RUSH",
                skillNameToken = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_WB_RUSH",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PASSIVE_DESCRIPTION_WB_RUSH",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeWB"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Justice.RushingJustice)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef flowingResolveSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_WB_FLOW",
                skillNameToken = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_WB_FLOW",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PASSIVE_DESCRIPTION_WB_FLOW",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texResolveWB"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Resolve.FlowingResolve)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            SkillDef crashingCourageSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_WB_CRASH",
                skillNameToken = "OZZ_GUARDIAN_BODY_PASSIVE_NAME_WB_CRASH",
                skillDescriptionToken = "OZZ_GUARDIAN_BODY_PASSIVE_DESCRIPTION_WB_CRASH",
                skillIcon = Assets.subAssetBundle.LoadAsset<Sprite>("texCourageWB"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(GuardianPlugin.SkillStates.Virtue.Courage.CrashingCourage)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0
            });

            emanatingVirtues = new SkillDef[] { rushingJusticeSkillDef, flowingResolveSkillDef, crashingCourageSkillDef };
        }

        private void Update()
        {
            if (debugging)
            {
                Debug();
            }

            // Controls
            if (!activatedJustice && Input.GetKeyDown(KeyCode.Alpha1))
            {
                ActivateCoreJustice();
            }

            if (!activatedResolve && Input.GetKeyDown(KeyCode.Alpha2))
            {
                ActivateCoreResolve();
            }

            if (!activatedCourage && Input.GetKeyDown(KeyCode.Alpha3))
            {
                ActivateCoreCourage();
            }

            // Justice
            if (activatedJustice)
            {
                justiceCooldownTimer -= 1f * Time.deltaTime;

                if (justiceCooldownTimer <= 0)
                {
                    activatedJustice = false;

                    switch (eliteSpecialisation)
                    {
                        case 0:
                            justiceCooldownTimer = StaticValues.virtueCoreActivatedJusticeCooldown;
                            break;

                        case 1:
                            break;

                        case 2:
                            break;

                        case 3:
                            break;

                        default:
                            break;
                    }   
                }
            }

            // Resolve
            if (activatedResolve)
            {
                resolveCooldownTimer -= 1f * Time.deltaTime;

                if (resolveCooldownTimer <= 0f)
                {
                    activatedResolve = false;

                    switch (eliteSpecialisation)
                    {
                        case 0:
                            resolveCooldownTimer = StaticValues.virtueCoreActivatedResolveCooldown;
                            break;

                        case 1:
                            break;

                        case 2:
                            break;

                        case 3:
                            break;

                        default:
                            break;
                    }

                    resolveIntervalTimer = StaticValues.virtuePassiveResolveInterval;
                }
            }
            else
            {
                resolveIntervalTimer -= 1f * Time.deltaTime;

                if (resolveIntervalTimer <= 0f)
                {
                    GrantResolveHeal(StaticValues.virtuePassiveResolveHeal, false);
                    resolveIntervalTimer = StaticValues.virtuePassiveResolveInterval;
                }
            }

            // Courage
            if (activatedCourage)
            {
                if (courageTicking)
                {
                    courageIntervalTimer -= 1f * Time.deltaTime;

                    if (courageIntervalTimer <= 0)
                    {
                        courageTicks--;
                        GrantCourageBuff(true);
                        courageIntervalTimer = 0.5f;
                    }

                    if (courageTicks <= 0)
                    {
                        courageTicking = false;
                    }
                }
                else
                {
                    courageCooldownTimer -= 1f * Time.deltaTime;

                    if (courageCooldownTimer <= 0)
                    {
                        activatedCourage = false;

                        switch (eliteSpecialisation)
                        {
                            case 0:
                                courageCooldownTimer = StaticValues.virtueCoreActivatedCourageCooldown;
                                courageTicks = 4f;
                                break;

                            case 1:
                                break;

                            case 2:
                                break;

                            case 3:
                                break;

                            default:
                                break;
                        }

                        courageIntervalTimer = StaticValues.virtuePassiveCourageInterval;
                    }
                }            
            }
            else
            {
                courageIntervalTimer -= 1f * Time.deltaTime;

                if (courageIntervalTimer <= 0f)
                {
                    GrantCourageBuff(false);
                    courageIntervalTimer = StaticValues.virtuePassiveCourageInterval;
                }
            }
        }

        private void Debug()
        {
            if (activatedJustice)
            {
                if (!usingTomeOfJustice)
                {
                    attackChainController.OverrideChain(true);
                    usingTomeOfJustice = true;

                    SkillLocator skillLocator = this.GetComponent<CharacterBody>().skillLocator;

                    SkillDef skillDef = skillLocator.primary.skillDef;
                    skillLocator.primary.UnsetSkillOverride(skillLocator.primary, skillLocator.primary.skillDef, GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, tomeOfJustice[0], GenericSkill.SkillOverridePriority.Contextual);

                    skillDef = skillLocator.secondary.skillDef;
                    skillLocator.secondary.UnsetSkillOverride(skillLocator.secondary, skillLocator.secondary.skillDef, GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.secondary.SetSkillOverride(skillLocator.secondary, tomeOfJustice[1], GenericSkill.SkillOverridePriority.Contextual);

                    skillDef = skillLocator.utility.skillDef;
                    skillLocator.utility.UnsetSkillOverride(skillLocator.utility, skillLocator.utility.skillDef, GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.utility.SetSkillOverride(skillLocator.utility, tomeOfJustice[2], GenericSkill.SkillOverridePriority.Contextual);

                    skillDef = skillLocator.special.skillDef;
                    skillLocator.special.UnsetSkillOverride(skillLocator.special, skillLocator.special.skillDef, GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.special.SetSkillOverride(skillLocator.special, tomeOfJustice[3], GenericSkill.SkillOverridePriority.Contextual);
                }               
            }
            else
            {
                if (usingTomeOfJustice)
                {
                    attackChainController.OverrideChain(false);
                    usingTomeOfJustice = false;

                    SkillLocator skillLocator = this.GetComponent<CharacterBody>().skillLocator;

                    SkillDef skillDef = skillLocator.primary.skillDef;
                    skillLocator.primary.UnsetSkillOverride(skillLocator.primary, skillLocator.primary.skillDef, GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, skillLoadout[0], GenericSkill.SkillOverridePriority.Contextual);

                    skillDef = skillLocator.secondary.skillDef;
                    skillLocator.secondary.UnsetSkillOverride(skillLocator.secondary, skillLocator.secondary.skillDef, GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.secondary.SetSkillOverride(skillLocator.secondary, skillLoadout[1], GenericSkill.SkillOverridePriority.Contextual);

                    skillDef = skillLocator.utility.skillDef;
                    skillLocator.utility.UnsetSkillOverride(skillLocator.utility, skillLocator.utility.skillDef, GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.utility.SetSkillOverride(skillLocator.utility, skillLoadout[2], GenericSkill.SkillOverridePriority.Contextual);

                    skillDef = skillLocator.special.skillDef;
                    skillLocator.special.UnsetSkillOverride(skillLocator.special, skillLocator.special.skillDef, GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.special.SetSkillOverride(skillLocator.special, skillLoadout[3], GenericSkill.SkillOverridePriority.Contextual);
                }              
            }
        }

        #region Shared

        public void IncrementJusticeHits()
        {
            if (activatedJustice)
            {
                if (justiceHits + 1 >= StaticValues.virtuePassiveJusticeHitsNeeded)
                {
                    GrantJusticeBuff(1, false);
                    justiceHits = 0;
                }
                else
                {
                    justiceHits++;
                }
            }
        }

        private void GrantJusticeBuff(int amount, bool aoe)
        {
            if (aoe)
            {
                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = StaticValues.virtueRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
                    .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                foreach (HurtBox hurtBox in hurtBoxes)
                {
                    if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex == TeamIndex.Player)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            hurtBox.healthComponent.body.AddTimedBuff(GuardianPlugin.Modules.Buffs.guardianJusticeBuff, 10f, 5);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    characterBody.AddTimedBuff(GuardianPlugin.Modules.Buffs.guardianJusticeBuff, 10f, 5);
                }
            }
        }

        private void GrantResolveHeal(float amount, bool aoe)
        {
            amount /= 100;

            if (aoe)
            {
                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = StaticValues.virtueRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
                    .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                foreach (HurtBox hurtBox in hurtBoxes)
                {
                    if (hurtBox.healthComponent.combinedHealthFraction < 1 && hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex == TeamIndex.Player)
                    {
                        hurtBox.healthComponent.HealFraction(amount, new ProcChainMask());
                    }
                }
            }
            else
            {
                if (characterBody.healthComponent.combinedHealthFraction < 1)
                {
                    characterBody.healthComponent.HealFraction(amount, new ProcChainMask());
                }
            }
        }

        private void GrantCourageBuff(bool aoe)
        {
            if (SharedPluginWrapper.enabled)
            {
                if (aoe)
                {
                    List<HurtBox> hurtBoxes = new List<HurtBox>();
                    hurtBoxes = new SphereSearch { radius = StaticValues.virtueRadius, mask = LayerIndex.entityPrecise.mask, origin = transform.position }
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

        #endregion

        #region Virtues

        private void ActivateCoreJustice()
        {
            activatedJustice = true;

            for (int i = 0; i < 4; i++)
            {
                GrantJusticeBuff(5, true);
            }
        }

        private void ActivateCoreResolve()
        {
            activatedResolve = true;
            GrantResolveHeal(StaticValues.virtuePassiveResolveHeal * 3, true);
        }

        private void ActivateCoreCourage()
        {
            courageTicks = 4;
            courageTicking = true;
            courageIntervalTimer = 0f;

            activatedCourage = true;
        }

        #endregion

        public bool[] ReturnActivatedVirtues()
        {
            return new bool[] { activatedJustice, activatedResolve, activatedCourage };
        }

        public float[] ReturnCooldowns()
        {
            return new float[] { justiceCooldownTimer, resolveCooldownTimer, courageCooldownTimer };
        }

        public void ResetCooldown(int virtue)
        {
            switch (virtue)
            {
                case 0:
                    justiceCooldownTimer = 0.1f;
                    break;

                case 1:
                    resolveCooldownTimer = 0.1f;
                    break;

                case 2:
                    courageCooldownTimer = 0.1f;
                    break;
            }
        }

        public void ResetCooldowns()
        {
            justiceCooldownTimer = 0.1f;
            resolveCooldownTimer = 0.1f;
            courageCooldownTimer = 0.1f;
        }

        public void GrantCourageBuff()
        {
            characterBody.AddTimedBuff(SharedPlugin.Modules.Buffs.aegisBuff, 10f, 5);
        }
    }
}
