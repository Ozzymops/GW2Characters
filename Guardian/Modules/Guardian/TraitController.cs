using GuardianPlugin;
using RoR2;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    public class TraitController : MonoBehaviour
    {
        private CharacterBody characterBody;
        private VirtueController virtueController;

        private bool adeptTrait;
        private bool masterTrait;
        private bool grandmasterTrait;

        private int adeptLevel = 5;
        private int masterLevel = 10;
        private int grandmasterLevel = 15;

        private void Awake()
        {
            characterBody = GetComponent<CharacterBody>();
            virtueController = GetComponent<VirtueController>();

            adeptTrait = false;
            masterTrait = false;
            grandmasterTrait = false;

            On.RoR2.CharacterBody.OnLevelUp += HandleTraits;
        }

        private void OnDestroy()
        {
            On.RoR2.CharacterBody.OnLevelUp -= HandleTraits;
        }

        private void HandleTraits(On.RoR2.CharacterBody.orig_OnLevelUp orig, CharacterBody self)
        {
            orig.Invoke(self);

            if (!adeptTrait && self.level >= adeptLevel)
            {
                adeptTrait = true;
            }

            if (!masterTrait && self.level >= masterLevel)
            {
                masterTrait = true;
            }

            if (!grandmasterTrait && self.level >= grandmasterLevel)
            {
                grandmasterTrait = true;
                virtueController.ReduceCooldownsAndIntervals();
            }           
        }

        #region Core
        private void Adept_ShatteredAegis()
        {

        }
        #endregion

        /// <summary>
        /// Get current enabled traits
        /// </summary>
        /// <returns>adept trait, master trait, grandmaster trait</returns>
        public bool[] GetTraits()
        {
            return new bool[] { adeptTrait, masterTrait, grandmasterTrait };
        }
    }
}
