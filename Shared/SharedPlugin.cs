using BepInEx;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API.Utils;
using RoR2;
using RoR2.UI;
using SharedPlugin.Modules;
using System;
using UnityEngine;

namespace ExamplePlugin
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]

    public class SharedPlugin : BaseUnityPlugin
	{
        public const string MODUID = "com.Ozzymops.GW2Shared";
        public const string MODNAME = "GW2Shared";
        public const string MODVERSION = "1.0.0";

        public static SharedPlugin instance;

        public void Awake()
        {
            instance = this;           

            Assets.Initialize();
            Buffs.RegisterBuffs();

            new ContentPacks().Initialize();

            Hook();
        }

        public void Hook()
        {
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_Shared_RecalculateStats;
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_Shared_TakeDamage;
            PoisonedHealingReduction();
        }

        public void OnDestroy()
        {
            On.RoR2.CharacterBody.RecalculateStats -= CharacterBody_Shared_RecalculateStats;
            On.RoR2.HealthComponent.TakeDamage -= HealthComponent_Shared_TakeDamage;
        }

        private void CharacterBody_Shared_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {      
            orig(self);

            if (self)
            {
                // Alacrity - reduce skill cooldowns by 33%
                if (self.HasBuff(Buffs.alacrityBuff))
                {
                    self.skillLocator.primary.cooldownScale *= 0.66f;
                    self.skillLocator.secondary.cooldownScale *= 0.66f;
                    self.skillLocator.utility.cooldownScale *= 0.66f;
                    self.skillLocator.special.cooldownScale *= 0.66f;
                }

                // Fury - increase critical strike chance by 20%
                if (self.HasBuff(Buffs.furyBuff))
                {
                    self.crit += 20f;
                }

                // Quickness - increase attack speed by 20%
                if (self.HasBuff(Buffs.quicknessBuff))
                {
                    self.attackSpeed *= 1.2f;
                }

                // Regeneration - increase regeneration by 5
                if (self.HasBuff(Buffs.regenerationBuff))
                {
                    self.regen += 5f;
                }

                // Resistance - ignore non-damaging debuffs
                if (!self.HasBuff(Buffs.resistanceBuff))
                {
                    // Chilled - reduce movement and attack speed by 33%
                    if (self.HasBuff(Buffs.chilledDebuff))
                    {
                        self.moveSpeed *= 0.66f;
                        self.attackSpeed *= 0.66f;
                    }

                    // Crippled - reduce movement speed by 66%
                    if (self.HasBuff(Buffs.crippledDebuff))
                    {
                        self.moveSpeed *= 0.33f;
                    }

                    // Immobilized - reduce movement speed by 66%
                    if (self.HasBuff(Buffs.crippledDebuff))
                    {
                        self.moveSpeed *= 0.00f;
                    }

                    // Slow - reduce attack speed by 66%
                    if (self.HasBuff(Buffs.slowDebuff))
                    {
                        self.attackSpeed *= 0.33f;
                    }
                }

                // Swiftness - increase movement speed by 25%
                if (self.HasBuff(Buffs.swiftnessBuff))
                {
                    self.moveSpeed *= 1.25f;
                }

                // Vigor - increase jump height by 20%
                if (self.HasBuff(Buffs.vigorBuff))
                {
                    self.jumpPower *= 1.2f;
                }
            }
        }

        public void HealthComponent_Shared_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            // Aegis - fully negate an incoming direct attack
            if (self.body.HasBuff(Buffs.aegisBuff) && damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
            {
                Buffs.HandleTimedBuffs(Buffs.aegisBuff, self.body, 5);

                EffectData effectData = new EffectData { origin = damageInfo.position, rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero ? damageInfo.force : UnityEngine.Random.onUnitSphere)) };
                EffectManager.SpawnEffect(Resources.Load<GameObject>("prefabs/effects/BearProc"), effectData, true);

                damageInfo.rejected = true;
            }

            // Fury - increase critical hit damage by 20%
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.furyBuff))
            {
                if (damageInfo.crit)
                {
                    damageInfo.damage *= 1.2f;
                }
            }

            // Might - increase outgoing damage by 2% per stack up to 50%
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.mightBuff))
            {
                damageInfo.damage *= (1f + (0.02f * damageInfo.attacker.GetComponent<CharacterBody>().GetBuffCount(Buffs.mightBuff))); // 0% - 50%
            }

            // Protection - reduce incoming direct attack damage by 33%
            if (self.body.HasBuff(Buffs.protectionBuff) && damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
            {
                damageInfo.damage *= 0.66f;
            }

            // Resolution - reduce incoming DoT damage by 33%
            if (self.body.HasBuff(Buffs.resolutionBuff) && damageInfo.damageType == DamageType.DoT)
            {
                damageInfo.damage *= 0.66f;
            }

            // Stability - nullify all knockback taken
            if (self.body.HasBuff(Buffs.stabilityBuff) && damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
            {
                damageInfo.force = Vector3.zero;
            }

            // Blinded - fully negate an outgoing direct attack
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.blindedDebuff))
            {
                Buffs.HandleTimedBuffs(Buffs.blindedDebuff, damageInfo.attacker.GetComponent<CharacterBody>(), 1);
                damageInfo.rejected = true;
            }

            // Confusion - take damage per stack when attacking
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.confusionDebuff))
            {
                DamageInfo confusionDamageInfo = new DamageInfo();
                confusionDamageInfo.damage = damageInfo.attacker.GetComponent<CharacterBody>().damage * (500f * damageInfo.attacker.GetComponent<CharacterBody>().GetBuffCount(Buffs.confusionDebuff));
                confusionDamageInfo.attacker = damageInfo.attacker;
                confusionDamageInfo.inflictor = damageInfo.attacker;
                confusionDamageInfo.force = Vector3.zero;
                confusionDamageInfo.crit = false;
                confusionDamageInfo.procCoefficient = 0f;
                confusionDamageInfo.damageType = DamageType.BypassArmor;

                damageInfo.attacker.GetComponent<CharacterBody>().healthComponent.TakeDamage(confusionDamageInfo);

                Buffs.HandleTimedBuffs(Buffs.confusionDebuff, damageInfo.attacker.GetComponent<CharacterBody>(), 25);
            }

            // Vulnerability - increase incoming damage by 2% per stack up to 50%
            if (self.body.HasBuff(Buffs.vulnerabilityDebuff))
            {
                damageInfo.damage *= (1f + (0.02f * self.body.GetBuffCount(Buffs.vulnerabilityDebuff))); // 0% - 50%
            }

            // Weakness - 50% of glancing blows
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.weaknessDebuff))
            {
                bool glancingBlow = Util.CheckRoll(50, damageInfo.attacker.GetComponent<CharacterBody>().master);

                if (glancingBlow)
                {
                    damageInfo.crit = false;
                    damageInfo.damage *= 0.5f;
                }
            }

            orig(self, damageInfo);
        }

        private void PoisonedHealingReduction()
        {
            IL.RoR2.HealthComponent.HandleHeal += il =>
            {
                ILCursor c = new ILCursor(il);

                bool found;

                found = c.TryGotoNext(
                        x => x.MatchLdarg(1),
                        x => x.MatchStloc(2));

                if (found)
                {
                    c.Emit(OpCodes.Ldloc_2);
                    c.Emit(OpCodes.Ldarg_0);
                    c.EmitDelegate<Func<float, HealthComponent, float>>((origHeal, component) =>
                    {
                        float newHeal = origHeal;

                        if (component.GetComponent<CharacterBody>() is CharacterBody body)
                        {
                            if (component.GetComponent<CharacterBody>().HasBuff(Buffs.poisonedDebuff))
                            {
                                newHeal *= 0.5f;
                                Debug.LogError("GW2Shared - poisoned healing reduction succesfully executed.");
                            }
                        }

                        return newHeal;
                    });

                    c.Emit(OpCodes.Stloc_2);
                }
                else
                {
                    Debug.LogError("GW2Shared - poisoned healing reduction hook failed to register.");
                }
            };
        }
    }
}
