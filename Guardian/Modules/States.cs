﻿using GuardianPlugin.SkillStates;
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
            entityStates.Add(typeof(SlashCombo));

            entityStates.Add(typeof(Mace1));
            entityStates.Add(typeof(Mace2));
            entityStates.Add(typeof(Mace3));

            entityStates.Add(typeof(Shoot));

            entityStates.Add(typeof(Roll));

            entityStates.Add(typeof(ThrowBomb));
        }
    }
}