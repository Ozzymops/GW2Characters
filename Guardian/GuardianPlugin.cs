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
                // Set up main container
                GameObject guardianContainer = new GameObject("GuardianUIContainer");
                guardianContainer.transform.SetParent(hud.mainContainer.transform);
                RectTransform guardianContainerTransform = guardianContainer.AddComponent<RectTransform>();
                guardianContainerTransform.anchorMin = new Vector2(0.05f, 0.15f);
                guardianContainerTransform.anchorMax = new Vector2(0.35f, 0.35f);
                guardianContainerTransform.sizeDelta = Vector2.zero;
                guardianContainerTransform.anchoredPosition = new Vector2(0.5f, 0.5f);

                // Profession container
                GameObject professionContainer = new GameObject("GuardianUIContainer_Profession");
                professionContainer.transform.SetParent(hud.mainContainer.transform);
                RectTransform professionContainerTransform = professionContainer.AddComponent<RectTransform>();
                professionContainerTransform.anchorMin = new Vector2(0.0025f, 0.05f);
                professionContainerTransform.anchorMax = new Vector2(0.06f, 0.15f);
                professionContainerTransform.sizeDelta = Vector2.zero;
                professionContainerTransform.anchoredPosition = new Vector2(0.5f, 0.5f);

                // 'Justice' container
                GameObject justiceContainer = new GameObject("GuardianUIContainer_Justice");
                justiceContainer.transform.SetParent(guardianContainer.transform);
                RectTransform justiceContainerTransform = justiceContainer.AddComponent<RectTransform>();
                justiceContainerTransform.anchorMin = new Vector2(0.05f, 0.15f);
                justiceContainerTransform.anchorMax = new Vector2(0.15f, 0.35f);
                justiceContainerTransform.sizeDelta = Vector2.zero;
                justiceContainerTransform.anchoredPosition = new Vector2(0.5f, 0.5f);

                // 'Resolve' container
                GameObject resolveContainer = new GameObject("GuardianUIContainer_Resolve");
                resolveContainer.transform.SetParent(guardianContainer.transform);
                RectTransform resolveContainerTransform = resolveContainer.AddComponent<RectTransform>();
                resolveContainerTransform.anchorMin = new Vector2(0.15f, 0.15f);
                resolveContainerTransform.anchorMax = new Vector2(0.25f, 0.35f);
                resolveContainerTransform.sizeDelta = Vector2.zero;
                resolveContainerTransform.anchoredPosition = new Vector2(0.5f, 0.5f);

                // 'Courage' container
                GameObject courageContainer = new GameObject("GuardianUIContainer_Courage");
                courageContainer.transform.SetParent(guardianContainer.transform);
                RectTransform courageContainerTransform = courageContainer.AddComponent<RectTransform>();
                courageContainerTransform.anchorMin = new Vector2(0.25f, 0.15f);
                courageContainerTransform.anchorMax = new Vector2(0.35f, 0.35f);
                courageContainerTransform.sizeDelta = Vector2.zero;
                courageContainerTransform.anchoredPosition = new Vector2(0.5f, 0.5f);

                // Assign images
                professionContainer.AddComponent<Image>();
                justiceContainer.AddComponent<Image>();
                resolveContainer.AddComponent<Image>();
                courageContainer.AddComponent<Image>();
                professionContainer.GetComponent<Image>().sprite = Modules.Assets.subAssetBundle.LoadAsset<Sprite>("profession");
                justiceContainer.GetComponent<Image>().sprite = Modules.Assets.subAssetBundle.LoadAsset<Sprite>("skillVirtueJustice");
                resolveContainer.GetComponent<Image>().sprite = Modules.Assets.subAssetBundle.LoadAsset<Sprite>("skillVirtueResolve");
                courageContainer.GetComponent<Image>().sprite = Modules.Assets.subAssetBundle.LoadAsset<Sprite>("skillVirtueCourage");
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