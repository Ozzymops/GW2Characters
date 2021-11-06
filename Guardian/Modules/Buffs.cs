using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace GuardianPlugin.Modules
{
    public static class Buffs
    {
        internal static BuffDef guardianJusticeBuff;
        internal static BuffDef guardianShieldOfAbsorptionBuff;

        internal static List<BuffDef> buffDefs = new List<BuffDef>();

        internal static void RegisterBuffs()
        {
            guardianJusticeBuff = AddNewBuff("GW2GuardianJusticeBuff", Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texJusticeCore"), Color.white, true, false);
            guardianShieldOfAbsorptionBuff = AddNewBuff("GW2GuardianShieldOfAbsorptionBuff", Modules.Assets.subAssetBundle.LoadAsset<Sprite>("texUtilityCore"), Color.white, false, false);
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