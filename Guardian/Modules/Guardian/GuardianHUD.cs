using RoR2;
using RoR2.UI;
using Guardian.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Guardian.Modules.Guardian
{
    public class GuardianHUD : MonoBehaviour
    {
        private bool activateHUD;
        private HUD hud;
        private VirtueController virtueController;
        private GameObject customHUD;

        private void Awake()
        {
            if (GetComponent<CharacterBody>().baseNameToken.Contains("GUARDIAN"))
            {
                hud = this.GetComponent<HUD>();
                virtueController = GetComponent<VirtueController>();

                if (virtueController == null)
                {
                    Debug.LogError("GuardianHUD: no virtue controller found!");
                }

                activateHUD = true;
                Debug.LogWarning("Enabled guardian HUD.");

                On.RoR2.UI.HUD.Awake += BuildHUD;
                On.RoR2.UI.HUD.Update += UpdateHUD;
            }
        }

        private void OnDestroy()
        {
            On.RoR2.UI.HUD.Awake -= BuildHUD;
            On.RoR2.UI.HUD.Update -= UpdateHUD;
        }

        public void BuildHUD(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);
            hud = self;

            customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>("GuardianHUD"), hud.mainContainer.transform);
        }

        public void UpdateHUD(On.RoR2.UI.HUD.orig_Update orig, RoR2.UI.HUD self)
        {
            orig(self);

            if (virtueController._justiceUsed)
            {
                customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueController._justiceCooldown <= 4f)
                {
                    customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueController._justiceCooldown.ToString("0.0");
                }
                else
                {
                    customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueController._justiceCooldown.ToString("0");
                }
          
                customHUD.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1f - (virtueController._justiceCooldown / 10f);
            }
            else
            {
                customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
                customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
                customHUD.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1f;
            }

            if (virtueController._resolveUsed)
            {
                customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueController._resolveCooldown <= 4f)
                {
                    customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueController._resolveCooldown.ToString("0.0");
                }
                else
                {
                    customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueController._resolveCooldown.ToString("0");
                }

                customHUD.transform.GetChild(1).GetChild(0).GetComponent<Image>().GetComponent<Image>().fillAmount = 1f - (virtueController._resolveCooldown / 15f);
            }
            else
            {
                customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
                customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
                customHUD.transform.GetChild(1).GetChild(0).GetComponent<Image>().GetComponent<Image>().fillAmount = 1f;
            }

            if (virtueController._courageUsed)
            {
                customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueController._courageCooldown <= 4f)
                {
                    customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueController._courageCooldown.ToString("0.0");
                }
                else
                {
                    customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueController._courageCooldown.ToString("0");
                }

                customHUD.transform.GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = 1f - (virtueController._courageCooldown / 20f);
            }
            else
            {
                customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
                customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
                customHUD.transform.GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = 1f;
            }
        }
    }
}
