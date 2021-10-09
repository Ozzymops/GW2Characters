using GuardianPlugin;
using GuardianPlugin.SkillStates;
using EntityStates;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    // parts of code were liberated from Daredevil
    public class AttackChainController : MonoBehaviour
    {
        public int chainCount = 0;
        private int maxChainCount = 2;
        private float chainTimer = 3f;

        private Sprite[] chainSprites;
        private SkillDef[] chainSkills;

        private void Awake()
        {
            chainSprites = new Sprite[] { GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryCore1"),
                                          GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryCore2"),
                                          GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texPrimaryCore3")};

            chainSkills = SetupSkills();

            ResetChain();
            UpdateAttack();
        }

        private SkillDef[] SetupSkills()
        {
            SkillDef mace1 = ScriptableObject.CreateInstance<SkillDef>();
            mace1 = GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace1)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_CHAIN1", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_CHAIN1", chainSprites[0], true);
            
            SkillDef mace2 = ScriptableObject.CreateInstance<SkillDef>();
            mace2 = GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace2)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_CHAIN2", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_CHAIN2", chainSprites[1], true);

            SkillDef mace3 = ScriptableObject.CreateInstance<SkillDef>();
            mace3 = GuardianPlugin.Modules.Skills.CreatePrimarySkillDef(new SerializableEntityStateType(typeof(Mace3)), "Weapon", "OZZ_GUARDIAN_BODY_PRIMARY_NAME_CHAIN3", "OZZ_GUARDIAN_BODY_PRIMARY_DESCRIPTION_CHAIN3", chainSprites[2], true);


            return new SkillDef[] { mace1, mace2, mace3 };
        }

        private void FixedUpdate()
        {
            ChainDecay();
        }

        private void ChainDecay()
        {
            if (chainCount > 0)
            {
                chainTimer -= Time.fixedDeltaTime;

                if (chainTimer <= 0)
                {
                    ResetChain();
                }
            }
        }

        public void ProgressChain()
        {
            int oldChain = chainCount;

            if (chainCount + 1 > maxChainCount)
            {
                ResetChain();
            }
            else
            {
                chainTimer = 3f;
                chainCount++;
            }

            UpdateAttack();
        }

        private void UpdateAttack()
        {
            SkillLocator skillLocator = this.GetComponent<CharacterBody>().skillLocator;
            SkillDef skillDef = skillLocator.primary.skillDef;

            switch (chainCount)
            {
                case 0:
                    skillLocator.primary.UnsetSkillOverride(skillLocator.primary, chainSkills[0], GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, chainSkills[0], GenericSkill.SkillOverridePriority.Contextual);
                    break;

                case 1:
                    skillLocator.primary.UnsetSkillOverride(skillLocator.primary, chainSkills[1], GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, chainSkills[1], GenericSkill.SkillOverridePriority.Contextual);
                    break;

                case 2:
                    skillLocator.primary.UnsetSkillOverride(skillLocator.primary, chainSkills[2], GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, chainSkills[2], GenericSkill.SkillOverridePriority.Contextual);
                    break;

                default:
                    skillLocator.primary.UnsetSkillOverride(skillLocator.primary, chainSkills[0], GenericSkill.SkillOverridePriority.Contextual);
                    skillLocator.primary.SetSkillOverride(skillLocator.primary, chainSkills[0], GenericSkill.SkillOverridePriority.Contextual);
                    break;
            }
        }

        private void ResetChain()
        {
            chainCount = 0;
            chainTimer = 3f;
            UpdateAttack();
        }
    }
}
