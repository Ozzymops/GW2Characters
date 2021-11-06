using BepInEx;
using Guardian.Modules;
using Guardian.Modules.Guardian;
using GuardianPlugin.Modules;
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
    [BepInDependency("com.Ozzymops.GW2Shared", BepInDependency.DependencyFlags.SoftDependency)]
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

            // 'Connect' with other plugin
            if (Guardian.Modules.SharedPluginWrapper.enabled)
            {
                Debug.Log("GW2Guardian - Dependency com.Ozzymops.GW2Shared loaded!");
            }
            else
            {
                Debug.LogError("GW2Guardian - Dependency com.Ozzymops.GW2Shared not found/loaded!");
            }

            Hook();
        }

        private void LateSetup(HG.ReadOnlyArray<RoR2.ContentManagement.ReadOnlyContentPack> obj)
        {
            // have to set item displays later now because they require direct object references..
            Modules.Survivors.GuardianSurvivor.instance.SetItemDisplays();
        }

        private void Hook()
        {
            On.RoR2.CharacterBody.RecalculateStats += RecalculateStats_Buffs;
            On.RoR2.HealthComponent.TakeDamage += TakeDamage_Traits;
        }        

        private void OnDestroy()
        {
            On.RoR2.CharacterBody.RecalculateStats -= RecalculateStats_Buffs;
            On.RoR2.HealthComponent.TakeDamage -= TakeDamage_Traits;
        }

        private void RecalculateStats_Buffs(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);

            if (self)
            {
                // Shield of Absorption
                if (self.HasBuff(Modules.Buffs.guardianShieldOfAbsorptionBuff))
                {
                    self.armor += 50f;
                    self.moveSpeed *= 0.8f;
                }
            }
        }

        private void TakeDamage_Traits(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {           
            if (SharedPluginWrapper.enabled && self.GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ_GUARDIAN") && self.body.HasBuff(SharedPlugin.Modules.Buffs.aegisBuff))
            {
                // Shattered Aegis
                DamageInfo shatteredAegis = new DamageInfo();
                shatteredAegis.damage = self.body.damage * StaticValues.traitAegisDamageCoefficient;
                shatteredAegis.attacker = self.gameObject;
                shatteredAegis.inflictor = self.gameObject;
                shatteredAegis.force = Vector3.zero;
                shatteredAegis.crit = false;
                shatteredAegis.procCoefficient = 0f;
                shatteredAegis.damageType = DamageType.BypassArmor;

                List<HurtBox> hurtBoxes = new List<HurtBox>();
                hurtBoxes = new SphereSearch { radius = 7f, mask = LayerIndex.entityPrecise.mask, origin = self.transform.position }
                    .RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.all).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

                foreach (HurtBox hurtBox in hurtBoxes)
                {
                    if (hurtBox.healthComponent.body && hurtBox.healthComponent.body.teamComponent.teamIndex != TeamIndex.Player)
                    {
                        shatteredAegis.position = hurtBox.transform.position;

                        GlobalEventManager.instance.OnHitEnemy(shatteredAegis, hurtBox.gameObject);
                        GlobalEventManager.instance.OnHitAll(shatteredAegis, hurtBox.gameObject);
                        hurtBox.healthComponent.TakeDamage(shatteredAegis);
                    }
                }
            }

            if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Modules.Buffs.guardianJusticeBuff))
            {
                if (damageInfo.attacker.GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ_GUARDIAN"))
                {
                    // Increased Justice damage
                    damageInfo.damage *= 1.15f;
                }

                // Inflict Burning
                // damageInfo.damageType = SharedPlugin.Modules.DamageTypes;
                damageInfo.damageType = DamageType.IgniteOnHit;
            }
        
            orig(self, damageInfo);

            if (SharedPluginWrapper.enabled && self.GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ_GUARDIAN") && self.isHealthLow)
            {
                // Low Health Aegis
                self.GetComponent<TraitController>().LowHealthAegis();
            }

            bool doOnlyOnce = false;

            if (!self.alive && !doOnlyOnce && damageInfo.attacker.GetComponent<CharacterBody>().baseNameToken.StartsWith("OZZ_GUARDIAN"))
            {
                doOnlyOnce = true;

                // Renewed Justice
                damageInfo.attacker.GetComponent<TraitController>().RenewedJustice();
            }
        }
    }
}