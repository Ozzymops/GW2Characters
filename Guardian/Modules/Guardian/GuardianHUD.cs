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
        private TraitController traitController;
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

        private Image adeptTrait;
        private Image adeptTraitFill;
        private Image masterTrait;
        private Image masterTraitFill;
        private Image grandmasterTrait;
        private Image grandmasterTraitFill;

        #region Special
        private Image dhPullBack;
        private Image dhPullFill;
        private Text dhPullText;

        private Image[] fbTomePulseAnim;
        private Image fbChargesBack;
        private Image fbChargesFill;

        private Image[] wbEmanatingVirtuePulseAnim;
        #endregion
        
        #endregion

        private void Awake()
        {
            if (GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ"))
            {
                activateScript = true;              
            }

            if (activateScript)
            {
                Debug.Log("GW2 GuardianHUD: Guardian player detected, enabling custom HUD.");

                hud = GetComponent<HUD>();
                virtueController = GetComponent<VirtueController>();
                traitController = GetComponent<TraitController>();

                On.RoR2.UI.HUD.Awake += CreateHUD;
                On.RoR2.UI.HUD.Update += UpdateHUD;
            }
        }

        private void Update()
        {
            if (activateScript)
            {
                // DEBUGGING
                if (hudType == HUDType.Firebrand)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        fbTomePulseAnim[0].enabled = true;
                        fbTomePulseAnim[1].enabled = true;
                        fbTomePulseAnim[2].enabled = true;
                        fbTomePulseAnim[0].GetComponent<Animator>().Play("Base Layer.Pulse", 0, 0f);
                        fbTomePulseAnim[1].GetComponent<Animator>().Play("Base Layer.Pulse", 0, 0f);
                        fbTomePulseAnim[2].GetComponent<Animator>().Play("Base Layer.Pulse", 0, 0f);
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        fbTomePulseAnim[0].enabled = false;
                        fbTomePulseAnim[1].enabled = false;
                        fbTomePulseAnim[2].enabled = false;
                        fbTomePulseAnim[0].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);
                        fbTomePulseAnim[1].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);
                        fbTomePulseAnim[2].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);
                    }
                }

                if (hudType == HUDType.Willbender)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        wbEmanatingVirtuePulseAnim[0].enabled = true;
                        wbEmanatingVirtuePulseAnim[1].enabled = true;
                        wbEmanatingVirtuePulseAnim[2].enabled = true;
                        wbEmanatingVirtuePulseAnim[0].GetComponent<Animator>().Play("Base Layer.Pulse", 0, 0f);
                        wbEmanatingVirtuePulseAnim[1].GetComponent<Animator>().Play("Base Layer.Pulse", 0, 0f);
                        wbEmanatingVirtuePulseAnim[2].GetComponent<Animator>().Play("Base Layer.Pulse", 0, 0f);
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        wbEmanatingVirtuePulseAnim[0].enabled = false;
                        wbEmanatingVirtuePulseAnim[1].enabled = false;
                        wbEmanatingVirtuePulseAnim[2].enabled = false;
                        wbEmanatingVirtuePulseAnim[0].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);
                        wbEmanatingVirtuePulseAnim[1].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);
                        wbEmanatingVirtuePulseAnim[2].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);
                    }
                }
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
                    adeptTraitFill = customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
                    masterTraitFill = customHUD.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>();
                    Debug.Log("GW2 GuardianHUD: Core HUD loaded.");
                    break;

                case HUDType.Dragonhunter:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[1]), hud.mainContainer.transform);
                    dhPullBack = customHUD.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
                    dhPullFill = customHUD.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>();
                    dhPullText = customHUD.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>();
                    Debug.Log("GW2 GuardianHUD: Dragonhunter HUD loaded.");
                    break;

                case HUDType.Firebrand:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[2]), hud.mainContainer.transform);
                    fbTomePulseAnim = new Image[] { customHUD.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>(),
                                                    customHUD.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Image>(),
                                                    customHUD.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Image>() };
                    fbChargesBack = customHUD.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>();
                    fbChargesFill = customHUD.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Image>();

                    fbTomePulseAnim[0].enabled = false;
                    fbTomePulseAnim[1].enabled = false;
                    fbTomePulseAnim[2].enabled = false;
                    fbTomePulseAnim[0].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);
                    fbTomePulseAnim[1].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);
                    fbTomePulseAnim[2].GetComponent<Animator>().Play("Base Layer.Idle", 0, 0f);

                    Debug.Log("GW2 GuardianHUD: Firebrand HUD loaded.");
                    break;

                case HUDType.Willbender:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[3]), hud.mainContainer.transform);
                    wbEmanatingVirtuePulseAnim = new Image[] { customHUD.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>(),
                                                               customHUD.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Image>(),
                                                               customHUD.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Image>() };
                    Debug.Log("GW2 GuardianHUD: Willbender HUD loaded.");
                    break;

                default:
                    customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabName[0]), hud.mainContainer.transform);
                    Debug.Log("GW2 GuardianHUD: HUDType was not set correctly, defaulting to Core HUD.");
                    break;
            }

            justiceFill = customHUD.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
            resolveFill = customHUD.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>();
            courageFill = customHUD.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>();
            justiceText = customHUD.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>();
            resolveText = customHUD.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>();
            courageText = customHUD.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>();

            adeptTrait = customHUD.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            masterTrait = customHUD.transform.GetChild(1).GetChild(1).GetComponent<Image>();
            grandmasterTrait = customHUD.transform.GetChild(1).GetChild(2).GetComponent<Image>();

            adeptTrait.enabled = false;
            masterTrait.enabled = false;
            grandmasterTrait.enabled = false;

            if (adeptTraitFill)
            {
                adeptTraitFill.enabled = false;
            }

            if (masterTraitFill)
            {
                masterTraitFill.enabled = false;
            }

            if (grandmasterTraitFill)
            {
                grandmasterTraitFill.enabled = false;
            }
        }

        private void UpdateHUD(On.RoR2.UI.HUD.orig_Update orig, RoR2.UI.HUD self)
        {
            orig(self);

            bool[] virtues = virtueController.GetActivatedVirtues();
            float[] virtueCD = virtueController.GetCooldowns();

            bool[] traits = traitController.GetTraits();
            bool[] traitsCon = traitController.GetConditionalTraits();
            float[] traitsCD = traitsCD = traitController.GetCooldowns();

            #region General

            #region Justice
            if (!virtues[0])
            {
                justiceText.color = visibleTextColor;
                justiceText.text = HandleCooldownText(virtueCD[0]);
                justiceFill.fillAmount = HandleFillAmount(virtueCD[0], virtueCD[1]);
            }
            else
            {
                justiceText.color = transparentTextColor;
                justiceText.text = "0";
                justiceFill.fillAmount = 1f;
            }
            #endregion

            #region Resolve
            if (!virtues[1])
            {
                resolveText.color = visibleTextColor;
                resolveText.text = HandleCooldownText(virtueCD[2]);
                resolveFill.fillAmount = HandleFillAmount(virtueCD[2], virtueCD[3]);
            }
            else
            {
                resolveText.color = transparentTextColor;
                resolveText.text = "0";
                resolveFill.fillAmount = 1f;
            }
            #endregion

            #region Courage
            if (!virtues[2])
            {
                courageText.color = visibleTextColor;
                courageText.text = HandleCooldownText(virtueCD[4]);
                courageFill.fillAmount = HandleFillAmount(virtueCD[4], virtueCD[5]);
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
                // if tethered, show Hunter's Verdict
                // get Hunter's Verdict cooldown
                // will hide for now :)
                dhPullBack.enabled = false;
                dhPullFill.enabled = false;
                dhPullText.enabled = false;
            }
            else if (hudType == HUDType.Firebrand)
            {
                // get Tome charges
                // if in Tome, fill charges and drain charges
                // not handled with fill! will be handled with sizing
                // whill hide for now :)
                fbChargesBack.enabled = true;
                fbChargesFill.enabled = true;
            }
            else if (hudType == HUDType.Willbender)
            {
                
            }
            #endregion

            #region Traits
            if (traits[0])
            {
                adeptTrait.enabled = true;

                if (adeptTraitFill)
                {
                    adeptTraitFill.enabled = true;

                    if (!traitsCon[0])
                    {
                        adeptTraitFill.fillAmount = HandleFillAmount(traitsCD[0], traitsCD[1]);
                    }
                    else
                    {
                        adeptTraitFill.fillAmount = 1f;
                    }
                }
            }

            if (traits[1])
            {
                masterTrait.enabled = true;

                if (masterTraitFill)
                {
                    masterTraitFill.enabled = true;

                    if (!traitsCon[1])
                    {
                        masterTraitFill.fillAmount = HandleFillAmount(traitsCD[2], traitsCD[3]);
                    }
                    else
                    {
                        masterTraitFill.fillAmount = 1f;
                    }
                }
            }

            if (traits[2])
            {
                grandmasterTrait.enabled = true;

                if (grandmasterTraitFill)
                {
                    grandmasterTraitFill.enabled = true;

                    if (!traitsCon[2])
                    {
                        grandmasterTraitFill.fillAmount = HandleFillAmount(traitsCD[4], traitsCD[5]);
                    }
                    else
                    {
                        grandmasterTraitFill.fillAmount = 1f;
                    }
                }
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
