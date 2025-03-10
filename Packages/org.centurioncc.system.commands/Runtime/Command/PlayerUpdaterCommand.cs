using CenturionCC.System.Player.MassPlayer;
using DerpyNewbie.Common;
using DerpyNewbie.Logger;
using UdonSharp;
using UnityEngine;

namespace CenturionCC.System.Command
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PlayerUpdaterCommand : NewbieConsoleCommandHandler
    {
        [SerializeField] [HideInInspector] [NewbieInject]
        private PlayerUpdater updater;

        public override string Label => "PlayerUpdater";
        public override string Usage => "<command> <list|maxSortStep|count> [value]";

        public override string OnCommand(NewbieConsole console, string label, string[] vars,
            ref string[] envVars)
        {
            if (vars.Length == 0)
            {
                console.PrintUsage(this);
                return ConsoleLiteral.GetNone();
            }

            switch (vars[0].ToLower())
            {
                case "l":
                case "list":
                    var listStr = updater.GetOrder();
                    console.Print(listStr);
                    return listStr;
                case "m":
                case "maxsortstep":
                    if (vars.Length > 1)
                    {
                        var i = ConsoleParser.TryParseInt(vars[1]);
                        if (i < 1 || i > updater.ModelCount - 2)
                        {
                            console.Println($"{i} exceeds capable range of 1 to {updater.ModelCount - 2}!");
                            return ConsoleLiteral.GetNone();
                        }

                        updater.sortStepCount = i;
                    }

                    console.Println($"{updater.sortStepCount}");
                    return ConsoleLiteral.Of(updater.sortStepCount);
                case "c":
                case "count":
                    if (vars.Length > 1)
                    {
                        var i = ConsoleParser.TryParseInt(vars[1]);
                        if (i < 1 || i > updater.ModelCount - 1)
                        {
                            console.Println($"{i} exceeds capable range of 1 to {updater.ModelCount - 1}!");
                            return ConsoleLiteral.GetNone();
                        }

                        updater.ViewCount = i;
                    }

                    console.Println($"{updater.ViewCount}");
                    return ConsoleLiteral.Of(updater.ViewCount);
                default:
                    console.PrintUsage(this);
                    return ConsoleLiteral.GetNone();
            }
        }
    }
}