using GuardianPlugin;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    public class VirtueController : MonoBehaviour
    {
        private bool scriptEnabled;

        private CharacterBody characterBody;
        private HealthComponent healthComponent;
        private TraitController traitController;

        private int hitsForJustice;
        private int hitsUntilJustice = 2;
        private bool justiceActive;
        private float justiceCooldown = 15f;
        private float justiceCooldownTimer;

        private float passiveResolveInterval = 2f;
        private float passiveResolveIntervalTimer;
        private float passiveResolveHeal = 2.5f;
        private float activeResolveHeal = 15f;
        private bool resolveActive;
        private float resolveCooldown = 20f;
        private float resolveCooldownTimer;

        private float passiveCourageInterval = 10f;
        private float passiveCourageIntervalTimer;
        private int activeCourageCount;
        private int activeCourageCountMax = 4;
        private bool courageActive;
        private float courageCooldown = 30f;
        private float courageCooldownTimer;

        private void Awake()
        {
            characterBody = GetComponent<CharacterBody>();
            healthComponent = GetComponent<HealthComponent>();

            if (characterBody.baseNameToken.Contains("GUARDIAN"))
            {
                hitsForJustice = 0;
                passiveResolveIntervalTimer = passiveResolveInterval;
                passiveCourageIntervalTimer = passiveCourageInterval;
                activeCourageCount = activeCourageCountMax;

                justiceCooldownTimer = justiceCooldown;
                resolveCooldownTimer = resolveCooldown;
                courageCooldownTimer = courageCooldown;

                scriptEnabled = true;
            }
        }

        private void Update()
        {
            if (scriptEnabled)
            {
                Controls();
                Cooldowns();
                PassiveEffects();
            }
        }

        private void Controls()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !justiceActive)
            {
                Util.PlaySound("PlayJusticeActivation", characterBody.gameObject);
                ApplyJustice(5, false);
                justiceActive = true;

                Debug.Log("[Active Justice]: activated.");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && !resolveActive)
            {
                Util.PlaySound("PlayResolveActivation", characterBody.gameObject);
                ApplyResolve(activeResolveHeal, false);
                resolveActive = true;

                Debug.Log("[Active Resolve]: activated.");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && !courageActive)
            {
                courageActive = true;
                passiveCourageIntervalTimer = 0f;
                Debug.Log("[Active Courage]: activated.");
            }
        }

        private void Cooldowns()
        {
            if (justiceActive)
            {
                justiceCooldownTimer -= 1f * Time.deltaTime;

                if (justiceCooldownTimer <= 0)
                {
                    hitsForJustice = 0;
                    justiceActive = false;
                    justiceCooldownTimer = justiceCooldown;
                }
            }

            if (resolveActive)
            {
                resolveCooldownTimer -= 1f * Time.deltaTime;

                if (resolveCooldownTimer <= 0)
                {
                    passiveResolveIntervalTimer = passiveResolveInterval;
                    resolveActive = false;
                    resolveCooldownTimer = resolveCooldown;
                }
            }

            if (courageActive)
            {
                courageCooldownTimer -= 1f * Time.deltaTime;

                if (courageCooldownTimer <= 0)
                {
                    passiveCourageIntervalTimer = passiveCourageInterval;
                    activeCourageCount = activeCourageCountMax;
                    courageActive = false;
                    courageCooldownTimer = courageCooldown;
                }
            }
        }

        private void PassiveEffects()
        {
            if (!justiceActive)
            {
                if (hitsForJustice + 1 > hitsUntilJustice)
                {
                    ApplyJustice(1, false);
                    hitsForJustice = 0;
                }
            }

            if (!resolveActive)
            {
                passiveResolveIntervalTimer -= 1f * Time.deltaTime;

                if (passiveResolveIntervalTimer <= 0)
                {
                    ApplyResolve(passiveResolveHeal, false);
                    passiveResolveIntervalTimer = passiveResolveInterval;
                }
            }

            if (courageActive)
            {
                passiveCourageIntervalTimer -= 1f * Time.deltaTime;

                if (passiveCourageIntervalTimer <= 0 && activeCourageCount > 0)
                {
                    ApplyCourage(false);
                    passiveCourageIntervalTimer = 1f;
                    activeCourageCount--;

                    Debug.Log("[Active Courage]: procced.");
                }
            }
            else
            {
                passiveCourageIntervalTimer -= 1f * Time.deltaTime;

                if (passiveCourageIntervalTimer <= 0)
                {
                    ApplyCourage(false);
                    passiveCourageIntervalTimer = passiveCourageInterval;

                    Debug.Log("[Passive Courage]: procced.");
                }
            }
        }

        private void ApplyJustice(int stacks, bool aoe)
        {
            for (int i = 0; i < stacks; i++)
            {
                characterBody.AddTimedBuff(GuardianPlugin.Modules.Buffs.justiceBuff, 9.9f, 5);
            }
        }

        private void ApplyResolve(float percent, bool aoe)
        {
            percent /= 100;

            healthComponent.HealFraction(percent, new ProcChainMask());

            if (aoe)
            {

            }
        }

        private void ApplyCourage(bool aoe)
        {
            Util.PlaySound("PlayCourageActivation", characterBody.gameObject);
            characterBody.AddTimedBuff(GuardianPlugin.Modules.Buffs.aegisBuff, 9.9f, 5);
        }

        public void IncrementJustice()
        {
            if (!justiceActive)
            {
                hitsForJustice++;
            }
        }

        public void RenewJustice()
        {
            if (justiceActive)
            {
                justiceCooldownTimer = 0f;
            }
        }

        public void ReduceCooldownsAndIntervals()
        {
            justiceCooldown *= 0.66f;
            resolveCooldown *= 0.66f;
            courageCooldown *= 0.66f;

            passiveResolveInterval *= 0.66f;
            passiveCourageInterval *= 0.66f;

            justiceCooldownTimer = 0f;
            resolveCooldownTimer = 0f;
            courageCooldownTimer = 0f;
        }

        /// <summary>
        /// Get VirtueController active virtues
        /// </summary>
        /// <returns>justice, resolve, courage</returns>
        public bool[] GetBools()
        {
            return new bool[] { justiceActive, resolveActive, courageActive };
        }

        /// <summary>
        /// Get current and maximum VirtueController cooldowns
        /// </summary>
        /// <returns>justice timer, justice cooldown, resolve timer, resolve cooldown, courage timer, courage cooldown</returns>
        public float[] GetCooldowns()
        {
            return new float[] { justiceCooldownTimer, justiceCooldown, resolveCooldownTimer, resolveCooldown, courageCooldownTimer, courageCooldown };
        }
    }
}
