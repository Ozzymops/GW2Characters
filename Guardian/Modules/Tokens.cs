using R2API;
using System;

namespace GuardianPlugin.Modules
{
    internal static class Tokens
    {
        internal static void AddTokens()
        {
            #region Shared
            string prefix = GuardianPlugin.developerPrefix + "_GUARDIAN_BODY_";

            string outro = "...and so he left, having banished the evil from this planet.";
            string outroFailure = "...and so he vanished, his flames doused, his resolve broken, his courage forever forgotten.";

            #region Skins
            LanguageAPI.Add(prefix + "CORE_SKIN_NAME", "Core");
            LanguageAPI.Add(prefix + "DRAGONHUNTER_SKIN_NAME", "Dragonhunter");
            LanguageAPI.Add(prefix + "FIREBRAND_SKIN_NAME", "Firebrand");
            #endregion

            #region Achievements
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Guardian: Mastery");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESC", "As Guardian, beat the game or obliterate on Monsoon. Unlock Dragonhunter.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Guardian: Mastery");

            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE2_ACHIEVEMENT_NAME", "Dragonhunter: Mastery");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE2_ACHIEVEMENT_DESC", "As Dragonhunter, beat the game or obliterate on Monsoon. Unlock Firebrand.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE2_UNLOCKABLE_NAME", "Dragonhunter: Mastery");
            #endregion

            #endregion

            #region Guardian
            string nameCore = "Guardian";
            string subtitleCore = "Selfless Defender";

            string descCore = "The Guardian is a very durable fighter, capable of staying in the fight for extended periods of time. Their magic in the form of Virtues allow them to mend, protect and defeat.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            descCore = descCore + "< ! > Justice comes to all. Spread the burning passion of Justice by attacking your foes, punishing the unjust for their sins with the Virtue of Justice." + Environment.NewLine + Environment.NewLine;
            descCore = descCore + "< ! > A Guardian’s Resolve is not to be tested. Continuously mend your wounds as the battle rages on with the Virtue of Resolve." + Environment.NewLine + Environment.NewLine;
            descCore = descCore + "< ! > A Guardian is most of all Courageous. Subconsciously block attacks as a vision appears before you, informing you of your enemies’ next moves with the Virtue of Courage." + Environment.NewLine + Environment.NewLine;
            descCore = descCore + "< ! > Passive effects of Virtues are temporarily lost on activation." + Environment.NewLine + Environment.NewLine;
            descCore = descCore + "< ! > The Dragonhunter is a formidable long range skirmisher, using a longbow and light-based traps to inflict massive damage, trading in passive Virtues for Projected Virtues that allow their Virtues to actively interact with the world, and foes." + Environment.NewLine + Environment.NewLine;
            descCore = descCore + "< ! > The Firebrand is a powerful frontliner, using an axe and mantras to support and defeat, trading in Virtues for Tomes that allow for even more power, support and mitigation." + Environment.NewLine + Environment.NewLine;

            string loreCore = "As the frontline is broken, a man clad in silver armor walks up, projecting a massive bubble from his shield." +
                              "\n''Rally to me!'' he yells to his broken brethren in arms. ''I can protect you!''" +
                              "\n\nA rain of arrows falls down on the Ascalonians as they are slaughtered by the Charr, one by one. The wounded flock to the Guardian, desperate for survival." +
                              "\nThe Guardian's hope never faltered. He can protect them, he can defeat the Charr. But he must bring his brethren to safety first." +
                              "\n\nHe retreats, his bubble protecting him and the ones underneath from the rain of death. A few of his brethren are being carried by him." +
                              "\nThe Guardian flashes, using his Resolve to mend the wounded, keeping them atleast from bleeding out. Some are inspired to fight, and sacrifice their life to guarantee the Guardian's retreat." +
                              "\n\nIt was all in vain. The final attack from the Charr was unexpected. Magical, burning crystals rain down from the sky, causing massive widespread destruction, wiping out Ascalon in mere minutes." +
                              "\nThe shield bubble sustains, but even the Guardian's power is limited. As he finally escorted his brethren to safety, his shield bubble flickers." +
                              "\n\nA final flash is seen, and the Guardian is no more." +
                              "\n\nAfter what seems like an eternity passed, the Guardian awakens in a strange world. All he knows is that he must protect, and if necessary, defeat.";

            LanguageAPI.Add(prefix + "NAME", nameCore);
            LanguageAPI.Add(prefix + "DESCRIPTION", descCore);
            LanguageAPI.Add(prefix + "SUBTITLE", subtitleCore);
            LanguageAPI.Add(prefix + "LORE", loreCore);
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skills
            LanguageAPI.Add(prefix + "PASSIVE_NAME_CORE", "Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION_CORE", "Virtue of Justice: inflict <style=cIsDamage>burning</style> on the next five attacks.\n" +
                                                                 "Virtue of Resolve: <style=cIsHealing>heal yourself and nearby allies for 10% health, removing two debuffs</style>.\n" +
                                                                 "Virtue of Courage: gain <style=cIsHealing>Aegis</style> every second for three seconds.");
            // Keywords for virtues!
            LanguageAPI.Add(prefix + "KEYWORD_JUSTICE", "Justice: inflict <style=cIsDamage>burning</style> every three attacks.");
            LanguageAPI.Add(prefix + "KEYWORD_RESOLVE", "Resolve: <style=cIsHealing>regenerate 1% health</style> every two seconds.");
            LanguageAPI.Add(prefix + "KEYWORD_COURAGE", "Courage: gain <style=cIsUtility>Aegis</style> every ten seconds.");

            LanguageAPI.Add(prefix + "PRIMARY_MACECHAIN_NAME", "Mace Chain");
            LanguageAPI.Add(prefix + "PRIMARY_MACECHAIN_DESCRIPTION", Helpers.agilePrefix + $"Attack chain only progresses on succesful hits. Final attack in chain always progresses.\n" +
                                                                                        $"True Strike: deal <style=cIsDamage>{100f * StaticValues.maceDamageCoefficient}% damage</style>.\n" +
                                                                                        $"Pure Strike: deal <style=cIsDamage>{150f * StaticValues.maceDamageCoefficient}% damage</style>.\n" +
                                                                                        $"Faithful Strike: deal <style=cIsDamage>{250f * StaticValues.maceDamageCoefficient}% damage</style> and <style=cIsHealing>heal yourself and nearby allies for 1% health</style>.");

            LanguageAPI.Add(prefix + "PRIMARY_MACE_ONE_NAME", "True Strike");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_ONE_DESCRIPTION", Helpers.agilePrefix + $"Smash your foe for <style=cIsDamage>{80f * StaticValues.maceDamageCoefficient}% damage</style>. Chains into <style=cIsUtility>Pure Strike</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_TWO_NAME", "Pure Strike");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_TWO_DESCRIPTION", Helpers.agilePrefix + $"Bash your foe for <style=cIsDamage>{100f * StaticValues.maceDamageCoefficient}% damage</style>. Chains into <style=cIsUtility>Faithful Strike</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_THREE_NAME", "Faithful Strike");
            LanguageAPI.Add(prefix + "PRIMARY_MACE_THREE_DESCRIPTION", Helpers.agilePrefix + $"Hit your foe with a final strike for <style=cIsDamage>{155f * StaticValues.maceDamageCoefficient}% damage</style> and <style=cIsHealing>heal yourself and nearby allies for 1% health</style>.");

            LanguageAPI.Add(prefix + "SECONDARY_ZEALOTSDEFENSE_NAME", "Zealot's Defense");
            LanguageAPI.Add(prefix + "SECONDARY_ZEALOTSDEFENSE_DESCRIPTION", $"Slash in the air, <style=cIsUtility>destroying enemy projectiles</style> while casting eight magical projectiles for <style=cIsDamage>{100f * StaticValues.zealotsDefenseDamageCoefficient}% damage</style>.");

            LanguageAPI.Add(prefix + "UTILITY_SHIELD_NAME", "Shield of Absorption");
            LanguageAPI.Add(prefix + "UTILITY_SHIELD_DESCRIPTION", $"Create a dome around you, <style=cIsUtility>blocking attacks for five seconds</style>. Grant allies <style=cIsUtility>Aegis</style> while inside your dome.");

            LanguageAPI.Add(prefix + "SPECIAL_SHIELD_NAME", "Sanctuary");
            LanguageAPI.Add(prefix + "SPECIAL_SHIELD_DESCRIPTION", $"Form a large protective barrier, <style=cIsUtility>protecting any inside from projectiles</style>. The barrier <style=cIsUtility>pulses 10 times</style>. Each pulse <style=cIsHealing>heals yourself and allies for 5% health</style>, and <style=cIsDamage>damages foes for {75f * StaticValues.shieldDamageCoefficient}% damage</style>, inflicting <style=cIsDamage>burning</style>.");
            #endregion

            #endregion

            #region Dragonhunter
            string nameDragonhunter = "Dragonhunter";
            string subtitleDragonhunter = "Destroyer of Dragons";

            string descDragonhunter = "Favors Melee * Supports Allies * Defensive Magic" + Environment.NewLine;
            descDragonhunter = descDragonhunter + "The Guardian is a very durable fighter, capable of staying in the fight for extended periods of time. Their magic in the form of Virtues allow them to mend, protect and defeat.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            descDragonhunter = descDragonhunter + "< ! > Virtue of Justice: Justice comes to all. Spread the burning passion of Justice by attacking your foes, punishing the unjust for their sins. When activated, the next attack will engulf your target in flames, making sure that they will not escape the flames of Justice." + Environment.NewLine + Environment.NewLine;
            descDragonhunter = descDragonhunter + "< ! > Virtue of Resolve: A Guardian’s Resolve is not to be tested. Continuously mend your wounds as the battle rages on. When activated, unleash a burst of restorative magic, mending your and your allies’ wounds and removing any debilitating factors, such as poisons." + Environment.NewLine + Environment.NewLine;
            descDragonhunter = descDragonhunter + "< ! > Virtue of Courage: A Guardian is most of all Courageous. Subconsciously block attacks as a vision appears before you, informing you of your enemies’ next moves. When activated, protect you and your allies from any attacks for the next three seconds." + Environment.NewLine + Environment.NewLine;
            descDragonhunter = descDragonhunter + "< ! > Passive effects of Virtues are temporarily lost on activation." + Environment.NewLine + Environment.NewLine;

            string loreDragonhunter = "blah blah";

            #region Skills
            LanguageAPI.Add(prefix + "PASSIVE_NAME_DRAGONHUNTER", "Projected Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION_DRAGONHUNTER", "Spear of Justice: inflict <style=cIsDamage>burning</style> every three attacks. When activated, inflict <style=cIsDamage>5 * burning</style> on next attack.\n" +
                                                                 "Wings of Resolve: <style=cIsHealing>regenerate 1% health</style> every two seconds. When activated, <style=cIsHealing>heal yourself and nearby allies for 10% health, removing two debuffs</style>.\n" +
                                                                 "Shield of Courage: gain <style=cIsUtility>Aegis</style> every ten seconds. When activated, gain <style=cIsHealing>Aegis</style> every second for three seconds.");

            LanguageAPI.Add(prefix + "PRIMARY_LONGBOW_NAME", "Puncture Shot");
            LanguageAPI.Add(prefix + "PRIMARY_LONGBOW_DESCRIPTION", Helpers.agilePrefix + $"Fire an arrow that <style=cIsUtility>pierces</style> enemies, dealing <style=cIsDamage>{250f * StaticValues.bowDamageCoefficient}% damage</style>. If the arrow hits a second target, all struck enemies are hit for an extra <style=cIsDamage>{75f * StaticValues.bowDamageCoefficient}% damage</style>.");

            LanguageAPI.Add(prefix + "SECONDARY_LONGBOW_NAME", "True Shot");
            LanguageAPI.Add(prefix + "SECONDARY_LONGBOW_DESCRIPTION", $"Charge up a powerful bowshot, dealing up to <style=cIsDamage>{250f * StaticValues.trueShotDamageCoefficient}% - {2500f * StaticValues.trueShotDamageCoefficient}% damage depending on charge</style>. This attack <style=cIsUtility>pierces</style>.");

            LanguageAPI.Add(prefix + "UTILITY_TRAPS_NAME", "Test of Faith, Procession of Blades");
            LanguageAPI.Add(prefix + "UTILITY_TRAPS_DESCRIPTION", $"<style=cIsUtility>Lay down two traps that synergize with each other</style>.\n" +
                                                                  $"<style=cIsUtility>Test of Faith</style> punishes enemies trying to cross the trap's treshold, dealing <style=cIsDamage>{300f * StaticValues.trapsDamageCoefficient}% damage on crossing</style>.\n" +
                                                                  $"<style=cIsUtility>Procession of Blades</style> swirls within, dealing <style=cIsDamage>{150f * StaticValues.trapsDamageCoefficient}% damage every second</style>.");

            LanguageAPI.Add(prefix + "SPECIAL_TRAP_NAME", "Dragon's Maw");
            LanguageAPI.Add(prefix + "SPECIAL_TRAP_DESCRIPTION", $"Lay a trap that <style=cIsUtility>pulls enemies in and creates a barrier that holds them in</style>, dealing <style=cIsDamage>{500f * StaticValues.dragonsMawDamageCoefficient}% damage</style>.");
            #endregion

            #endregion

            #region Firebrand
            #endregion
        }
    }
}