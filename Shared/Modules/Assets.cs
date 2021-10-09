using System.Reflection;
using R2API;
using UnityEngine;
using UnityEngine.Networking;
using RoR2;
using System.IO;
using System.Collections.Generic;
using RoR2.UI;

namespace SharedPlugin.Modules
{
    internal static class Assets
    {
        internal static AssetBundle mainAssetBundle;
        private static string[] assetNames = new string[0];
        private const string assetbundleName = "sharedassetbundle";

        internal static void Initialize()
        {
            LoadAssetBundle();
        }

        internal static void LoadAssetBundle()
        {
            if (mainAssetBundle == null)
            {
                using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Shared." + assetbundleName))
                {                
                    mainAssetBundle = AssetBundle.LoadFromStream(assetStream);
                    Debug.LogWarning("GW2Shared - assets loaded!");
                }
            }

            assetNames = mainAssetBundle.GetAllAssetNames();
        }
    }
}