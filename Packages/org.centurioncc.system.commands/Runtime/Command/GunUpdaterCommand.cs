using DerpyNewbie.Logger;
using UdonSharp;
namespace CenturionCC.System.Command
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GunUpdaterCommand : NewbieConsoleCommandHandler
    {

        public override string Label => "GunUpdater";
        public override string Usage => "no-op";

        public override string OnCommand(NewbieConsole console, string label, string[] vars, ref string[] envVars)
        {
            return console.PrintUsage(this);
        }
    }
}
