using BepInEx;
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
            On.RoR2.HealthComponent.TakeDamage += HandleGuardianBuffs;
        }

        private void OnDestroy() 
        {
            On.RoR2.HealthComponent.TakeDamage -= HandleGuardianBuffs;
        }

        private void HandleGuardianBuffs(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            CharacterBody attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();

            // Justice
            if (damageInfo != null && damageInfo.attacker && attackerBody && attackerBody.baseNameToken.Contains("GUARDIAN"))
            {
                if (damageInfo.damageType != DamageType.DoT)
                {
                    if (attackerBody.HasBuff(Modules.Buffs.justiceBuff))
                    {
                        int buffCount = 0;
                        float buffTimer = 0f;

                        foreach(CharacterBody.TimedBuff timedBuff in attackerBody.timedBuffs)
                        {
                            if (timedBuff.buffIndex == Modules.Buffs.justiceBuff.buffIndex)
                            {
                                buffTimer = timedBuff.timer;
                                buffCount++;
                            }
                        }

                        attackerBody.ClearTimedBuffs(Modules.Buffs.justiceBuff);

                        // Re-add buffs minus one
                        for (int i = 1; i < buffCount; i++)
                        {
                            attackerBody.AddTimedBuff(Modules.Buffs.justiceBuff, buffTimer, 5);
                        }

                        damageInfo.damageType = DamageType.PercentIgniteOnHit;                      
                    }

                    damageInfo.attacker.GetComponent<Guardian.Modules.Guardian.VirtueController>().IncrementJustice();
                }
            }

            // Courage
            if (self.body.HasBuff(Modules.Buffs.aegisBuff))
            {
                if (damageInfo.damageType != DamageType.DoT)
                {
                    Debug.Log("[Passive Courage]: aegis used, buff removed.");

                    self.body.ClearTimedBuffs(Modules.Buffs.aegisBuff);

                    EffectData effectData = new EffectData { origin = damageInfo.position, rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero ? damageInfo.force : Random.onUnitSphere)) };
                    EffectManager.SpawnEffect(Resources.Load<GameObject>("prefabs/effects/BearProc"), effectData, true);
                    damageInfo.rejected = true;
                }
            }

            orig.Invoke(self, damageInfo);
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