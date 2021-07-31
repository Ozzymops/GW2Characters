using R2API;
using System;

namespace GuardianPlugin.Modules
{
    internal static class Tokens
    {
        internal static void AddTokens()
        {
            #region Guardian
            string prefix = GuardianPlugin.developerPrefix + "_GUARDIAN_BODY_";

            string desc = "Guardians are devoted fighters who protect their allies and smite their enemies by drawing from the power of their virtues.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Guardians use the virtues of Justice, Resolve and Courage. Justice inflicts burning on attacks, Resolve heals over time and Courage grants immunity to damage." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Mace is a powerful supportive attack chain, capable of healing those around him whilst dealing damage." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > True Shot is a powerful spirit-bowshot, dealing more damage the longer it is charged." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Wings of Resolve allows repositioning whilst supporting allies." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Sanctuary protects any and all from projectiles outside, whilst healing allies and dealing damage to foes within." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so he left, in search of a greater cause to protect.";
            string outroFailure = "..and so he vanished, his valor forever forgotten.";

            LanguageAPI.Add(prefix + "NAME", "Guardian");
            LanguageAPI.Add(prefix + "DESCRIPTION", desc);
            LanguageAPI.Add(prefix + "SUBTITLE", "Protector of the Meek");
            LanguageAPI.Add(prefix + "LORE", "sample lore");
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Core");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Dragonhunter");
            #endregion

            #region Passive
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Justice: inflict <style=cIsDamage>burning</style> every three attacks. Active: inflict <style=cIsDamage>five stacks of burning</style> on the next attack.\n" +
                                     "Resolve: regain 2% health every five seconds. Active: heal yourself for <style=cIsHealing>10% health</style> and allies for <style=cIsHealing>25% health</style>.\n" +
                                     "Courage: gain <style=cIsUtility>Aegis</style> every ten seconds. Active: gain three stacks of <style=cIsUtility>Aegis</style>, granting one stack to nearby allies.\n" +
                                     "Passive effects of virtues are lost temporarily after activation.");
            #endregion

            #region Primary
            LanguageAPI.Add(prefix + "PRIMARY_SLASH_NAME", "Mace");
            LanguageAPI.Add(prefix + "PRIMARY_SLASH_DESCRIPTION", Helpers.agilePrefix + $"Attack chain only progresses on succesful hits.\n" +
                                                                                        $"True Strike: swing your mace for <style=cIsDamage>{100f * StaticValues.swordDamageCoefficient}% damage</style>.\n" +
                                                                                        $"Pure Strike: swing your mace for <style=cIsDamage>{100f * StaticValues.swordDamageCoefficient}% damage</style>.\n" +
                                                                                        $"Faithful Strike: swing your mace downwards, <style=cIsHealing>healing</style> nearby allies and damaging foes for <style=cIsDamage>{100f * StaticValues.swordDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondary
            LanguageAPI.Add(prefix + "SECONDARY_GUN_NAME", "True Shot");
            LanguageAPI.Add(prefix + "SECONDARY_GUN_DESCRIPTION", $"Charge up a powerful spirit-bowshot for <style=cIsDamage>{100f * StaticValues.gunDamageCoefficient}% damage</style>, depending on charge.");
            #endregion

            #region Utility
            LanguageAPI.Add(prefix + "UTILITY_ROLL_NAME", "Wings of Resolve");
            LanguageAPI.Add(prefix + "UTILITY_ROLL_DESCRIPTION", "Leap up in the air. When landing, deal <style=cIsDamage>200% damage</style> around you and heal yourself and allies for <style=cIsHealing>5% health</style>.");
            #endregion

            #region Special
            LanguageAPI.Add(prefix + "SPECIAL_BOMB_NAME", "Sanctuary");
            LanguageAPI.Add(prefix + "SPECIAL_BOMB_DESCRIPTION", $"Form a large protective barrier, protecting everything inside from projectiles. The barrier pulses 10 times. Each pulse heals yourself and allies for <style=cIsHealing>5% health</style>, and damages foes for <style=cIsDamage>{100f * StaticValues.bombDamageCoefficient}% damage</style>, inflicting <style=cIsDamage>burning</style>.");
            #endregion

            #region Achievements
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Guardian: Mastery");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESC", "As Guardian, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Guardian: Mastery");
            #endregion
            #endregion
        }
    }
}