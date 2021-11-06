using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace SharedPlugin.Modules
{
    public static class Buffs
    {
        public static BuffDef aegisBuff;
        public static BuffDef alacrityBuff;
        public static BuffDef furyBuff;
        public static BuffDef mightBuff;
        public static BuffDef protectionBuff;
        public static BuffDef quicknessBuff;
        public static BuffDef regenerationBuff;
        public static BuffDef resistanceBuff;
        public static BuffDef resolutionBuff;
        public static BuffDef stabilityBuff;
        public static BuffDef swiftnessBuff;
        public static BuffDef vigorBuff;

        public static BuffDef bleedingDebuff;
        public static BuffDef blindedDebuff;
        public static BuffDef burningDebuff;
        public static BuffDef chilledDebuff;
        public static BuffDef confusionDebuff;
        public static BuffDef crippledDebuff;
        public static BuffDef fearDebuff;
        public static BuffDef immobilizeDebuff;
        public static BuffDef poisonedDebuff;
        public static BuffDef slowDebuff;
        public static BuffDef tauntDebuff;
        public static BuffDef tormentDebuff;
        public static BuffDef vulnerabilityDebuff;
        public static BuffDef weaknessDebuff;

        internal static List<BuffDef> buffDefs = new List<BuffDef>();

        internal static void RegisterBuffs()
        {
            // Boons
            aegisBuff = AddNewBuff("GW2AegisBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonAegis"), Color.white, true, false);
            alacrityBuff = AddNewBuff("GW2AlacrityBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonAlacrity"), Color.white, true, false);
            furyBuff = AddNewBuff("GW2FuryBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonFury"), Color.white, false, false);
            mightBuff = AddNewBuff("GW2MightBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonMight"), Color.white, true, false);
            protectionBuff = AddNewBuff("GW2ProtectionBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonProtection"), Color.white, false, false);
            quicknessBuff = AddNewBuff("GW2QuicknessBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonQuickness"), Color.white, false, false);
            regenerationBuff = AddNewBuff("GW2RegenerationBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonRegeneration"), Color.white, false, false);
            resistanceBuff = AddNewBuff("GW2ResistanceBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonResistance"), Color.white, false, false);
            resolutionBuff = AddNewBuff("GW2ResolutionBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonResolution"), Color.white, false, false);
            stabilityBuff = AddNewBuff("GW2StabilityBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonStability"), Color.white, false, false);
            swiftnessBuff = AddNewBuff("GW2SwiftnessBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonSwiftness"), Color.white, false, false);
            vigorBuff = AddNewBuff("GW2VigorBuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texBoonVigor"), Color.white, false, false);

            // Conditions
            bleedingDebuff = AddNewBuff("GW2BleedingDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionBleed"), Color.white, false, true);
            blindedDebuff = AddNewBuff("GW2BlindedDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionBlind"), Color.white, false, true);
            burningDebuff = AddNewBuff("GW2BurningDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionBurn"), Color.white, false, true);
            chilledDebuff = AddNewBuff("GW2ChilledDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionChill"), Color.white, false, true);
            confusionDebuff = AddNewBuff("GW2ConfusionDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionConfusion"), Color.white, false, true);
            crippledDebuff = AddNewBuff("GW2CrippledDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionCripple"), Color.white, false, true);
            fearDebuff = AddNewBuff("GW2FearDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionFear"), Color.white, false, true);
            immobilizeDebuff = AddNewBuff("GW2ImmobilizeDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionImmobile"), Color.white, false, true);
            poisonedDebuff = AddNewBuff("GW2PoisonedDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionPoison"), Color.white, false, true);
            slowDebuff = AddNewBuff("GW2SlowDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionSlow"), Color.white, true, true);
            tauntDebuff = AddNewBuff("GW2TauntDebuff", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texConditionTaunt"), Color.white, true, true);
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