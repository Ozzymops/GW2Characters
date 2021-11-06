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

            entityStates.Add(typeof(Mace1));
            entityStates.Add(typeof(Mace2));
            entityStates.Add(typeof(Mace3));

            entityStates.Add(typeof(ZealotsDefense));

            entityStates.Add(typeof(ShieldOfAbsorption));

            entityStates.Add(typeof(ThrowBomb));
        }
    }
}