using RoR2;
using System.Collections.Generic;
using UnityEngine;
using static R2API.DamageAPI;
using static R2API.DotAPI;

namespace SharedPlugin.Modules.Conditions
{
    public class ConditionBleedingComponent : MonoBehaviour
    {
        public List<ConditionBleedingStack> conditionBleedingStacks = new List<ConditionBleedingStack>();
    }

    public class ConditionBleedingStack
    {
        private int ticksLeft;
        private float tickTimer;
        private float tickInterval = 0.5f;
        private float damage;
        private float damageDealt;
        private GameObject attacker;
        private DamageType damageType;
        
        public ConditionBleedingStack(int ticksLeft, float tickTimer, float damage, GameObject attacker, DamageType damageType)
        {
            this.ticksLeft = ticksLeft;
            this.tickTimer = tickTimer;
            this.damage = damage;
            this.attacker = attacker;
            this.damageType = damageType;
        }

        private void Awake()
        {
            Hooks();
        }

        private void Hooks()
        {
            On.RoR2.CharacterBody.FixedUpdate += TickDamage;
        }

        private void FixedUpdate(RoR2.CharacterBody characterBody)
        {
            tickTimer -= Time.fixedDeltaTime;
            ticksLeft -= 1;

            if (tickTimer <= 0)
            {
                tickTimer = tickInterval;

                DamageInfo bleedInfo = new DamageInfo
                {
                    attacker = this.attacker,
                    crit = false,
                    damage = this.damage,
                    force = Vector3.zero,
                    inflictor = null,
                    position = characterBody.corePosition,
                    procCoefficient = 0f,
                    damageColorIndex = DamageColorIndex.Bleed,
                    damageType = this.damageType
                };

                characterBody.healthComponent.TakeDamage(bleedInfo);
            }
        }

        public void TickDamage(On.RoR2.CharacterBody.orig_FixedUpdate orig, RoR2.CharacterBody self)
        {
            orig(self);

            ConditionBleedingComponent bleedComponent = self.GetComponent<ConditionBleedingComponent>();

            if (!bleedComponent) { bleedComponent = self.gameObject.AddComponent<ConditionBleedingComponent>(); }

            foreach (ConditionBleedingStack bleedStack in bleedComponent.conditionBleedingStacks)
            {
                bleedStack.FixedUpdate(self);
            }

            bleedComponent.conditionBleedingStacks.RemoveAll(x => x.ticksLeft <= 0);
        }
    }
}