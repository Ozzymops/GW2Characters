using System;

namespace GuardianPlugin.Modules
{
    internal static class StaticValues
    {
        #region Core Skills
        internal const float corePrimaryChain1Damage = 125f;
        internal const float corePrimaryChain2Damage = 150f;
        internal const float corePrimaryChain3Damage = 250f;
        internal const float corePrimaryChain3Heal = 5f;
        internal const float coreSecondaryDamage = 100f;
        internal const int coreSecondaryAmount = 8;
        internal const float coreUtilityDuration = 5f;
        internal const float coreSpecialDamage = 100f;
        internal const int coreSpecialPulses = 10;
        #endregion

        #region Dragonhunter Skills
        internal const float dhPrimaryDamage = 250f;
        internal const float dhPrimaryDamageBonus = 75f;
        internal const float dhSecondaryMinDamage = 250f;
        internal const float dhSecondaryMaxDamage = 2500f;
        internal const float dhUtilityDamageBoundary = 300f;
        internal const float dhUtilityDamageWhirl = 100f;
        internal const float dhUtilityActiveDuration = 5f;
        internal const float dhSpecialDamage = 500f;
        internal const float dhSpecialActiveDuration = 5f;
        #endregion

        #region Firebrand Skills
        internal const float fbPrimaryChain1DamagePhysical = 100f;
        internal const float fbPrimaryChain1DamageMagical = 50f;
        internal const float fbPrimaryChain2DamagePhysical = 150f;
        internal const float fbPrimaryChain2DamageMagical = 50f;
        internal const float fbPrimaryChain3DamagePhysical = 250f;
        internal const float fbPrimaryChain3DamageMagical = 50f;
        internal const float fbSecondaryDamage = 150f;
        internal const float fbSecondaryDuration = 5f;
        internal const float fbUtilityHeal = 10f;
        internal const int fbUtilityCharges = 3;
        internal const float fbSpecialDamage = 150f;
        internal const int fbSpecialCharges = 3;
        #endregion

        #region Willbender Skills
        internal const float wbPrimaryChain1Damage = 150f;
        internal const float wbPrimaryChain2Damage = 250f;
        internal const float wbPrimaryChain3Damage = 400f;
        internal const float wbPrimaryChain3DamageProjectiles = 150f;
        internal const float wbSecondaryDamageHit = 50f;
        internal const float wbSecondaryDamageConfirm = 500f;
        internal const float wbUtilityDamage = 300f;
        internal const float wbSpecialDamage = 300f;
        #endregion

        #region Virtues

        #region Shared values
        internal const float virtueRadius = 15f;

        internal const int virtuePassiveJusticeHitsNeeded = 5;
        internal const float virtuePassiveResolveInterval = 2f;
        internal const float virtuePassiveResolveHeal = 3f;
        internal const float virtuePassiveCourageInterval = 10f;
        #endregion

        #region Core Virtues
        internal const int virtueCoreActivatedJusticeStacks = 5;
        internal const float virtueCoreActivatedResolveHeal = 10f;
        internal const int virtueCoreActivatedCourageTicks = 5;
        internal const float virtueCoreActivatedCourageTimer = 0.5f;

        internal const float virtueCoreActivatedJusticeCooldown = 10f;
        internal const float virtueCoreActivatedResolveCooldown = 15f;
        internal const float virtueCoreActivatedCourageCooldown = 25f;
        #endregion

        #region Projected Virtues
        internal const float virtueDHSpearDamage = 100f;
        internal const float virtueDHWingsHeal = 10f;
        internal const float virtueDHShieldDuration = 5f;
        #endregion

        #region Tomes
        internal const int virtueFBCharges = 5;

        internal const float virtueFBJusticePrimaryDamage = 150f;
        internal const float virtueFBJusticeSecondaryDamage = 250f;
        internal const float virtueFBJusticeUtilityDamage = 100f;
        internal const float virtueFBJusticeSpecialDamage = 100f;

        internal const float virtueFBResolvePrimaryHeal = 10f;
        internal const float virtueFBResolveSecondaryHeal = 5f;
        internal const int virtueFBResolveSecondaryCleanses = 3;
        internal const float virtueFBResolveSpecialHeal = 50f;

        internal const float virtueFBCourageSecondaryDamage = 100f;
        internal const float virtueFBCourageSpecialInvulnerabilityDuration = 5f;

        // Cooldowns!
        #endregion

        #region Emanating Virtues
        internal const float virtueWBJusticeDamage = 200f;
        internal const float virtueWBResolveHeal = 2.5f;
        internal const float virtueWBCourageDamage = 300f;
        internal const float virtueWBJusticeDuration = 5.0f;
        internal const float virtueWBResolveDuration = 5.0f;
        internal const float virtueWBCourageDuration = 5.0f;
        #endregion

        #endregion

        #region Traits

        #region Core Traits
        internal const float traitAegisDamageCoefficient = 2f;
        internal const float traitAegisCooldown = 6f;
        internal const float traitRenewedJusticeCooldown = 5f;
        internal const float traitHealFromAbilityAmount = 1f;
        #endregion

        #endregion
    }
}