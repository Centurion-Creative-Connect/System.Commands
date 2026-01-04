using DerpyNewbie.Logger;
using UdonSharp;

namespace CenturionCC.System.Command
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class EventLoggerCommand : NewbieConsoleCommandHandler
    {
        private const string ShotLogPrefix = "-- Begin Shot Log --";
        private const string ShotLogSuffix = "-- End Shot Log --";

        private const string HitLogPrefix = "-- Begin Hit Log --";
        private const string HitLogSuffix = "-- End Hit Log --";

        private const string AvailableSubOptions = "<color=red>Available Sub Options:\n" +
                                                   "   hit\n" +
                                                   "   shot\n" +
                                                   "   all</color>";

        public override string Label => "EventLogger";

        public override string Usage =>
            "no-op";

        public override string Description => "no-op";

        public override string OnCommand(NewbieConsole console, string label, string[] vars, ref string[] envVars)
        {
            return "no-op";
        }
    }
}