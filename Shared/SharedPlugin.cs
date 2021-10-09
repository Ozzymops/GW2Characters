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
            }
        }

        public void HealthComponent_Shared_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            CharacterBody attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();

            // Aegis - negate an attack
            if (self.body.HasBuff(Buffs.aegisBuff))
            {
                if (damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
                {
                    // Clear and reapply Aegis stacks minus one
                    ClearAndReapplyTimedBuffs(Buffs.aegisBuff, self.body, 5);

                    // "Blocked!" effect
                    EffectData effectData = new EffectData { origin = damageInfo.position, rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero ? damageInfo.force : Random.onUnitSphere)) };
                    EffectManager.SpawnEffect(Resources.Load<GameObject>("prefabs/effects/BearProc"), effectData, true);

                    damageInfo.rejected = true;
                }
            }

            // Protection - reduce incoming strike damage by 33%
            if (self.body.HasBuff(Buffs.protectionBuff))
            {
                if (damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
                {
                    damageInfo.damage *= 0.66f;
                }
            }

            // Might - increase outgoing strike damage by 2% up to 50% at 25 stacks
            if (attackerBody.HasBuff(Buffs.mightBuff))
            {
                damageInfo.damage *= (1f + (0.02f * attackerBody.GetBuffCount(Buffs.mightBuff)));
            }

            // Vulnerability - increase incoming strike damage by 2% up to 50% at 25 stacks
            if (self.body.HasBuff(Buffs.vulnerabilityDebuff))
            {
                if (damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
                {
                    damageInfo.damage *= (1f + (0.02f * self.body.GetBuffCount(Buffs.vulnerabilityDebuff)));
                }
            }

            // Weakness - 50% chance at "Glancing Blows", dealing 50% reduced damage that cannot critically strike
            if (attackerBody.HasBuff(Buffs.weaknessDebuff))
            {
                bool glancingBlow = Util.CheckRoll(50, attackerBody.master);

                if (glancingBlow)
                {
                    damageInfo.crit = false;
                    damageInfo.damage *= 0.5f;
                }
            }

            // Original code
            orig(self, damageInfo);
        }

        private void ClearAndReapplyTimedBuffs(BuffDef buffDef, CharacterBody body, int maxStacks)
        {
            int buffCount = 0;
            float buffTimer = 0f;

            foreach (CharacterBody.TimedBuff buff in body.timedBuffs)
            {
                if (buff.buffIndex == buffDef.buffIndex)
                {
                    if (buffTimer > buff.timer || buffTimer == 0f)
                    {
                        buffTimer = buff.timer;
                    }

                    buffCount++;
                }
            }

            body.ClearTimedBuffs(buffDef);

            for (int i = 1; i < buffCount; i++)
            {
                body.AddTimedBuff(buffDef, buffTimer, maxStacks);
            }
        }
    }
}
