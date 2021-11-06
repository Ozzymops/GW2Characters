using GuardianPlugin.SkillStates;
using GuardianPlugin.SkillStates.Primary;
using GuardianPlugin.SkillStates.Secondary;
using GuardianPlugin.SkillStates.Utility;
using GuardianPlugin.SkillStates.BaseStates;
using System.Collections.Generic;
using System;

namespace GuardianPlugin.Modules
{
    public static class States
    {
        internal static List<Type> entityStates = new List<Type>();

        internal static void RegisterStates()
        {
            entityStates.Add(typeof(BaseMeleeAttack));

            entityStates.Add(typeof(TrueStrike));
            entityStates.Add(typeof(PureStrike));
            entityStates.Add(typeof(FaithfulStrike));

            entityStates.Add(typeof(ZealotsDefense));

            entityStates.Add(typeof(ShieldOfAbsorption));

            entityStates.Add(typeof(ThrowBomb));
        }
    }
}