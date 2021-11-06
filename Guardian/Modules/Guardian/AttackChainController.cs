using GuardianPlugin;
using GuardianPlugin.SkillStates.Primary;
using EntityStates;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    public class AttackChainController : MonoBehaviour
    {
        private bool setup = false;

        public int chainCount = 0;
        private int maxChainCount = 2;
        private float chainTimer = 3f;
        private bool overrideChain = false;

        private Sprite[] chainSprites = new Sprite[] { };
        private SkillDef[] chainSkills = new SkillDef[] { };

        public void Setup(int eliteSpecialisation)
        {
            if (!setup)
            {
                setup = true;

                // Dragonhunter is missing because it doesn't have any auto-attack chainable skills
                switch (eliteSpecialisation)
                {
                    case 0: // Core
                        chainSprites = new Sprite[] { GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryCore1"),
                                                      GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryCore2"),
                                                      GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryCore3")};

                        chainSkills = new SkillDef[] { GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace1)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_CHAIN1", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_CHAIN1", chainSprites[0], true),
                                                       GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace2)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_CHAIN2", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_CHAIN2", chainSprites[1], true),
                                                       GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace3)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_CHAIN3", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_CHAIN3", chainSprites[2], true)};
                        break;

                    case 2: // Firebrand
                        chainSprites = new Sprite[] { GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryFB1"),
                                                      GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryFB2"),
                                                      GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryFB3")};

                        chainSkills = new SkillDef[] { GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace1)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_CHAIN1", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_FB_CHAIN1", chainSprites[0], true),
                                                       GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace2)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_CHAIN2", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_FB_CHAIN2", chainSprites[1], true),
                                                       GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace3)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_FB_CHAIN3", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_FB_CHAIN3", chainSprites[2], true)};
                        break;

                    case 3: // Willbender
                        chainSprites = new Sprite[] { GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryWB1"),
                                                      GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryWB2"),
                                                      GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryWB3")};

                        chainSkills = new SkillDef[] { GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace1)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_WB_CHAIN1", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_WB_CHAIN1", chainSprites[0], true),
                                                       GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace2)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_WB_CHAIN2", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_WB_CHAIN2", chainSprites[1], true),
                                                       GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace3)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_WB_CHAIN3", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_WB_CHAIN3", chainSprites[2], true)};
                        break;

                    default:
                        break;
                }

                ResetChain();
            }
        }

        private void FixedUpdate()
        {
            if (!overrideChain)
            {
                if (chainCount > 0)
                {
                    chainTimer -= 1f * Time.deltaTime;

                    if (chainTimer <= 0)
                    {
                        ResetChain();
                    }
                }
            }
        }

        private void ResetChain()
        {
            chainCount = 0;
            chainTimer = 3f;
            UpdateSkill();
        }

        private void UpdateSkill()
        {
            SkillLocator skillLocator = GetComponent<CharacterBody>().skillLocator;
            SkillDef updatedSkill = skillLocator.primary.skillDef;

            skillLocator.primary.UnsetSkillOverride(skillLocator.primary, skillLocator.primary.skillDef, GenericSkill.SkillOverridePriority.Contextual);

            switch (chainCount)
            {
                case 0:
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, chainSkills[0], GenericSkill.SkillOverridePriority.Contextual);
                    break;

                case 1:
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, chainSkills[1], GenericSkill.SkillOverridePriority.Contextual);
                    break;

                case 2:
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, chainSkills[2], GenericSkill.SkillOverridePriority.Contextual);
                    break;

                default:
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, chainSkills[0], GenericSkill.SkillOverridePriority.Contextual);
                    break;
            }
        }

        public void ProgressChain()
        {
            if (!overrideChain)
            {
                int previousChainCount = chainCount;

                if (chainCount + 1 > maxChainCount)
                {
                    ResetChain();
                }
                else
                {
                    chainTimer = 3f;
                    chainCount++;
                }

                UpdateSkill();
            }          
        }

        public void OverrideChain(bool doOverride)
        {
            overrideChain = doOverride;

            if (!doOverride)
            {
                ResetChain();
            }
        }
    }
}
