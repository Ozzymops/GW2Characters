using GuardianPlugin.SkillStates;
using RoR2;
using UnityEngine;

namespace Guardian.Modules
{
    // parts of code were liberated from Daredevil
    public class AttackChainController : MonoBehaviour
    {
        public int chainCount;
        public int maxChainCount = 2;
        public float chainTimer;
        public float chainTimerDecay = 3f;

        private void Awake()
        {
            chainCount = 0;
        }

        private void ChainDecay()
        {
            if (chainCount > 0)
            {
                chainTimerDecay -= Time.fixedDeltaTime;
            }
        }
    }
}
