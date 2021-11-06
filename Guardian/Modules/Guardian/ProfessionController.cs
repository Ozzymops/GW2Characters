using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace Guardian.Modules.Guardian
{
    public class ProfessionController : MonoBehaviour
    {
        private int eliteSpecialisation = 0;
        private bool setup = false;
        private CharacterBody characterBody;

        public void Awake()
        {
            if (!setup)
            {
                characterBody = GetComponent<CharacterBody>();

                if (characterBody.baseNameToken.StartsWith("OZZ_GUARDIAN"))
                {
                    characterBody.gameObject.AddComponent<AttackChainController>();
                    characterBody.gameObject.GetComponent<AttackChainController>().Setup(eliteSpecialisation);

                    characterBody.gameObject.AddComponent<MechanicController>();
                    characterBody.gameObject.GetComponent<MechanicController>().Setup(eliteSpecialisation, characterBody, characterBody.gameObject.GetComponent<AttackChainController>());

                    characterBody.gameObject.AddComponent<TraitController>();
                    characterBody.gameObject.GetComponent<TraitController>().Setup(eliteSpecialisation, characterBody, characterBody.gameObject.GetComponent<MechanicController>());

                    characterBody.gameObject.AddComponent<HUDController>();
                    characterBody.gameObject.GetComponent<HUDController>().Setup(eliteSpecialisation, characterBody.gameObject.GetComponent<MechanicController>(), characterBody.gameObject.GetComponent<TraitController>());
                }
            }
        }
    }
}
