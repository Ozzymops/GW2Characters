using BepInEx;
using GuardianPlugin.Modules.Survivors;
using R2API.Utils;
using RoR2;
using RoR2.UI;
using System.Collections.Generic;
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
            new Guardian().Initialize();

            // now make a content pack and add it- this part will change with the next update
            new Modules.ContentPacks().Initialize();

            RoR2.ContentManagement.ContentManager.onContentPacksAssigned += LateSetup;

            Hook();
        }



        private void LateSetup(HG.ReadOnlyArray<RoR2.ContentManagement.ReadOnlyContentPack> obj)
        {
            // have to set item displays later now because they require direct object references..
            Modules.Survivors.Guardian.instance.SetItemDisplays();
        }

        private void Hook()
        {
            // run hooks here, disabling one is as simple as commenting out the line
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            On.RoR2.UI.HUD.Awake += InitializeUserInterface;
        }

        private void OnDestroy()
        {
            On.RoR2.UI.HUD.Awake -= InitializeUserInterface;
        }

        // UI
        private HUD hud = null;

        private void InitializeUserInterface(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);
            hud = self;

            if (self)
            {
                // Main container
                UnityEngine.GameObject guardianContainer = new GameObject("GuardianContainer");
                guardianContainer.transform.SetParent(hud.mainContainer.transform);
                RectTransform containerTransform = guardianContainer.AddComponent<RectTransform>();
                containerTransform.anchorMin = new Vector2(0.5f, 0.5f);
                containerTransform.anchorMax = new Vector2(0.5f, 0.5f);
                containerTransform.sizeDelta = new Vector2(200, 50);
                containerTransform.anchoredPosition = new Vector2(1000, 1000);

                // Virtue of Justice
                GameObject guardianVirtueJustice = new GameObject("VirtueJustice");
                guardianVirtueJustice.transform.SetParent(guardianContainer.transform);
                RectTransform justiceTransform = guardianVirtueJustice.AddComponent<RectTransform>();
                justiceTransform.anchorMin = new Vector2(0f, 0.5f);
                justiceTransform.anchorMax = new Vector2(0f, 0.5f);
                justiceTransform.sizeDelta = new Vector2(50, 50);
                justiceTransform.position = new Vector3(25, 0, 0);
                guardianVirtueJustice.AddComponent<Image>();
                guardianVirtueJustice.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/itemicons/texBearIcon");

                // Virtue of Resolve
                GameObject guardianVirtueResolve = new GameObject("VirtueResolve");
                guardianVirtueResolve.transform.SetParent(guardianContainer.transform);
                RectTransform resolveTransform = guardianVirtueResolve.AddComponent<RectTransform>();
                resolveTransform.anchorMin = new Vector2(0.5f, 0.5f);
                resolveTransform.anchorMax = new Vector2(0.5f, 0.5f);
                resolveTransform.sizeDelta = new Vector2(50, 50);
                resolveTransform.position = new Vector3(0, 0, 0);
                guardianVirtueResolve.AddComponent<Image>();
                guardianVirtueResolve.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/itemicons/texBearIcon");

                // Virtue of Courage
                GameObject guardianVirtueCourage = new GameObject("VirtueCourage");
                guardianVirtueCourage.transform.SetParent(guardianContainer.transform);
                RectTransform courageTransform = guardianVirtueCourage.AddComponent<RectTransform>();
                courageTransform.anchorMin = new Vector2(1f, 0.5f);
                courageTransform.anchorMax = new Vector2(1f, 0.5f);
                courageTransform.sizeDelta = new Vector2(50, 50);
                courageTransform.position = new Vector3(-25, 0, 0);
                guardianVirtueCourage.AddComponent<Image>();
                guardianVirtueCourage.GetComponent<Image>().sprite = Resources.Load<Sprite>("textures/itemicons/texBearIcon");
            }
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