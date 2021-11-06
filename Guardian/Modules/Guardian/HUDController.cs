using RoR2;
using RoR2.UI;
using Guardian.Modules;
using GuardianPlugin.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Guardian.Modules.Guardian
{
    public class HUDController : MonoBehaviour
    {
        private bool setup = false;
        private int eliteSpecialisation = 0;
        private MechanicController mechanicController;
        private TraitController traitController;

        private HUD hud;
        private GameObject customHUD;
        private string[] prefabString = new string[] { "GuardianHUD_Core", "GuardianHUD_Dragonhunter", "GuardianHUD_Firebrand", "GuardianHUD_Willbender" };

        private Color opaqueColor = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);
        private Color transparentColor = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);

        #region Cooldowns
        private float justiceCd;
        private float resolveCd;
        private float courageCd;

        private float adeptTraitCd;
        private float masterTraitCd;
        private float grandmasterTraitCd;
        #endregion

        #region Images
        private Image justiceCdFill;
        private Image resolveCdFill;
        private Image courageCdFill;

        private Image adeptTrait;
        private Image masterTrait;
        private Image grandmasterTrait;

        private Image adeptTraitCdFill;
        private Image masterTraitCdFill;
        private Image grandmasterTraitCdFill;
        #endregion

        #region Texts
        private Text justiceCdTimer;
        private Text resolveCdTimer;
        private Text courageCdTimer;
        #endregion

        public void Setup(int es, MechanicController mc, TraitController tc)
        {
            if (!setup)
            {
                setup = true;
                mechanicController = mc;
                traitController = tc;
                hud = GetComponent<HUD>();

                On.RoR2.UI.HUD.Awake += CreateHUD;
                On.RoR2.UI.HUD.Update += UpdateHUD;
            }
        }

        private void OnDestroy()
        {
            On.RoR2.UI.HUD.Awake -= CreateHUD;
            On.RoR2.UI.HUD.Update -= UpdateHUD;
        }

        private void CreateHUD(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);
            hud = self;

            switch (eliteSpecialisation)
            {
                case 0: // Core
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabString[0]), hud.mainContainer.transform);
                    adeptTraitCdFill = customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
                    masterTraitCdFill = customHUD.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>();

                    justiceCd = StaticValues.virtueCoreActivatedJusticeCooldown;
                    resolveCd = StaticValues.virtueCoreActivatedResolveCooldown;
                    courageCd = StaticValues.virtueCoreActivatedCourageCooldown;
                    adeptTraitCd = StaticValues.traitAegisCooldown;
                    masterTraitCd = StaticValues.traitRenewedJusticeCooldown;
                    break;

                case 1: // Dragonhunter
                    break;

                case 2: // Firebrand
                    break;

                case 3: // Willbender
                    break;
            }

            justiceCdFill = customHUD.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
            resolveCdFill = customHUD.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>();
            courageCdFill = customHUD.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>();

            justiceCdTimer = customHUD.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>();
            resolveCdTimer = customHUD.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>();
            courageCdTimer = customHUD.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>();

            adeptTrait = customHUD.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            masterTrait = customHUD.transform.GetChild(1).GetChild(1).GetComponent<Image>();
            grandmasterTrait = customHUD.transform.GetChild(1).GetChild(2).GetComponent<Image>();

            adeptTrait.enabled = false;
            masterTrait.enabled = false;
            grandmasterTrait.enabled = false;

            if (adeptTraitCdFill)
            {
                adeptTraitCdFill.enabled = false;
            }

            if (masterTraitCdFill)
            {
                masterTraitCdFill.enabled = false;
            }

            if (grandmasterTraitCdFill)
            {
                grandmasterTraitCdFill.enabled = false;
            }
        }

        private void UpdateHUD(On.RoR2.UI.HUD.orig_Update orig, RoR2.UI.HUD self)
        {
            orig(self);

            bool[] mechanicBools = mechanicController.ReturnActivatedVirtues();
            bool[] traitBools = traitController.ReturnTraits();
            bool[] traitAvailableBools = traitController.ReturnTraitsAvailability();

            float[] mechanicCd = mechanicController.ReturnCooldowns();
            float[] traitCd= traitController.ReturnCooldowns();

            // Justice
            if (mechanicBools[0])
            {
                justiceCdFill.fillAmount = HandleFillAmount(mechanicCd[0], justiceCd);
                justiceCdTimer.text = HandleCooldownText(mechanicCd[0]);
                justiceCdTimer.color = opaqueColor;
            }
            else
            {
                justiceCdFill.fillAmount = 1f;
                justiceCdTimer.text = "";
                justiceCdTimer.color = transparentColor;
            }

            // Resolve
            if (mechanicBools[1])
            {
                resolveCdFill.fillAmount = HandleFillAmount(mechanicCd[1], resolveCd);
                resolveCdTimer.text = HandleCooldownText(mechanicCd[1]);
                resolveCdTimer.color = opaqueColor;
            }
            else
            {
                resolveCdFill.fillAmount = 1f;
                resolveCdTimer.text = "";
                resolveCdTimer.color = transparentColor;
            }

            // Courage
            if (mechanicBools[2])
            {
                courageCdFill.fillAmount = HandleFillAmount(mechanicCd[2], courageCd);
                courageCdTimer.text = HandleCooldownText(mechanicCd[2]);
                courageCdTimer.color = opaqueColor;
            }
            else
            {
                courageCdFill.fillAmount = 1f;
                courageCdTimer.text = "";
                courageCdTimer.color = transparentColor;
            }

            // Adept Trait
            if (traitBools[0])
            {
                adeptTrait.enabled = true;

                if (adeptTraitCdFill)
                {
                    adeptTraitCdFill.enabled = true;

                    if (!traitAvailableBools[0])
                    {
                        adeptTraitCdFill.fillAmount = HandleFillAmount(traitCd[0], adeptTraitCd);
                    }
                    else
                    {
                        adeptTraitCdFill.fillAmount = 1f;
                    }
                }
            }

            // Master Trait
            if (traitBools[1])
            {
                masterTrait.enabled = true;

                if (masterTraitCdFill)
                {
                    masterTraitCdFill.enabled = true;

                    if (!traitAvailableBools[1])
                    {
                        masterTraitCdFill.fillAmount = HandleFillAmount(traitCd[1], masterTraitCd);
                    }
                    else
                    {
                        masterTraitCdFill.fillAmount = 1f;
                    }
                }
            }

            // Grandmaster Trait
            if (traitBools[2])
            {
                grandmasterTrait.enabled = true;

                if (grandmasterTraitCdFill)
                {
                    grandmasterTraitCdFill.enabled = true;

                    if (!traitAvailableBools[2])
                    {
                        grandmasterTraitCdFill.fillAmount = HandleFillAmount(traitCd[2], grandmasterTraitCd);
                    }
                    else
                    {
                        grandmasterTraitCdFill.fillAmount = 1f;
                    }
                }
            }
        }

        private float HandleFillAmount(float timer, float cooldown)
        {
            return (1f - timer/cooldown);
        }

        private string HandleCooldownText(float timer)
        {
            if (timer <= 3.0f)
            {
                return timer.ToString("0.0");
            }
            else
            {
                return timer.ToString("0");
            }
        }
    }
}
