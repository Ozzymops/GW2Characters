using RoR2;
using RoR2.UI;
using Guardian.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Guardian.Modules.Guardian
{
    public class GuardianHUD : MonoBehaviour
    {
        private bool activateScript;

        private enum HUDType { Core, Dragonhunter, Firebrand, Willbender };
        private HUDType hudType = HUDType.Core;
        private string[] prefabName = new string[] { "GuardianHUD_Core", "GuardianHUD_Dragonhunter", "GuardianHUD_Firebrand", "GuardianHUD_Willbender" };

        private HUD hud;
        private VirtueController virtueController;
        private GameObject customHUD;

        #region Images/Texts
        private Color visibleTextColor = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);
        private Color transparentTextColor = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);

        private Image justiceFill;
        private Image resolveFill;
        private Image courageFill;
        private Text justiceText;
        private Text resolveText;
        private Text courageText;

        #region Special
        private Image dhFill;
        private Image fbFill;
        private Image wbFill;
        private Text dhText;
        private Text fbText;
        private Text wbText;
        #endregion
        
        #endregion

        private void Awake()
        {
            if (GetComponent<CharacterBody>().baseNameToken.Contains("GUARDIAN"))
            {
                activateScript = true;              
            }

            if (activateScript)
            {
                Debug.Log("GW2 GuardianHUD: Guardian player detected, enabling custom HUD.");

                hud = GetComponent<HUD>();
                virtueController = GetComponent<VirtueController>();

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

            switch (hudType)
            {
                case HUDType.Core:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[0]), hud.mainContainer.transform);
                    Debug.Log("GW2 GuardianHUD: Core HUD loaded.");
                    break;

                case HUDType.Dragonhunter:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[1]), hud.mainContainer.transform);
                    Debug.Log("GW2 GuardianHUD: Dragonhunter HUD loaded.");
                    break;

                case HUDType.Firebrand:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[2]), hud.mainContainer.transform);
                    Debug.Log("GW2 GuardianHUD: Firebrand HUD loaded.");
                    break;

                case HUDType.Willbender:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[3]), hud.mainContainer.transform);
                    Debug.Log("GW2 GuardianHUD: Willbender HUD loaded.");
                    break;

                default:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[0]), hud.mainContainer.transform);
                    Debug.Log("GW2 GuardianHUD: HUDType was not set correctly, defaulting to Core HUD.");
                    break;
            }

            justiceFill = customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
            resolveFill = customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
            courageFill = customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
            justiceText = customHUD.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            resolveText = customHUD.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>();
            courageText = customHUD.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>();
        }

        private void UpdateHUD(On.RoR2.UI.HUD.orig_Update orig, RoR2.UI.HUD self)
        {
            orig(self);

            bool[] virtuesActivated = virtueController.GetBools();
            float[] virtueCooldowns = virtueController.GetCooldowns();

            #region General

            #region Justice
            if (virtuesActivated[0])
            {
                justiceText.color = visibleTextColor;
                justiceText.text = HandleCooldownText(virtueCooldowns[0]);
                justiceFill.fillAmount = HandleFillAmount(virtueCooldowns[0], virtueCooldowns[1]);
            }
            else
            {
                justiceText.color = transparentTextColor;
                justiceText.text = "0";
                justiceFill.fillAmount = 1f;
            }
            #endregion

            #region Resolve
            if (virtuesActivated[1])
            {
                resolveText.color = visibleTextColor;
                resolveText.text = HandleCooldownText(virtueCooldowns[2]);
                resolveFill.fillAmount = HandleFillAmount(virtueCooldowns[2], virtueCooldowns[3]);
            }
            else
            {
                resolveText.color = transparentTextColor;
                resolveText.text = "0";
                resolveFill.fillAmount = 1f;
            }
            #endregion

            #region Courage
            if (virtuesActivated[2])
            {
                courageText.color = visibleTextColor;
                courageText.text = HandleCooldownText(virtueCooldowns[4]);
                courageFill.fillAmount = HandleFillAmount(virtueCooldowns[4], virtueCooldowns[5]);
            }
            else
            {
                courageText.color = transparentTextColor;
                courageText.text = "0";
                courageFill.fillAmount = 1f;
            }
            #endregion

            #endregion

            #region Special
            if (hudType == HUDType.Dragonhunter)
            {
                // get Hunter's Verdict cooldown
            }
            else if (hudType == HUDType.Firebrand)
            {
                // get Tome charges
            }
            else if (hudType == HUDType.Willbender)
            {
                
            }
            #endregion
        }

        private string HandleCooldownText(float cooldown)
        {
            if (cooldown <= 3.0f)
            {
                return cooldown.ToString("0.0");
            }
            else
            {
                return cooldown.ToString("0");
            }
        }

        private float HandleFillAmount(float cooldown, float maxCooldown)
        {
            return (1f - (cooldown / maxCooldown));
        }
    }
}
