using GuardianPlugin;
using RoR2;
using GuardianPlugin.Modules;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    public class TraitController : MonoBehaviour
    {
        private bool setup = false;
        private int eliteSpecialisation = 0;
        private CharacterBody characterBody;
        private MechanicController mechanicController;

        private bool adeptTrait;
        private bool masterTrait;
        private bool grandmasterTrait;

        private bool adeptTraitAvailable;
        private bool masterTraitAvailable;
        private bool grandmasterTraitAvailable;

        private float adeptTraitCooldownTimer;
        private float masterTraitCooldownTimer;
        private float grandmasterTraitCooldownTimer;

        public void Setup(int es, CharacterBody cb, MechanicController mc)
        {
            if (!setup)
            {
                setup = true;

                eliteSpecialisation = es;
                characterBody = cb;
                mechanicController = mc;

                On.RoR2.CharacterBody.OnLevelUp += LevelUp;
            }
        }

        private void OnDestroy()
        {
            On.RoR2.CharacterBody.OnLevelUp -= LevelUp;
        }

        private void LevelUp(On.RoR2.CharacterBody.orig_OnLevelUp orig, CharacterBody self)
        {
            orig(self);

            if (!adeptTrait && self.level >= 5)
            {
                adeptTrait = true;
                adeptTraitAvailable = true;
            }

            if (!masterTrait && self.level >= 10)
            {
                masterTrait = true;
                masterTraitAvailable = true;
            }

            if (!grandmasterTrait && self.level >= 15)
            {
                grandmasterTrait = true;
                grandmasterTraitAvailable = true;
            }

            mechanicController.ResetCooldowns();
        }

        private void Update()
        {
            if (!adeptTraitAvailable)
            {
                adeptTraitCooldownTimer -= 1f * Time.deltaTime;

                if (adeptTraitCooldownTimer <= 0)
                {
                    adeptTraitAvailable = true;
                    adeptTraitCooldownTimer = 99f;
                }
            }

            if (!masterTraitAvailable)
            {
                masterTraitCooldownTimer -= 1f * Time.deltaTime;

                if (masterTraitCooldownTimer <= 0)
                {
                    masterTraitAvailable = true;
                    masterTraitCooldownTimer = 99f;
                }
            }

            if (!grandmasterTraitAvailable)
            {
                grandmasterTraitCooldownTimer -= 1f * Time.deltaTime;

                if (grandmasterTraitCooldownTimer <= 0)
                {
                    grandmasterTraitAvailable = true;
                    grandmasterTraitCooldownTimer = 99f;
                }
            }
        }

        public void SetTraitUnavailable(int trait, float cooldown)
        {
            switch (trait)
            {
                case 0:
                    adeptTraitAvailable = false;
                    adeptTraitCooldownTimer = cooldown;
                    break;

                case 1:
                    masterTraitAvailable = false;
                    masterTraitCooldownTimer = cooldown;
                    break;

                case 2:
                    grandmasterTraitAvailable = false;
                    grandmasterTraitCooldownTimer = cooldown;
                    break;
            }
        }  

        public bool[] ReturnTraits()
        {
            return new bool[] { adeptTrait, masterTrait, grandmasterTrait };
        }

        public bool[] ReturnTraitsAvailability()
        {
            return new bool[] { adeptTraitAvailable, masterTraitAvailable, grandmasterTraitAvailable };
        }

        public float[] ReturnCooldowns()
        {
            return new float[] { adeptTraitCooldownTimer, masterTraitCooldownTimer, grandmasterTraitCooldownTimer };
        }

        #region Core Traits

        public void LowHealthAegis()
        {
            if (adeptTrait && adeptTraitAvailable)
            {
                mechanicController.GrantCourageBuff();
                SetTraitUnavailable(0, StaticValues.traitAegisCooldown);
            }      
        }

        public void RenewedJustice()
        {
            if (masterTrait && masterTraitAvailable && mechanicController.ReturnActivatedVirtues()[0])
            {
                mechanicController.ResetCooldown(0);
                SetTraitUnavailable(1, StaticValues.traitRenewedJusticeCooldown);
            }
        }

        #endregion
    }
}
