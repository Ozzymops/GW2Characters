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

            string desc = "Favors Melee * Supports Allies * Defensive Magic" + Environment.NewLine;
            desc = desc + "The Guardian is a very durable fighter, capable of staying in the fight for extended periods of time. Their magic in the form of Virtues allow them to mend, protect and defeat.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Virtue of Justice: Justice comes to all. Spread the burning passion of Justice by attacking your foes, punishing the unjust for their sins. When activated, the next attack will engulf your target in flames, making sure that they will not escape the flames of Justice." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Virtue of Resolve: A Guardian’s Resolve is not to be tested. Continuously mend your wounds as the battle rages on. When activated, unleash a burst of restorative magic, mending your and your allies’ wounds and removing any debilitating factors, such as poisons." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Virtue of Courage: A Guardian is most of all Courageous. Subconsciously block attacks as a vision appears before you, informing you of your enemies’ next moves. When activated, protect you and your allies from any attacks for the next three seconds." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so he left, having banished the evil from this planet.";
            string outroFailure = "..and so he vanished, his flames doused, his resolve broken, his courage forever forgotten.";

            LanguageAPI.Add(prefix + "NAME", "Guardian");
            LanguageAPI.Add(prefix + "DESCRIPTION", desc);
            LanguageAPI.Add(prefix + "SUBTITLE", "Selfless Defender");
            LanguageAPI.Add(prefix + "LORE", "sample lore");
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Core");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Dragonhunter");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Firebrand");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Willbender");
            #endregion

            #region Passive
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Justice: inflict <style=cIsDamage>burning</style> every three attacks. Active: inflict <style=cIsDamage>five stacks of burning</style> on the next attack.\n" +
                                     "Resolve: <style=cIsHealing>regenerate 1% health</style> every two seconds. Active: <style=cIsHealing>heal yourself and allies for 10% health</style> and <style=cIsHealing>cleanse two debuffs</style>.\n" +
                                     "Courage: gain <style=cIsUtility>Aegis</style> every ten seconds, negating one attack. Active: gain <style=cIsUtility>Aegis</style> every second for three seconds, negating up to three attacks.\n" +
                                     "< ! > Passive effects of virtues are lost temporarily after activation.");
            #endregion

            #region Primary
            LanguageAPI.Add(prefix + "PRIMARY_SLASH_NAME", "Mace");
            LanguageAPI.Add(prefix + "PRIMARY_SLASH_DESCRIPTION", Helpers.agilePrefix + $"Attack chain only progresses on succesful hits. Final attack in chain always progresses.\n" +
                                                                                        $"True Strike: deal <style=cIsDamage>{80f * StaticValues.maceDamageCoefficient}% damage</style>, cleaving up to three enemies.\n" +
                                                                                        $"Pure Strike: deal <style=cIsDamage>{100f * StaticValues.maceDamageCoefficient}% damage</style>, cleaving up to three enemies.\n" +
                                                                                        $"Faithful Strike: deal <style=cIsDamage>{155f * StaticValues.maceDamageCoefficient}% damage</style>, cleaving up to three enemies, and <style=cIsHealing>heal yourself and nearby allies for 1% health</style>.");
            #endregion

            #region Primary chain
            LanguageAPI.Add(prefix + "PRIMARY_MACE_ONE_NAME", "True Strike");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_ONE_DESCRIPTION", Helpers.agilePrefix + $"Smash your foe for <style=cIsDamage>{80f * StaticValues.maceDamageCoefficient}% damage. Chains into <style=cIsUtility>Pure Strike</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_TWO_NAME", "Pure Strike");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_TWO_DESCRIPTION", Helpers.agilePrefix + $"Bash your foe for <style=cIsDamage>{100f * StaticValues.maceDamageCoefficient}% damage. Chains into <style=cIsUtility>Faithful Strike</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_THREE_NAME", "Faithful Strike");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_THREE_DESCRIPTION", Helpers.agilePrefix + $"Hit your foe with a final strike for <style=cIsDamage>{155f * StaticValues.maceDamageCoefficient}% damage and <style=cIsHealing>heal yourself and nearby allies for 1% health</style>.");
            #endregion

            #region Secondary
            LanguageAPI.Add(prefix + "SECONDARY_GUN_NAME", "True Shot");
            LanguageAPI.Add(prefix + "SECONDARY_GUN_DESCRIPTION", $"Charge up a powerful spirit-bowshot, dealing between <style=cIsDamage>{250f * StaticValues.bowDamageCoefficient}% and {2500f * StaticValues.bowDamageCoefficient}% damage depending on charge</style>. This attack <style=cIsUtility>pierces</style>.");
            #endregion

            #region Utility
            LanguageAPI.Add(prefix + "UTILITY_ROLL_NAME", "Wings of Resolve");
            LanguageAPI.Add(prefix + "UTILITY_ROLL_DESCRIPTION", $"Sprout <style=cIsUtility>two angelic wings and leap up in the air</style>. While winged, <style=cIsUtility>receive 50% less damage</style>. Upon landing, lose your wings and deal <style=cIsDamage>{200f * StaticValues.wingsDamageCoefficient}% damage</style> around you and <style=cIsHealing>heal yourself and allies for 5% health</style>.");
            #endregion

            #region Special
            LanguageAPI.Add(prefix + "SPECIAL_BOMB_NAME", "Sanctuary");
            LanguageAPI.Add(prefix + "SPECIAL_BOMB_DESCRIPTION", $"Form <style=cIsUtility>a large protective barrier, protecting any inside from projectiles</style>. The barrier <style=cIsUtility>pulses 10 times</style>. Each pulse <style=cIsHealing>heals yourself and allies for 5% health</style>, and <style=cIsDamage>damages foes for {75f * StaticValues.shieldDamageCoefficient}% damage</style>, inflicting <style=cIsDamage>burning</style>.");
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