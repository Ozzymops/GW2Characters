using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianPlugin.Modules
{
    public static class Buffs
    {
        internal static BuffDef justiceBuff;
        internal static BuffDef shieldOfAbsorptionBuff;

        internal static List<BuffDef> buffDefs = new List<BuffDef>();

        internal static void RegisterBuffs()
        {
            justiceBuff = AddNewBuff("GW2JusticeBuff", Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeCore"), Color.white, true, false);
            shieldOfAbsorptionBuff = AddNewBuff("GW2ShieldOfAbsorptionBuff", Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texUtilityCore"), Color.white, false, false);
        }

        // simple helper method
        internal static BuffDef AddNewBuff(string buffName, Sprite buffIcon, Color buffColor, bool canStack, bool isDebuff)
        {
            BuffDef buffDef = ScriptableObject.CreateInstance<BuffDef>();
            buffDef.name = buffName;
            buffDef.buffColor = buffColor;
            buffDef.canStack = canStack;
            buffDef.isDebuff = isDebuff;
            buffDef.eliteDef = null;
            buffDef.iconSprite = buffIcon;

            buffDefs.Add(buffDef);

            return buffDef;
        }
    }
}