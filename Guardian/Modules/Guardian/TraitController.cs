using GuardianPlugin;
using RoR2;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    public class TraitController : MonoBehaviour
    {
        private CharacterBody characterBody;
        private VirtueController virtueController;
        private enum TraitType { Core, Dragonhunter, Firebrand, Willbender };
        private TraitType traitType = TraitType.Core;

        private bool adeptTrait = false;        // level  5
        private bool masterTrait = false;       // level 10
        private bool grandmasterTrait = false;  // level 15

        private int adeptLevel = 5;
        private int masterLevel = 10;
        private int grandmasterLevel = 15;

        #region Core Traits
        private bool core_lowHealthAegis;
        private float core_lowHealthAegisTimer;
        private float core_lowHealthAegisCooldown = 6f;

        private bool core_renewedJustice;
        private float core_renewedJusticeTimer;
        private float core_renewedJusticeCooldown = 5f;

        private float core_healFromAbilityPercent = 2.5f;
        #endregion

        private void Awake()
        {
            characterBody = GetComponent<CharacterBody>();

            if (characterBody.baseNameToken.StartsWith("OZZ"))
            {
                virtueController = GetComponent<VirtueController>();

                On.RoR2.CharacterBody.OnLevelUp += HandleTraitsOnLevelUp;
            }
        }

        private void Destroy()
        {
            On.RoR2.CharacterBody.OnLevelUp -= HandleTraitsOnLevelUp;
        }

        private void HandleTraitsOnLevelUp(On.RoR2.CharacterBody.orig_OnLevelUp orig, CharacterBody self)
        {
            orig.Invoke(self);

            if (!adeptTrait && self.level >= adeptLevel)
            {
                adeptTrait = true;

                if (traitType == TraitType.Core)
                {
                    core_lowHealthAegis = true;
                    core_lowHealthAegisTimer = core_lowHealthAegisCooldown;
                }            
            }

            if (!masterTrait && self.level >= masterLevel)
            {
                masterTrait = true;

                if (traitType == TraitType.Core)
                {
                    core_renewedJustice = true;
                    core_renewedJusticeTimer = core_renewedJusticeCooldown;
                }               
            }

            if (!grandmasterTrait && self.level >= grandmasterLevel)
            {
                grandmasterTrait = true;
            }
        }

        private void Update()
        {
            #region Core Trait Cooldowns
            if (traitType == TraitType.Core)
            {
                if (adeptTrait && !core_lowHealthAegis)
                {
                    core_lowHealthAegisTimer -= 1f * Time.deltaTime;

                    if (core_lowHealthAegisTimer <= 0f)
                    {
                        core_lowHealthAegis = true;
                        core_lowHealthAegisTimer = core_lowHealthAegisCooldown;
                    }
                }

                if (masterTrait && !core_renewedJustice)
                {
                    core_renewedJusticeTimer -= 1f * Time.deltaTime;

                    if (core_renewedJusticeTimer <= 0f)
                    {
                        core_renewedJustice = true;
                        core_renewedJusticeTimer = core_renewedJusticeCooldown;
                    }
                }
            }
            #endregion
        }

        public bool[] GetTraits()
        {
            return new bool[] { adeptTrait, masterTrait, grandmasterTrait };
        }

        public bool GetTrait(int trait)
        {
            switch(trait)
            {
                case 0:
                    return adeptTrait;

                case 1:
                    return masterTrait;

                case 2:
                    return grandmasterTrait;

                default:
                    return false;
            }
        }

        public bool[] GetConditionalTraits()
        {
            switch (traitType)
            {
                case TraitType.Core:
                    return new bool[] { core_lowHealthAegis, core_renewedJustice, false };

                default:
                    return new bool[] { false, false, false };
            }
        }

        public bool GetConditionalTrait(int trait)
        {
            switch (traitType)
            {
                case TraitType.Core:
                    switch(trait)
                    {
                        case 0:
                            return core_lowHealthAegis;

                        case 1:
                            return core_renewedJustice;

                        default:
                            return false;
                    }

                default:
                    return false;
            }
        }

        public float[] GetCooldowns()
        {
            switch(traitType)
            {
                case TraitType.Core:
                    return new float[] { core_lowHealthAegisTimer, core_lowHealthAegisCooldown, core_renewedJusticeTimer, core_renewedJusticeCooldown, 0f, 0f };

                default:
                    return new float[] { 0f, 0f, 0f, 0f, 0f, 0f };
            }
        }

        /// <returns>0: core, 1: dragonhunter, 2: firebrand, 3: willbender</returns>
        public int GetTraitType()
        {
            return (int)traitType;
        }

        #region Core Trait Functions
        public void Core_AegisCooldown()
        {
            core_lowHealthAegis = false;
        }

        public void Core_JusticeCooldown()
        {
            core_renewedJustice = false;
        }
        #endregion

        #region Dragonhunter Trait Functions

        #endregion

        #region Firebrand Trait Functions

        #endregion

        #region Willbender Trait Functions

        #endregion
    }
}
