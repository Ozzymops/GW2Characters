using BepInEx;
using Guardian.Modules.Guardian;
using GuardianPlugin.Modules.Survivors;
using R2API.Utils;
using RoR2;
using RoR2.UI;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace GuardianPlugin
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "LanguageAPI",
        "SoundAPI",
    })]

    public class GuardianPlugin : BaseUnityPlugin
    {
        // if you don't change these you're giving permission to deprecate the mod-
        //  please change the names to your own stuff, thanks
        //   this shouldn't even have to be said
        public const string MODUID = "com.Ozzymops.GW2Guardian";
        public const string MODNAME = "GW2Guardian";
        public const string MODVERSION = "1.0.0";

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string developerPrefix = "OZZ";

        internal List<SurvivorBase> Survivors = new List<SurvivorBase>();

        public static GuardianPlugin instance;


        private void Awake()
        {
            instance = this;

            // load assets and read config
            Modules.Assets.Initialize();
            Modules.Config.ReadConfig();
            Modules.States.RegisterStates(); // register states for networking
            Modules.Buffs.RegisterBuffs(); // add and register custom buffs/debuffs
            Modules.Projectiles.RegisterProjectiles(); // add and register custom projectiles
            Modules.Tokens.AddTokens(); // register name tokens
            Modules.ItemDisplays.PopulateDisplays(); // collect item display prefabs for use in our display rules

            // survivor initialization
            new GuardianSurvivor().Initialize();

            // now make a content pack and add it- this part will change with the next update
            new Modules.ContentPacks().Initialize();

            RoR2.ContentManagement.ContentManager.onContentPacksAssigned += LateSetup;

            Hook();
        }



        private void LateSetup(HG.ReadOnlyArray<RoR2.ContentManagement.ReadOnlyContentPack> obj)
        {
            // have to set item displays later now because they require direct object references..
            Modules.Survivors.GuardianSurvivor.instance.SetItemDisplays();
        }

        private HUD hud = null;

        private void Hook()
        {
            // run hooks here, disabling one is as simple as commenting out the line
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
        }

        private void OnDestroy() 
        {
            On.RoR2.HealthComponent.TakeDamage -= HealthComponent_TakeDamage;
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            // Aegis - block all damage except DoT and Void Reaver explosion
            if (self.body.HasBuff(Modules.Buffs.aegisBuff) && damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.VoidDeath)
            {
                // Shattered Aegis

                // Clear and reapply stacks
                ClearAndReapplyTimedBuffs(Modules.Buffs.aegisBuff, self.body, 5);
                
                // Visual effect
                EffectData effectData = new EffectData { origin = damageInfo.position, rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero ? damageInfo.force : Random.onUnitSphere)) };
                EffectManager.SpawnEffect(Resources.Load<GameObject>("prefabs/effects/BearProc"), effectData, true);

                // Block
                Util.PlaySound("PlayCourageBlock", self.gameObject);
                damageInfo.rejected = true;
            }

            // Justice - deal 15% increased damage and inflict burning
            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Modules.Buffs.justiceBuff) && damageInfo.damageType != DamageType.DoT)
            {
                // Clear and reapply stacks
                ClearAndReapplyTimedBuffs(Modules.Buffs.justiceBuff, damageInfo.attacker.GetComponent<CharacterBody>(), 5);

                // Visual Effect

                // Extra damage & burning
                damageInfo.damage *= 1.15f;
                damageInfo.damageType = DamageType.IgniteOnHit;
            }

            if (damageInfo.attacker.GetComponent<CharacterBody>().baseNameToken.Contains("GUARDIAN"))
            {
                // Justice stacks
                damageInfo.attacker.GetComponent<VirtueController>().IncrementJustice();

                // Renewed Justice
                if (!self.alive && damageInfo.attacker.GetComponent<TraitController>().GetTraits()[1])
                {
                    damageInfo.attacker.GetComponent<VirtueController>().RenewJustice();
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
                buffTimer = buff.timer;
                buffCount++;
            }

            body.ClearTimedBuffs(buffDef);

            for (int i = 1; i < buffCount; i++)
            {
                body.AddTimedBuff(buffDef, buffTimer, maxStacks);
            }

            Debug.Log(buffCount + " * " + buffDef.name + " for " + buffTimer + "s.");
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);

            // a simple stat hook, adds armor after stats are recalculated
            if (self)
            {
                if (self.HasBuff(Modules.Buffs.armorBuff))
                {
                    self.armor += 300f;
                }
            }
        }
    }
}