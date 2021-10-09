using BepInEx;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Guardian.Modules
{
    public static class SharedPluginWrapper
    {
        #region Setup
        private static bool _enabled = false;

        public static bool enabled
        {
            get
            {
                if (!_enabled)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Ozzymops.GW2Shared");
                }

                return (bool) _enabled;
            }
        }
        #endregion
    }
}
