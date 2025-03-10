using CenturionCC.System.Player;
using CenturionCC.System.Player.MassPlayer;
using CenturionCC.System.Util;
using DerpyNewbie.Common;
using DerpyNewbie.Logger;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;

namespace CenturionCC.System.Command
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class DamageDataResolverCommand : NewbieConsoleCommandHandler
    {
        [SerializeField] [HideInInspector] [NewbieInject]
        private PlayerManager playerManager;

        [SerializeField] [HideInInspector] [NewbieInject]
        private DamageDataSyncerManager syncerMgr;

        [SerializeField] [HideInInspector] [NewbieInject]
        private DamageDataResolver resolver;

        [SerializeField] private MockDamageData mockData;

        public override string Label => "DamageDataResolver";
        public override string[] Aliases => new[] { "DmgResolver", "DDResolver" };
        public override string Description => "Configures DamageDataResolver";

        public override string Usage =>
            "<command> <getAssumedDiedTime|getConfirmedDiedTime|testInvokeHit|useTimeBasedCheck> [value] OR <command> <printEvents|pause|continue|isPaused>";

        public override string OnCommand(NewbieConsole console, string label, string[] vars, ref string[] envVars)
        {
            if (vars.Length == 0)
                return console.PrintUsage(this);

            switch (vars[0].ToLower())
            {
                // all strings are lowered
                // ReSharper disable StringLiteralTypo
                case "getassumed":
                case "getassumedtime":
                case "getassumeddiedtime":
                {
                    if (vars.Length <= 1)
                    {
                        console.Println("Requires Player ID value as argument");
                        return ConsoleLiteral.GetNone();
                    }

                    var playerId = ConsoleParser.TryParseInt(vars[1]);
                    if (playerId <= 0)
                    {
                        console.Println("Requires proper Player ID");
                        return ConsoleLiteral.GetNone();
                    }

                    var assumedTime = resolver.GetAssumedDiedTime(playerId);
                    return assumedTime.ToString("s");
                }
                case "getconfirmed":
                case "getconfirmedtime":
                case "getconfirmeddiedtime":
                {
                    if (vars.Length <= 1)
                    {
                        console.Println("Requires Player ID value as second argument");
                        return ConsoleLiteral.GetNone();
                    }

                    var playerId = ConsoleParser.TryParseInt(vars[1]);
                    if (playerId <= 0)
                    {
                        console.Println("Requires proper Player ID");
                        return ConsoleLiteral.GetNone();
                    }

                    var player = playerManager.GetPlayerById(playerId);
                    return player.LastHitData.HitTime.ToString("s");
                }
                case "ispaused":
                {
                    var paused = syncerMgr.ProcessingPaused;
                    return ConsoleLiteral.Of(paused);
                }
                case "printevents":
                {
                    if (syncerMgr.GetEventsJson(JsonExportType.Beautify, out var jsonToken))
                    {
                        var jsonStr = jsonToken.String;
                        console.Println(jsonStr);
                        return jsonStr;
                    }

                    console.Println(jsonToken.Error.ToString());
                    return ConsoleLiteral.GetNone();
                }
                case "pause":
                {
                    console.Println("Requested SyncerManager to pause processing");
                    syncerMgr.PauseProcessing();
                    return ConsoleLiteral.GetNone();
                }
                case "continue":
                {
                    console.Println("Requested SyncerManager to continue processing");
                    syncerMgr.ContinueProcessing();
                    return ConsoleLiteral.GetNone();
                }
                case "testinvokehit":
                {
                    if (vars.Length <= 1)
                    {
                        console.Println("Requires Player ID value as second argument");
                        return ConsoleLiteral.GetNone();
                    }

                    var playerId = ConsoleParser.TryParseInt(vars[1]);
                    if (playerId <= 0)
                    {
                        console.Println("Requires proper Player ID");
                        return ConsoleLiteral.GetNone();
                    }

                    var model = (PlayerModel)playerManager.GetPlayerById(playerId);
                    if (model == null)
                    {
                        console.Println("Requires in-game Player ID");
                        return ConsoleLiteral.GetNone();
                    }

                    if (model.PlayerView == null)
                    {
                        console.Println("Requires player model with view");
                        return ConsoleLiteral.GetNone();
                    }

                    var view = model.PlayerView;
                    var pCol = view.GetColliders()[0];

                    mockData.SetData(
                        true,
                        Networking.LocalPlayer.playerId,
                        Networking.LocalPlayer.GetPosition(),
                        Networking.LocalPlayer.GetRotation(),
                        Networking.GetNetworkDateTime(),
                        "TestInvokeHit"
                    );

                    console.Println($"Directly damaged a player `{NewbieUtils.GetPlayerName(model.VrcPlayer)}`");
                    pCol.OnDamage(mockData, model.Position);
                    return ConsoleLiteral.GetNone();
                }
                case "timebased":
                case "usetimebasedcheck":
                {
                    if (vars.Length > 1)
                    {
                        resolver.useTimeBasedCheck = ConsoleParser.TryParseBoolean(vars[1], resolver.useTimeBasedCheck);
                    }

                    console.Println($"{resolver.useTimeBasedCheck}");
                    return ConsoleLiteral.Of(resolver.useTimeBasedCheck);
                }
                default:
                {
                    return console.PrintUsage(this);
                }
                // ReSharper restore StringLiteralTypo
            }
        }
    }
}