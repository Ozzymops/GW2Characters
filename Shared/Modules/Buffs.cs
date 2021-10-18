using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace SharedPlugin.Modules
{
    public static class Buffs
    {
        public static BuffDef aegisBuff;
        public static BuffDef furyBuff;
        public static BuffDef mightBuff;
        public static BuffDef protectionBuff;
        public static BuffDef regenerationBuff;
        public static BuffDef swiftnessBuff;

        public static BuffDef chillDebuff;
        public static BuffDef crippleDebuff;
        public static BuffDef tormentDebuff;
        public static BuffDef vulnerabilityDebuff;
        public static BuffDef weaknessDebuff;

        internal static List<BuffDef> buffDefs = new List<BuffDef>();

        internal static void RegisterBuffs()
        {
            // Boons
            aegisBuff = AddNewBuff("GW2AegisBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonAegis"), Color.white, true, false);
            furyBuff = AddNewBuff("GW2FuryBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonFury"), Color.white, false, false);
            mightBuff = AddNewBuff("GW2MightBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonMight"), Color.white, true, false);
            protectionBuff = AddNewBuff("GW2ProtectionBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonProtection"), Color.white, false, false);
            regenerationBuff = AddNewBuff("GW2RegenerationBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonRegeneration"), Color.white, false, false);
            swiftnessBuff = AddNewBuff("GW2SwiftnessBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonSwiftness"), Color.white, false, false);

            // Conditions
            chillDebuff = AddNewBuff("GW2ChillDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionChill"), Color.white, false, true);
            crippleDebuff = AddNewBuff("GW2CrippleDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionCripple"), Color.white, false, true);
            tormentDebuff = AddNewBuff("GW2TormentDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionTorment"), Color.white, true, true);
            vulnerabilityDebuff = AddNewBuff("GW2VulnerabilityDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionVulnerability"), Color.white, true, true);
            weaknessDebuff = AddNewBuff("GW2WeaknessDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionWeakness"), Color.white, false, true);
        }

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

        public static void HandleTimedBuffs(BuffDef buffDef, CharacterBody body, int maxStacks)
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