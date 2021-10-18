using BepInEx;
using R2API.Utils;
using RoR2;
using RoR2.UI;
using SharedPlugin.Modules;
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
                // Fury - increase critical strike chance by 20%
                if (self.HasBuff(Buffs.furyBuff))
                {
                    self.crit += 20f;
                }

                // Regeneration - increase regeneration by 5
                if (self.HasBuff(Buffs.regenerationBuff))
                {
                    self.regen += 5f;
                }

                // Swiftness - increase movement speed by 25%
                if (self.HasBuff(Buffs.swiftnessBuff))
                {
                    self.moveSpeed *= 1.25f;
                }

                // Chill - reduce movement and attack speed by 33%
                if (self.HasBuff(Buffs.chillDebuff))
                {
                    self.moveSpeed *= 0.66f;
                    self.attackSpeed *= 0.66f;
                }

                // Cripple - reduce movement speed by 66%
                if (self.HasBuff(Buffs.chillDebuff))
                {
                    self.moveSpeed *= 0.33f;
                }

                // Torment - DoT, inflicts extra damage if moving
                if (self.HasBuff(Buffs.tormentDebuff))
                {

                }
            }
        }

        public void HealthComponent_Shared_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            // Aegis
            if (self.body.HasBuff(Buffs.aegisBuff) && damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
            {
                Buffs.HandleTimedBuffs(Buffs.aegisBuff, self.body, 5);

                EffectData effectData = new EffectData { origin = damageInfo.position, rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero ? damageInfo.force : Random.onUnitSphere)) };
                EffectManager.SpawnEffect(Resources.Load<GameObject>("prefabs/effects/BearProc"), effectData, true);

                damageInfo.rejected = true;
            }

            // Protection
            if (self.body.HasBuff(Buffs.protectionBuff) && damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
            {
                damageInfo.damage *= 0.66f;
            }

            // Vulnerability
            if (self.body.HasBuff(Buffs.vulnerabilityDebuff))
            {
                damageInfo.damage *= (1f + (0.02f * self.body.GetBuffCount(Buffs.vulnerabilityDebuff))); // 0% - 50%
            }

            // Weakness
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.mightBuff))
            {
                bool glancingBlow = Util.CheckRoll(50, damageInfo.attacker.GetComponent<CharacterBody>().master);

                if (glancingBlow)
                {
                    damageInfo.crit = false;
                    damageInfo.damage *= 0.5f;
                }
            }

            // Might
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.mightBuff))
            {
                damageInfo.damage *= (1f + (0.02f * damageInfo.attacker.GetComponent<CharacterBody>().GetBuffCount(Buffs.mightBuff))); // 0% - 50%
            }

            // Fury
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.furyBuff))
            {
                if (damageInfo.crit)
                {
                    damageInfo.damage *= 1.2f;
                }
            }

            orig(self, damageInfo);
        }
    }
}
