using System;

namespace PredatorPreySimulatorLib
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulator = new Simulator();
            Random rnd = new Random();
            int NoOfAnts = rnd.Next(10, 20);
            int NoOfDoodlebugs = rnd.Next(5, 15);
            simulator.GenerateCritters(NoOfAnts, NoOfDoodlebugs);
            simulator.DistributeCritters();
            simulator.ShowWorld();
            simulator.StartSimulation();
        }
    }
}
