using RoR2;
using RoR2.UI;
using Guardian.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Guardian.Modules.Guardian
{
    // parts of code were liberated from Daredevil
    public class GuardianHUD : MonoBehaviour
    {
        private HUD hud;

        private void Awake()
        {
            hud = this.GetComponent<HUD>();
        }

        public void Update()
        {
            if (hud == null || hud.targetBodyObject == null)
            {
                return;
            }
        }
    }
}
