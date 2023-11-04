using OpenSilver.Simulator;
using System;

namespace OpenSilverBusinessApplicationDemo.Simulator
{
    internal static class Startup
    {
        [STAThread]
        private static int Main(string[] args)
        {
            return SimulatorLauncher.Start(typeof(App));
        }
    }
}