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
        private int hudType;
        private HUD hud;
        private VirtueController virtueController;
        private GameObject customHUD;

        private Image justiceSlot;
        private Image resolveSlot;
        private Image courageSlot;
        private Text justiceText;
        private Text resolveText;
        private Text courageText;

        private void Awake()
        {
            if (GetComponent<CharacterBody>().baseNameToken.Contains("GUARDIAN"))
            {
                hudType = 0;
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

            string prefabString = "";

            switch (hudType)
            {
                case 0: // Core
                    prefabString = "GuardianHUDCore";
                    break;

                case 1: // DH
                    prefabString = "GuardianHUDDH";
                    break;
            }

            customHUD = Instantiate(GuardianPlugin.Modules.Assets.subAssetBundle.LoadAsset<GameObject>(prefabString), hud.mainContainer.transform);

            justiceSlot = customHUD.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            resolveSlot = customHUD.transform.GetChild(2).GetChild(0).GetComponent<Image>();
            courageSlot = customHUD.transform.GetChild(3).GetChild(0).GetComponent<Image>();
            justiceText = customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
            resolveText = customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
            courageText = customHUD.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>();
        }

        public void UpdateHUD(On.RoR2.UI.HUD.orig_Update orig, RoR2.UI.HUD self)
        {
            orig(self);

            bool[] virtueBools = virtueController.GetBools();
            float[] virtueFloats = virtueController.GetCooldowns();

            if (virtueBools.Length != 3)
            {
                Debug.LogWarning("[UpdateHUD] virtueBools did not return three booleans!");
            }

            if (virtueFloats.Length != 6)
            {
                Debug.LogWarning("[UpdateHUD] virtueFloats did not return six floats!");
            }

            // Justice
            if (virtueBools[0])
            {
                justiceText.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueFloats[0] <= 4f)
                {
                    justiceText.text = virtueFloats[0].ToString("0.0");
                }
                else
                {
                    justiceText.text = virtueFloats[0].ToString("0");
                }
          
                justiceSlot.fillAmount = 1f - (virtueFloats[0] / virtueFloats[1]);
            }
            else
            {
                justiceText.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
                justiceText.text = "0";
                justiceSlot.fillAmount = 1f;
            }

            // Resolve
            if (virtueBools[1])
            {
                resolveText.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueFloats[2] <= 4f)
                {
                    resolveText.text = virtueFloats[2].ToString("0.0");
                }
                else
                {
                    resolveText.text = virtueFloats[2].ToString("0");
                }

                resolveSlot.fillAmount = 1f - (virtueFloats[2] / virtueFloats[3]);
            }
            else
            {
                resolveText.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
                resolveText.text = "0";
                resolveSlot.fillAmount = 1f;
            }

            // Courage
            if (virtueBools[2])
            {
                courageText.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueFloats[4] <= 4f)
                {
                    courageText.text = virtueFloats[4].ToString("0.0");
                }
                else
                {
                    courageText.text = virtueFloats[4].ToString("0");
                }

                courageSlot.fillAmount = 1f - (virtueFloats[4] / virtueFloats[5]);
            }
            else
            {
                courageText.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
                courageText.text = "0";
                courageSlot.fillAmount = 1f;
            }
        }
    }
}
