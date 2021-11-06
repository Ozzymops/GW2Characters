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
            LanguageAPI.Add(prefix + "CORE_ACHIEVEMENT_NAME", "The Selfless Defender");
            LanguageAPI.Add(prefix + "CORE_ACHIEVEMENT_DESC", "As the Guardian, grant yourself and/or allies Virtue buffs 100 times.");
            LanguageAPI.Add(prefix + "CORE_UNLOCKABLE_NAME", "The Selfless Defender");

            LanguageAPI.Add(prefix + "DH_ACHIEVEMENT_NAME", "The Destroyer of Dragons");
            LanguageAPI.Add(prefix + "DH_ACHIEVEMENT_DESC", "As the Guardian elite specialisation; Dragonhunter, one-shot an enemy with over 100k damage.");
            LanguageAPI.Add(prefix + "DH_UNLOCKABLE_NAME", "The Destroyer of Dragons");

            LanguageAPI.Add(prefix + "FB_ACHIEVEMENT_NAME", "The Purger of Sins");
            LanguageAPI.Add(prefix + "FB_ACHIEVEMENT_DESC", "As the Guardian elite specialisation; Firebrand, inflict Burning on enemies 100 times.");
            LanguageAPI.Add(prefix + "FB_UNLOCKABLE_NAME", "The Purger of Sins");

            LanguageAPI.Add(prefix + "WB_ACHIEVEMENT_NAME", "The Protector of the Throne");
            LanguageAPI.Add(prefix + "WB_ACHIEVEMENT_DESC", "As the Guardian elite specialisation; Willbender, survive up to stage 20.");
            LanguageAPI.Add(prefix + "WB_UNLOCKABLE_NAME", "The Protector of the Throne");
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

            #region Description
            string description = "The Guardian is a very durable fighter, capable of staying in the fight for extended periods of time thanks to his Virtues, granting him Justice to deal damage, Resolve to regenerate health and Courage to mitigate attacks.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            description += "< ! > Levels 5, 10 and 15 grant a Trait, improving a certain aspect of your character." + Environment.NewLine + Environment.NewLine;
            description += "< ! > The Core Guardian uses a mace and shield to both deal damage and support allies. His Traits improve Aegis, restore Justice upon killing a foe and allow his skills to heal when ending." + Environment.NewLine + Environment.NewLine;
            description += "< ! > The Dragonhunter uses a longbow and traps to deal massive damage, trading his Virtues for Projected Virtues. His Traits improve long-range damage, reduce recharge times of his Projected Virtues and greatly improve traps." + Environment.NewLine + Environment.NewLine;
            description += "< ! > The Firebrand uses an axe and mantras to support allies, trading his Virtues for Tomes. His Traits make him grant Swiftness on granting a buff, double his Burning duration and increase his Burning damage and make him gain three extra Tome charges." + Environment.NewLine + Environment.NewLine;
            description += "< ! > The Willbender dual wields swords and uses martial-arts, trading his Virtues for Emanating Virtues. His Traits make him improve his Flowing Resolve, Rushing Justice and Crashing Courage.";
            LanguageAPI.Add(prefix + "DESCRIPTION", description);
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
            #endregion

            #region Outro
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", "...and so he left, having banished the evil from this planet.");
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", "...and so he vanished, his flames doused, his resolve broken, his courage forever forgotten.");
            #endregion

            #region Keywords
            LanguageAPI.Add(prefix + "KEYWORD_ATTACKCHAIN", "<style=cKeywordName>Attack Chain</style><style=cSub>Progresses primary skill on successful primary skill hits. Final attack in chain always reverts back to first primary skill in chain.</style>");
            LanguageAPI.Add(prefix + "KEYWORD_DHTRAP", "<style=cKeywordName>Trap</style><style=cSub>Will only trigger if enemy enters its radius.</style>");
            LanguageAPI.Add(prefix + "KEYWORD_WBUTILITY", $"<style=cKeywordName>Quick Retribution</style><style=cSub><style=cIsUtility>Lunge forward</style> and stab your foe for <style=cIsDamage>{StaticValues.wbUtilityDamage}%</style> damage, <style=cIsUtility>stunning them.</style></style>");
            LanguageAPI.Add(prefix + "KEYWORD_JUSTICE", $"<style=cKeywordName>Justice</style><style=cSub>Inflict <style=cIsDamage>Burning</style> every {StaticValues.virtuePassiveJusticeHitsNeeded} attacks.</style>");
            LanguageAPI.Add(prefix + "KEYWORD_RESOLVE", $"<style=cKeywordName>Resolve</style><style=cSub><style=cIsHealing>Regenerate {StaticValues.virtuePassiveResolveHeal}% health</style> every {StaticValues.virtuePassiveResolveInterval} seconds.</style>");
            LanguageAPI.Add(prefix + "KEYWORD_COURAGE", $"<style=cKeywordName>Courage</style><style=cSub>Gain <style=cIsUtility>Aegis</style> every {StaticValues.virtuePassiveCourageInterval} seconds.");
            #endregion

            #region Passives
            // CORE
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "<style=cUserSetting>Automatically swaps loadout to Core.</style>\n" +
                                                            $"<style=cSub>Virtue of Justice</style>: gain {StaticValues.virtueCoreActivatedJusticeStacks} stacks of Justice, inflicting <style=cIsDamage>Burning</style> on hit.\n" +
                                                            $"<style=cSub>Virtue of Resolve</style>: regenerate <style=cIsHealing>{StaticValues.virtueCoreActivatedResolveHeal}% health</style> of your and your allies' health and <style=cIsHealing>remove two debuffs</style>.\n" +
                                                            $"<style=cSub>Virtue of Courage</style>: gain {StaticValues.virtueCoreActivatedCourageTicks} stacks of <style=cIsUtility>Aegis</style> over {StaticValues.virtueCoreActivatedCourageTicks * StaticValues.virtueCoreActivatedCourageTimer} seconds.");

            // DH
            LanguageAPI.Add(prefix + "PASSIVE_NAME_DH", "Projected Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION_DH", "<style=cUserSetting>Automatically swaps loadout to Dragonhunter.</style>\n" +
                                                               $"<style=cSub>Spear of Justice</style>: throw a spear of light, inflicting <style=cIsDamage>{StaticValues.virtueDHSpearDamage}%</style> damage and tethering yourself to enemies, <style=cIsDamage>Burning</style> them every second. Chains into <style=cIsUtility>Hunter's Verdict</style>.\n" +
                                                               "<style=cSub>Hunter's Verdict</style>: <style=cIsUtility>pull</style> tethered enemies to your location.\n" +
                                                               $"<style=cSub>Wings of Resolve</style>: <style=cIsUtility>leap forward</style>, regenerating <style=cIsHealing>{StaticValues.virtueDHWingsHeal}% health</style> and <style=cIsUtility>launching nearby enemies away</style> on landing.\n" +
                                                               $"<style=cSub>Shield of Courage</style>: project a shield in front of you, <style=cIsUtility>blocking every attack from the front</style> for {StaticValues.virtueDHShieldDuration} seconds. <style=cIsHealth>You are still vulnerable from the sides and behind</style>.");

            // FB
            LanguageAPI.Add(prefix + "PASSIVE_NAME_FB", "Tomes of Virtue");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION_FB", "<style=cUserSetting>Automatically swaps loadout to Firebrand.</style>\n" + 
                                                               "<style=cSub>Tome of Justice</style>: read from the Tome of Justice, accessing multiple <style=cIsDamage>damaging</style> skills.\n" +
                                                               "<style=cSub>Tome of Resolve</style>: read from the Tome of Resolve, accessing multiple <style=cIsHealing>healing</style> skills.\n" +
                                                               "<style=cSub>Tome of Courage</style>: read from the Tome of Courage, accessing multiple <style=cIsUtility>supportive</style> skills.");

            #region Tomes
            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB_JUSTICETOME", "Chapter 1, Searing Spell");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB_JUSTICETOME", $"Fueled by tales of the desolation in Istan, incite a great swelling of heat before you, dealing <style=cIsDamage>{StaticValues.virtueFBJusticePrimaryDamage}%</style> damage and inflicting <style=cIsDamage>Burning</style>.");

            LanguageAPI.Add(prefix + "SECONDARY_NAME_FB_JUSTICETOME", "Chapter 2, Igniting Burst");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION_FB_JUSTICETOME", $"Ignite the air around you in an expanding burst, dealing <style=cIsDamage>{StaticValues.virtueFBJusticeSecondaryDamage}%</style> damage, <style=cIsUtility>launching nearby foes away</style> and inflicting <style=cIsDamage>Burning</style>.");

            LanguageAPI.Add(prefix + "UTILITY_NAME_FB_JUSTICETOME", "Chapter 3, Scorched Aftermath");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_FB_JUSTICETOME", $"Detail the suffering in fire and blood inflicted during Vabbi's occupation, dealing <style=cIsDamage>{StaticValues.virtueFBJusticeUtilityDamage}%</style> damage and inflicting <style=cIsDamage>Bleeding</style> and <style=cIsDamage>Burning</style> in the targeted area.");

            LanguageAPI.Add(prefix + "SPECIAL_NAME_FB_JUSTICETOME", "Epilogue, Ashes of the Just");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION_FB_JUSTICETOME", $"Recall the memory of fallen heroes, granting allies the searing blades of Justice, dealing <style=cIsDamage>{StaticValues.virtueFBJusticeSpecialDamage}%</style> damage and inflicting <style=cIsDamage>Burning</style> around you. <style=cIsUtility>Grant yourself and allies five stacks of Justice</style>.");
            #endregion

            // WB
            LanguageAPI.Add(prefix + "PASSIVE_NAME_WB", "Emanating Virtues");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION_WB", "<style=cUserSetting>Automatically swaps loadout to Willbender.</style>\n" + 
                                                               $"<style=cSub>Rushing Justice</style>: <style=cIsUtility>rush forward</style>, striking your target for <style=cIsDamage>{StaticValues.virtueWBJusticeDamage}%</style> damage and leaving a <style=cIsDamage>claw-shaped pattern of flames</style> under the target.\n" +
                                                               $"<style=cSub>Flowing Resolve</style>: <style=cIsUtility>rush foward, evading attacks</style> while temporarily <style=cIsHealing>{StaticValues.virtueWBResolveHeal}%</style> life-steal from your attacks, leaving a <style=cIsDamage>line of flames</style> in your wake.\n" +
                                                               $"<style=cSub>Crashing Courage</style>: <style=cIsUtility>teleport forward</style> while temporarily gaining <style=cIsUtility>Aegis</style> every three attacks, leaving a <style=cIsDamage>ring of flames</style> after teleporting.");

            #endregion

            #region Primaries
            // CORE
            LanguageAPI.Add(prefix + "PRIMARY_NAME", "Mace Chain");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION", $"Mace attack chain consisting of:\n" +
                                                            $"- True Strike: <style=cIsDamage>{StaticValues.corePrimaryChain1Damage}%</style> damage.\n" +
                                                            $"- Pure Strike: <style=cIsDamage>{StaticValues.corePrimaryChain2Damage}%</style> damage.\n" +
                                                            $"- Faithful Strike: <style=cIsDamage>{StaticValues.corePrimaryChain3Damage}%</style> damage, heal yourself and nearby allies for <style=cIsHealing>{StaticValues.corePrimaryChain3Heal}%</style>.");

            LanguageAPI.Add(prefix + "PRIMARY_NAME_CHAIN1", "True Strike");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_CHAIN1", $"Smash your foe for <style=cIsDamage>{StaticValues.corePrimaryChain1Damage}%</style> damage. <style=cSub>Chains into Pure Strike.</style>");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_CHAIN2", "Pure Strike");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_CHAIN2", $"Bash your foe for <style=cIsDamage>{StaticValues.corePrimaryChain2Damage}%</style> damage. <style=cSub>Chains into Faithful Strike.</style>");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_CHAIN3", "Faithful Strike");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_CHAIN3", $"Hit your foe with a final strike for <style=cIsDamage>{StaticValues.corePrimaryChain3Damage}%</style> damage and heal yourself and nearby allies for <style=cIsHealing>{StaticValues.corePrimaryChain3Heal}</style> health. <style=cSub>Ends chain.</style>");

            // DH
            LanguageAPI.Add(prefix + "PRIMARY_NAME_DH", "Puncture Shot");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_DH", $"Fire an arrow that <style=cIsUtility>pierces</style> targets, dealing <style=cIsDamage>{StaticValues.dhPrimaryDamage}%</style> damage. If the arrow hits a second target, deal <style=cIsDamage>{StaticValues.dhPrimaryDamage}%</style> extra damage to all struck targets.");

            // FB
            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB", "Axe Chain");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB", $"Axe attack chain consisting of:\n" +
                                                               $"- Core Cleave: <style=cIsDamage>{StaticValues.fbPrimaryChain1DamagePhysical}%</style> + <style=cIsDamage>{StaticValues.fbPrimaryChain1DamageMagical}%</style> damage and inflict <style=cIsDamage>Bleeding</style>.\n" +
                                                               $"- Bleeding Edge: <style=cIsDamage>{StaticValues.fbPrimaryChain2DamagePhysical}%</style> + <style=cIsDamage>{StaticValues.fbPrimaryChain2DamageMagical}%</style> damage and inflict <style=cIsDamage>Bleeding</style>.\n" +
                                                               $"- Searing Slash: <style=cIsDamage>{StaticValues.fbPrimaryChain3DamagePhysical}%</style> + <style=cIsDamage>{StaticValues.fbPrimaryChain3DamageMagical}%</style> damage and inflict <style=cIsDamage>Burning</style>.");

            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB_CHAIN1", "Core Cleave");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB_CHAIN1", $"Cleave at your foe with a <style=cIsUtility>physical and magical</style> axe for <style=cIsDamage>{StaticValues.fbPrimaryChain1DamagePhysical}%</style> + <style=cIsDamage>{StaticValues.fbPrimaryChain1DamageMagical}% damage</style>, inflicting <style=cIsDamage>Bleeding</style>. <style=cSub>Chains into Bleeding Edge.</style>");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB_CHAIN2", "Bleeding Edge");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB_CHAIN2", $"Slice your foe again, <style=cIsUtility>physically and magically</style>, as your weapon is heating up for <style=cIsDamage>{StaticValues.fbPrimaryChain2DamagePhysical}%</style> + <style=cIsDamage>{StaticValues.fbPrimaryChain2DamageMagical}%, inflicting <style=cIsDamage>Bleeding</style>. <style=cSub>Chains into Searing Slash.</style>");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_FB_CHAIN3", "Searing Slash");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_FB_CHAIN3", $"Unleash your searing axe in an overhand slash, <style=cIsUtility>following it up with a magical edge</style> for <style=cIsDamage>{StaticValues.fbPrimaryChain3DamagePhysical}%</style> + <style=cIsDamage>{StaticValues.fbPrimaryChain3DamageMagical}%, inflicting <style=cIsDamage>Burning</style>. <style=cSub>Ends chain.</style>");

            // WB
            LanguageAPI.Add(prefix + "PRIMARY_NAME_WB", "Sword Chain");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_WB", $"Sword attack chain consisting of:\n" +
                                                            $"- Sword of Wrath: <style=cIsDamage>{StaticValues.wbPrimaryChain1Damage}%</style> damage.\n" +
                                                            $"- Sword Arc: <style=cIsDamage>{StaticValues.wbPrimaryChain2Damage}%</style> damage.\n" +
                                                            $"- Sword Wave: <style=cIsDamage>{StaticValues.wbPrimaryChain3Damage}%</style> damage, launching three projectiles for <style=cIsDamage>{StaticValues.wbPrimaryChain3DamageProjectiles}%</style> damage.");

            LanguageAPI.Add(prefix + "PRIMARY_NAME_WB_CHAIN1", "Sword of Wrath");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_WB_CHAIN1", $"Slash your foe for <style=cIsDamage>{StaticValues.wbPrimaryChain1Damage}%</style> damage. <style=cSub>Chains into Sword Arc.</style>");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_WB_CHAIN2", "Sword Arc");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_WB_CHAIN2", $"Slash your foe again for <style=cIsDamage>{StaticValues.wbPrimaryChain2Damage}%</style> damage. <style=cSub>Chains into Sword Wave.</style>");
            LanguageAPI.Add(prefix + "PRIMARY_NAME_WB_CHAIN3", "Sword Wave");
            LanguageAPI.Add(prefix + "PRIMARY_DESCRIPTION_WB_CHAIN3", $"Stab your foe for <style=cIsDamage>{StaticValues.wbPrimaryChain3Damage}%</style> damage, sending out three projectiles for <style=cIsDamage>{StaticValues.wbPrimaryChain3DamageProjectiles}%</style> damage. <style=cSub>Ends chain.</style>");
            #endregion

            #region Secondaries
            // CORE
            LanguageAPI.Add(prefix + "SECONDARY_NAME", "Zealot's Defense");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION", $"<style=cIsUtility>Destroy nearby enemy projectiles</style> while casting {StaticValues.coreSecondaryAmount} magical projectiles dealing <style=cIsDamage>{StaticValues.coreSecondaryDamage}%</style> damage each. <style=cSub>Projectile count is increased by attack speed</style>.");

            // DH
            LanguageAPI.Add(prefix + "SECONDARY_NAME_DH", "True Shot");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION_DH", $"Charge up a powerful bowshot, dealing up to <style=cIsDamage>{StaticValues.dhSecondaryMinDamage}%</style> - <style=cIsDamage>{StaticValues.dhSecondaryMaxDamage}%</style> damage depending on charge. This attack <style=cIsUtility>pierces</style>.");

            // FB
            LanguageAPI.Add(prefix + "SECONDARY_NAME_FB", "Symbol of Vengeance");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION_FB", $"Cleave your axe into the ground, reducing enemy movement and <style=cIsUtility>carving a razor-sharp symbol of vengeance</style> that deals <style=cIsDamage>{StaticValues.fbSecondaryDamage}%</style> damage every second for {StaticValues.fbSecondaryDuration} seconds.");

            // WB
            LanguageAPI.Add(prefix + "SECONDARY_NAME_WB", "Executioner's Calling");
            LanguageAPI.Add(prefix + "SECONDARY_DESCRIPTION_WB", $"Strike your foe with a rending force for <style=cIsDamage>{StaticValues.wbSecondaryDamageHit}%</style> damage. After a successful hit, follow up this attack with a dual strike for <style=cIsDamage>{StaticValues.wbSecondaryDamageConfirm}%</style> damage, <style=cIsUtility>executing</style> struck targets.");

            #endregion

            #region Utilities
            // CORE
            LanguageAPI.Add(prefix + "UTILITY_NAME", "Shield of Absorption");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION", $"Create a dome around you, <style=cIsUtility>pushing foes back and absorbing enemy projectiles for {StaticValues.coreUtilityDuration} seconds</style>. Grant yourself and allies <style=cIsUtility>Protection</style> for ten seconds whilst inside your dome.");

            // DH
            LanguageAPI.Add(prefix + "UTILITY_NAME_DH", "Test of Faith, Procession of Blades");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_DH", $"<style=cIsUtility>Lay down two traps that synergize with each other</style>, that stay activated when triggered for {StaticValues.dhUtilityActiveDuration} seconds.\n" +
                                                               $"<style=cSub>Test of Faith</style> punishes enemies trying to cross the trap's treshold, dealing <style=cIsDamage>{StaticValues.dhUtilityDamageBoundary}%</style> damage on crossing.\n" +
                                                               $"<style=cSub>Procession of Blades</style> swirls within, dealing <style=cIsDamage>{StaticValues.dhUtilityDamageWhirl}% damage every second.");

            // FB
            LanguageAPI.Add(prefix + "UTILITY_NAME_FB", "Mantra of Solace");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_FB", $"Heal and grant <style=cIsUtility>Aegis to yourself and nearby allies for <style=cIsHealing>{StaticValues.fbUtilityHeal}</style>% health. <style=cSub>Holds {StaticValues.fbUtilityCharges} charges.</style>");

            // WB
            LanguageAPI.Add(prefix + "UTILITY_NAME_WB", "Roiling Light");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_WB", $"<style=cIsUtility>Dodge backwards</style>, gaining <style=cIsUtility>Swiftness</style> and <style=cIsUtility>Blinding</style> nearby foes. <style=cSub>Chains into Quick Retribution.</style>");

            LanguageAPI.Add(prefix + "UTILITY_NAME_WB_CHAIN", "Quick Retribution");
            LanguageAPI.Add(prefix + "UTILITY_DESCRIPTION_WB_CHAIN", $"<style=cIsUtility>Lunge forward</style> and stab your foe for <style=cIsDamage>{StaticValues.wbUtilityDamage}%</style> damage, <style=cIsUtility>stunning</style> them. <style=cSub>Returns to Roiling Light.</style>");
            #endregion

            #region Specials
            // CORE
            LanguageAPI.Add(prefix + "SPECIAL_NAME", "Sanctuary");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION", $"Form a protective shelter for allies that <style=cIsUtility>absorbs enemy projectiles</style>. <style=cSub>The inside of the shelter pulses {StaticValues.coreSpecialPulses} times over {StaticValues.coreSpecialPulses} seconds</style>. Foes inside are <style=cIsDamage>burnt</style> for <style=cIsDamage>{StaticValues.coreSpecialDamage}%</style> damage per pulse, and allies are granted two seconds of <style=cIsHealing>Regeneration</style> per pulse.");

            // DH
            LanguageAPI.Add(prefix + "SPECIAL_NAME_DH", "Dragon's Maw");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION_DH", $"Lay a trap that <style=cIsUtility>pulls enemies in and creates a barrier that holds them in for {StaticValues.dhSpecialActiveDuration} seconds</style>, dealing <style=cIsDamage>{StaticValues.dhSpecialDamage}%</style> damage.");

            // FB
            LanguageAPI.Add(prefix + "SPECIAL_NAME_FB", "Mantra of Flame");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION_FB", $"Release a circle of flame around you to sear enemies, dealing <style=cIsDamage>{StaticValues.fbSpecialDamage}</style>% damage and inflicting <style=cIsDamage>Burning</style>. <style=cSub>Holds {StaticValues.fbSpecialCharges} charges.</style>");

            // WB
            LanguageAPI.Add(prefix + "SPECIAL_NAME_WB", "Heaven's Palm");
            LanguageAPI.Add(prefix + "SPECIAL_DESCRIPTION_WB", $"Slam the ground, <style=cIsUtility>launching nearby enemies away</style> and dealing <style=cIsDamage>{StaticValues.wbSpecialDamage}%</style> damage around you.");
            #endregion
        }
    }
}