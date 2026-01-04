using CenturionCC.System.Player;
using CenturionCC.System.Util;
using DerpyNewbie.Common;
using DerpyNewbie.Logger;
using UdonSharp;
using UnityEngine;

namespace CenturionCC.System.Command
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class DamageDataResolverCommand : NewbieConsoleCommandHandler
    {
        [SerializeField] [HideInInspector] [NewbieInject]
        private PlayerManagerBase playerManager;

        [SerializeField] private MockDamageData mockData;

        public override string Label => "DamageDataResolver";
        public override string[] Aliases => new[] { "DmgResolver", "DDResolver" };
        public override string Description => "no-op";

        public override string Usage =>
            "no-op";

        public override string OnCommand(NewbieConsole console, string label, string[] vars, ref string[] envVars)
        {
            return "";
        }
    }
}