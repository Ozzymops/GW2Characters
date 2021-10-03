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

            // Justice: 0 0 1
            if (virtueBools[0])
            {
                customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueFloats[0] <= 4f)
                {
                    customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueFloats[0].ToString("0.0");
                }
                else
                {
                    customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueFloats[0].ToString("0");
                }
          
                customHUD.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1f - (virtueFloats[0] / virtueFloats[1]);
            }
            else
            {
                customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
                customHUD.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
                customHUD.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1f;
            }

            // Resolve 1 2 3
            if (virtueBools[1])
            {
                customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueFloats[2] <= 4f)
                {
                    customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueFloats[2].ToString("0.0");
                }
                else
                {
                    customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueFloats[2].ToString("0");
                }

                customHUD.transform.GetChild(1).GetChild(0).GetComponent<Image>().GetComponent<Image>().fillAmount = 1f - (virtueFloats[2] / virtueFloats[3]);
            }
            else
            {
                customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
                customHUD.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
                customHUD.transform.GetChild(1).GetChild(0).GetComponent<Image>().GetComponent<Image>().fillAmount = 1f;
            }

            // Courage 2 4 5
            if (virtueBools[2])
            {
                customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);

                if (virtueFloats[4] <= 4f)
                {
                    customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueFloats[4].ToString("0.0");
                }
                else
                {
                    customHUD.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = virtueFloats[4].ToString("0");
                }

                customHUD.transform.GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = 1f - (virtueFloats[4] / virtueFloats[5]);
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
