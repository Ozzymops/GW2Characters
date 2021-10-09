using R2API;
using System;

namespace GuardianPlugin.Modules
{
    internal static class Tokens
    {
        internal static void AddTokens()
        {
            string prefix = GuardianPlugin.developerPrefix + "_GUARDIAN_BODY_";

            #region Skins
            LanguageAPI.Add(prefix + "CORE_SKIN_NAME", "Core");
            LanguageAPI.Add(prefix + "DRAGONHUNTER_SKIN_NAME", "Dragonhunter");
            LanguageAPI.Add(prefix + "FIREBRAND_SKIN_NAME", "Firebrand");
            LanguageAPI.Add(prefix + "WILLBENDER_SKIN_NAME", "Willbender");
            #endregion

            #region Achievements
            LanguageAPI.Add(prefix + "COREMASTERY_ACHIEVEMENT_NAME", "Guardian: Mastery");
            LanguageAPI.Add(prefix + "COREMASTERY_ACHIEVEMENT_DESC", "As Guardian, beat the game or obliterate on Monsoon. Unlocks Dragonhunter.");
            LanguageAPI.Add(prefix + "COREMASTERY_UNLOCKABLE_NAME", "Guardian: Mastery");

            LanguageAPI.Add(prefix + "DHMASTERY_ACHIEVEMENT_NAME", "Dragonhunter: Mastery");
            LanguageAPI.Add(prefix + "DHMASTERY_ACHIEVEMENT_DESC", "As Dragonhunter, beat the game or obliterate on Monsoon. Unlocks Firebrand.");
            LanguageAPI.Add(prefix + "DHMASTERY_UNLOCKABLE_NAME", "Dragonhunter: Mastery");

            LanguageAPI.Add(prefix + "FBMASTERY_ACHIEVEMENT_NAME", "Firebrand: Mastery");
            LanguageAPI.Add(prefix + "FBMASTERY_ACHIEVEMENT_DESC", "As Firebrand, beat the game or obliterate on Monsoon. Unlocks Willbender.");
            LanguageAPI.Add(prefix + "FBMASTERY_UNLOCKABLE_NAME", "Firebrand: Mastery");

            LanguageAPI.Add(prefix + "WBMASTERY_ACHIEVEMENT_NAME", "Willbender: Mastery");
            LanguageAPI.Add(prefix + "WBMASTERY_ACHIEVEMENT_DESC", "As Willbender, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "WBMASTERY_UNLOCKABLE_NAME", "Willbender: Mastery");
            #endregion

            #region Names
            LanguageAPI.Add(prefix + "NAME", "Guardian");
            LanguageAPI.Add(prefix + "NAME_DH", "Dragonhunter");
            LanguageAPI.Add(prefix + "NAME_FB", "Firebrand");
            LanguageAPI.Add(prefix + "NAME_WB", "Willbender");
            #endregion

            #region Subtitles
            LanguageAPI.Add(prefix + "SUBTITLE", "Selfless Defender");
            LanguageAPI.Add(prefix + "SUBTITLE_DH", "Destroyer of Dragons");
            LanguageAPI.Add(prefix + "SUBTITLE_FB", "Purger of Sins");
            LanguageAPI.Add(prefix + "SUBTITLE_WB", "Protector of the Throne");
            #endregion

            #region Descriptions
            string description = "The Guardian is a very durable fighter, capable of staying in the fight for extended periods of time. Their magic in the form of Virtues allow them to mend, protect and defeat.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            description += "< ! > Virtues grant powerful buffs, passively and actively. Justice inflicts burning, Resolve heals over time, Courage grants Aegis. Passive effects are disabled when a Virtue is on cooldown." + Environment.NewLine + Environment.NewLine;
            description += "< ! > Mace Chain is a relatively weak melee attack-chain, but allows the Guardian to sustain by healing on the final hit of the chain." + Environment.NewLine + Environment.NewLine;
            description += "< ! > Zealot's Defense destroys projectiles near the Guardian, allowing them to safely attack from range. You are still vulnerable from melee attacks, however!" + Environment.NewLine + Environment.NewLine;
            description += "< ! > Shield of Absorption blocks all projectiles and pushes enemies away, while pulsing Aegis from within to yourself and allies. Using this at the right moment might be the difference between life and death." + Environment.NewLine + Environment.NewLine;
            description += "< ! > Sanctuary blocks all projectiles and burns enemies inside, while pulsing healing from within to yourself and allies. Mend, protect and defeat!" + Environment.NewLine + Environment.NewLine;
            description += "< ! > Levels 5, 10 and 15 grant a trait: [5] Aegis shatters on hit, Aegis is granted when struck below 33% health; [10] Justice is renewed on kill; [15] Most of your skills heal on ending.";
            LanguageAPI.Add(prefix + "DESCRIPTION", description);

            description = "";
            description += "< ! > Levels 5, 10 and 15 grant a trait: [5] Deal bonus damage depending on distance to enemy; [10] Reduce recharge of all Projected Virtues by 33%; [15] Traps deal 100% increased critical damage.";
            LanguageAPI.Add(prefix + "DESCRIPTION_DH", description);

            description = "";
            description += "< ! > Levels 5, 10 and 15 grant a trait: [5] When granting a buff to yourself or an ally, gain and grant Swiftness; [10] All burning applications are doubled; [15] All Tomes gain three extra charges.";
            LanguageAPI.Add(prefix + "DESCRIPTION_FB", description);

            description = "";
            description += "< ! > Levels 5, 10 and 15 grant a trait: [5] Resolve also grants Regeneration; [10] Justice grants increased damage; [15] You cannot die when Courage is active, extend Courage on kill.";
            LanguageAPI.Add(prefix + "DESCRIPTION_WB", description);
            #endregion

            #region Lore
            string lore = "As the frontline is broken, a man clad in silver armor walks up, projecting a massive bubble from his shield." + Environment.NewLine;
            lore += "''Rally to me!'' he yells to his broken brethren in arms. ''I can protect you!''" + Environment.NewLine + Environment.NewLine;
            lore += "A rain of arrows falls down on the Ascalonians as they are slaughtered by the Charr, one by one. The wounded flock to the Guardian, desperate for survival." + Environment.NewLine;
            lore += "The Guardian's hope never faltered. He can protect them, he can defeat the Charr. But he must bring his brethren to safety first." + Environment.NewLine + Environment.NewLine;
            lore += "He retreats, his bubble protecting him and the ones underneath from the rain of death. A few of his brethren are being carried by him." + Environment.NewLine;
            lore += "The Guardian flashes, using his unending Resolve to mend the wounded, keeping them atleast from bleeding out. Some are inspired to fight, and sacrifice their life to guarantee the Guardian's retreat." + Environment.NewLine + Environment.NewLine;
            lore += "It was all in vain. The final attack from the Charr was unexpected. Magical, burning crystals rain down from the sky, causing massive widespread destruction, wiping out Ascalon in mere minutes." + Environment.NewLine;
            lore += "The shield bubble sustains, but even the Guardian's power is limited. As he finally escorted his brethren to safety, his shield bubble flickers." + Environment.NewLine + Environment.NewLine;
            lore += "A final flash is seen, and the Guardian is no more. After what seems like an eternity passed, the Guardian awakens in a strange world. All he knows is that he must protect, and if necessary, defeat.";
            LanguageAPI.Add(prefix + "LORE", description);

            lore = "";
            LanguageAPI.Add(prefix + "LORE_DH", description);

            lore = "";
            LanguageAPI.Add(prefix + "LORE_FB", description);

            lore = "";
            LanguageAPI.Add(prefix + "LORE_WB", description);
            #endregion

            #region Outro
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", "...and so he left, having banished the evil from this planet.");
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", "...and so he vanished, his flames doused, his resolve broken, his courage forever forgotten.");
            #endregion

            #region Keywords
            LanguageAPI.Add(prefix + "KEYWORD_ATTACKCHAIN", "Chain: only progresses on succesful hits. Final attack in chain always progresses.");
            LanguageAPI.Add(prefix + "KEYWORD_AEGISBUFF", "Aegis: nullifies a hit, removing the buff.");
            LanguageAPI.Add(prefix + "KEYWORD_JUSTICEBUFF", "Justice: inflicts burning on hit, removing the buff.");
            LanguageAPI.Add(prefix + "KEYWORD_DHTRAP", "Trap: will only trigger if enemy passes through trigger.");
            LanguageAPI.Add(prefix + "KEYWORD_FBMANTRA", "Mantra: has three charges.");
            LanguageAPI.Add(prefix + "KEYWORD_WBUTILITY", "Quick Retribution: lunge forward and stab your foe, stunning them.");
            LanguageAPI.Add(prefix + "KEYWORD_JUSTICE", "Justice: inflict <style=cIsDamage>burning</style> every three attacks.");
            LanguageAPI.Add(prefix + "KEYWORD_RESOLVE", "Resolve: <style=cIsHealing>regenerate 1% health</style> every two seconds.");
            LanguageAPI.Add(prefix + "KEYWORD_COURAGE", "Courage: gain <style=cIsUtility>Aegis</style> every ten seconds.");
            #endregion

            #region Passives
            // CORE
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Virtue of Justice: inflict <style=cIsDamage>burning</style> on the next five attacks.\n" +
                                                            "Virtue of Resolve: <style=cIsHealing>heal yourself and nearby allies for 10% health, removing two debuffs</style>.\n" +
                                                            "Virtue of Courage: gain <style=cIsHealing>Aegis</style> every second for three seconds.");

            // DH
            LanguageAPI.Add(prefix + "PASSIVE_NAME_DH", "Projected Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION_DH", "Spear of Justice: throw a spear of light, inflicting 100% damage and tethering yourself to enemies. Tethered enemies suffer <style=cIsDamage>burning</style> every second. Chains into <style=cIsUtility>Hunter's Verdict</style>.\n" +
                                                               "Hunter's Verdict: <style=cIsUtility>pull</style> tethered enemies to your location.\n" +
                                                               "Wings of Resolve: leap to a targeted area, healing <style=cIsHealing>10% health</style> and <style=cIsUtility>launching enemies away</style> on landing.\n" +
                                                               "Shield of Courage: project a shield in front of you, <style=cIsUtility>blocking every attack from the front</style>. You are still vulnerable from the sides and behind.");

            // FB
            LanguageAPI.Add(prefix + "PASSIVE_NAME_FB", "Tomes of Virtue");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION_FB", "Tome of Justice: read from the Tome of Justice, accessing multiple <style=cIsDamage>damaging</style> skills.\n" +
                                                               "Tome of Resolve: read from the Tome of Resolve, accessing multiple <style=cIsHealing>healing</style> skills.\n" +
                                                               "Tome of Courage: read from the Tome of Courage, accessing multiple <style=cIsUtility>supportive</style> skills.");

            // WB
            LanguageAPI.Add(prefix + "PASSIVE_NAME_WB", "Emanating Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION_WB", "Rushing Justice: <style=cIsUtility>rush forward</style>, striking your target for <style=cIsDamage>200% damage</style> and leaving a <style=cIsDamage>claw-shaped pattern of flames</style> under the target.\n" +
                                                               "Flowing Resolve: <style=cIsUtility>rush foward, evading attacks</style> while temporarily <style=cIsHealing>healing</style> from your attacks, leaving a <style=cIsDamage>line of flames</style> in your wake.\n" +
                                                               "Crashing Courage: <style=cIsUtility>teleport forward</style> while temporarily gaining <style=cIsUtility>Aegis</style> from your attacks, leaving a <style=cIsDamage>ring of flames</style> after teleporting.");

            #endregion

            #region Primaries
            // CORE
            LanguageAPI.Add(prefix + "PRIMARY_NAME", "Mace Chain");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION", $"Mace attack chain consisting of:\n" +
                                                            $"- True Strike: deal <style=cIsDamage>{100f * StaticValues.corePrimaryDamageCoefficient}% damage</style>.\n" +
                                                            $"- Pure Strike: deal <style=cIsDamage>{120f * StaticValues.corePrimaryDamageCoefficient}% damage</style>.\n" +
                                                            $"- Faithful Strike: deal <style=cIsDamage>{170f * StaticValues.corePrimaryDamageCoefficient}% damage</style> and <style=cIsHealing>heal yourself and nearby allies for 1% health</style>.");

            LanguageAPI.Add(prefix + "PRIMARY_NAME_CHAIN1", "True Strike");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_CHAIN1", $"Smash your foe for <style=cIsDamage>{100f * StaticValues.corePrimaryDamageCoefficient}% damage</style>. Chains into <style=cIsUtility>Pure Strike</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_CHAIN2", "Pure Strike");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_CHAIN2", $"Bash your foe for <style=cIsDamage>{150f * StaticValues.corePrimaryDamageCoefficient}% damage</style>. Chains into <style=cIsUtility>Faithful Strike</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_CHAIN3", "Faithful Strike");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_CHAIN3", $"Hit your foe with a final strike for <style=cIsDamage>{250f * StaticValues.corePrimaryDamageCoefficient}% damage</style> and <style=cIsHealing>heal yourself and nearby allies for 1% health</style>.");

            // DH
            LanguageAPI.Add(prefix + "PRIMARY_NAME_DH", "Puncture Shot");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_DH", $"Fire an arrow that <style=cIsUtility>pierces</style> enemies, dealing <style=cIsDamage>{250f * StaticValues.dhPrimaryDamageCoefficient}% damage</style>. If the arrow hits a second target, all struck enemies are hit for an extra <style=cIsDamage>{75f * StaticValues.dhPrimaryDamageCoefficient}% damage</style>.");

            // FB
            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB", "Axe Chain");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB", $"Axe attack chain consisting of:\n" +
                                                            $"- Core Cleave: deal <style=cIsDamage>{100f * StaticValues.fbPrimaryDamageCoefficient}% damage</style> and inflict <style=cIsDamage>bleeding</style>.\n" +
                                                            $"- Bleeding Edge: deal <style=cIsDamage>{150f * StaticValues.fbPrimaryDamageCoefficient}% damage</style> and inflict <style=cIsDamage>bleeding</style>.\n" +
                                                            $"- Searing Slash: deal <style=cIsDamage>{200f * StaticValues.fbPrimaryDamageCoefficient}% damage</style> and inflict <style=cIsDamage>burning</style>.");

            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB_CHAIN1", "Core Cleave");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB_CHAIN1", $"Cleave at your foe for <style=cIsDamage>{100f * StaticValues.fbPrimaryDamageCoefficient}% damage</style>, inflicting <style=cIsDamage>bleeding</style>. Chains into <style=cIsUtility>Bleeding Edge</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB_CHAIN2", "Bleeding Edge");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB_CHAIN2", $"Slice your foe for <style=cIsDamage>{150f * StaticValues.fbPrimaryDamageCoefficient}% damage</style>, inflicting <style=cIsDamage>bleeding</style>. Chains into <style=cIsUtility>Searing Slash</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB_CHAIN3", "Searing Slash");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB_CHAIN3", $"Unleash your searing axe in an overhand slash for <style=cIsDamage>{250f * StaticValues.fbPrimaryDamageCoefficient}% damage</style>, inflicting <style=cIsDamage>burning</style>.");

            // WB
            LanguageAPI.Add(prefix + "PRIMARY_NAME_WB", "Sword Chain");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_WB", $"Sword attack chain consisting of:\n" +
                                                            $"- Sword of Wrath: deal <style=cIsDamage>{150f * StaticValues.wbPrimaryDamageCoefficient}% damage</style>.\n" +
                                                            $"- Sword Arc: deal <style=cIsDamage>{250f * StaticValues.wbPrimaryDamageCoefficient}% damage</style>.\n" +
                                                            $"- Sword Wave: deal <style=cIsDamage>{400f * StaticValues.wbPrimaryDamageCoefficient}% damage</style>, launching three projectiles for <style=cIsDamage>{150f * StaticValues.wbPrimaryDamageCoefficient}% damage</style>.");

            LanguageAPI.Add(prefix + "PRIMARY_NAME_WB_CHAIN1", "Sword of Wrath");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_WB_CHAIN1", $"Slash your foe for <style=cIsDamage>{150f * StaticValues.wbPrimaryDamageCoefficient}% damage</style>. Chains into <style=cIsUtility>Sword Arc</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_WB_CHAIN2", "Sword Arc");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_WB_CHAIN2", $"Slash your foe again for <style=cIsDamage>{250f * StaticValues.wbPrimaryDamageCoefficient}% damage</style>. Chains into <style=cIsUtility>Sword Wave</style>.");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_WB_CHAIN3", "Sword Wave");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_WB_CHAIN3", $"Stab your foe for <style=cIsDamage>{400f * StaticValues.wbPrimaryDamageCoefficient}% damage</style>, launcing three projectiles for <style=cIsDamage>{150f * StaticValues.wbPrimaryDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondaries
            // CORE
            LanguageAPI.Add(prefix + "SECONDARY_NAME", "Zealot's Defense");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION", $"Slash in the air, <style=cIsUtility>destroying enemy projectiles</style> while casting eight magical projectiles for <style=cIsDamage>{100f * StaticValues.coreSecondaryDamageCoefficient}% damage</style>.");

            // DH
            LanguageAPI.Add(prefix + "SECONDARY_NAME_DH", "True Shot");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION_DH", $"Charge up a powerful bowshot, dealing up to <style=cIsDamage>{250f * StaticValues.dhSecondaryDamageCoefficient}% - {2500f * StaticValues.dhSecondaryDamageCoefficient}% damage depending on charge</style>. This attack <style=cIsUtility>pierces</style>.");

            // FB
            LanguageAPI.Add(prefix + "SECONDARY_NAME_FB", "Blazing Edge");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION_FB", $"Spawn a spinning magical axe, cleaving foes in its area for <style=cIsDamage>{250f * StaticValues.fbSecondaryDamageCoefficient}% damage</style> during its lifetime.");

            // WB
            LanguageAPI.Add(prefix + "SECONDARY_NAME_WB", "Executioner's Calling");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION_WB", $"Strike your foe for <style=cIsDamage>{50f * StaticValues.wbSecondaryDamageCoefficient}% damage</style>. After a successful hit, follow up with an overhead slash for <style=cIsDamage>{250f * StaticValues.wbSecondaryDamageCoefficient}% damage</style>.");

            #endregion

            #region Utilities
            // CORE
            LanguageAPI.Add(prefix + "UTILITY_NAME", "Shield of Absorption");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION", $"Create a dome around you, <style=cIsUtility>blocking attacks for five seconds</style>. Grant allies <style=cIsUtility>Aegis</style> while inside your dome.");

            // DH
            LanguageAPI.Add(prefix + "UTILITY_NAME_DH", "Test of Faith, Procession of Blades");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_DH", $"<style=cIsUtility>Lay down two traps that synergize with each other</style>.\n" +
                                                               $"<style=cIsUtility>Test of Faith</style> punishes enemies trying to cross the trap's treshold, dealing <style=cIsDamage>{300f * StaticValues.dhUtilityCoefficient}% damage on crossing</style>.\n" +
                                                               $"<style=cIsUtility>Procession of Blades</style> swirls within, dealing <style=cIsDamage>{150f * StaticValues.dhUtilityCoefficient}% damage every second</style>.");

            // FB
            LanguageAPI.Add(prefix + "UTILITY_NAME_FB", "Mantra of Solace");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_FB", $"Heal yourself and nearby allies for <style=cIsHealing>10% health</style>.");

            // WB
            LanguageAPI.Add(prefix + "UTILITY_NAME_WB", "Roiling Light");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_WB", $"<style=cIsUtility>Dodge backwards</style>. Gain access to <style=cIsUtility>Quick Retribution</style> for a short while.");

            LanguageAPI.Add(prefix + "UTILITY_NAME_WB_CHAIN", "Quick Retribution");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_WB_CHAIN", $"<style=cIsUtility>Lunge forward</style> and stab your foe for <style=cIsDamage>{250f * StaticValues.wbUtilityCoefficient}% damage</style>, <style=cIsUtility>stunning</style> them.");
            #endregion

            #region Specials
            // CORE
            LanguageAPI.Add(prefix + "SPECIAL_NAME", "Sanctuary");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION", $"Form a large protective dome, <style=cIsUtility>protecting any inside from projectiles</style>. The barrier <style=cIsUtility>pulses 10 times</style>. Each pulse <style=cIsHealing>heals yourself and allies for 5% health</style>, and <style=cIsDamage>damages foes for {75f * StaticValues.coreSpecialCoefficient}% damage</style>, inflicting <style=cIsDamage>burning</style>.");

            // DH
            LanguageAPI.Add(prefix + "SPECIAL_NAME_DH", "Dragon's Maw");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION_DH", $"Lay a trap that <style=cIsUtility>pulls enemies in and creates a barrier that holds them in</style>, dealing <style=cIsDamage>{500f * StaticValues.dhSpecialCoefficient}% damage</style>.");

            // FB
            LanguageAPI.Add(prefix + "SPECIAL_NAME_FB", "Mantra of Flame");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION_FB", $"Chant a shockwave of fire, dealing <style=cIsDamage>{150f * StaticValues.fbSpecialCoefficient}% damage</style> and <style=cIsDamage>burning</style> enemies around you.");

            // WB
            LanguageAPI.Add(prefix + "SPECIAL_NAME_WB", "Heaven's Palm");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION_WB", $"Slam the ground, dealing <style=cIsDamage>{500f * StaticValues.wbSpecialCoefficient}% damage</style> around you, <style=cIsUtility>launching nearby enemies away</style>.");
            #endregion
        }
    }
}