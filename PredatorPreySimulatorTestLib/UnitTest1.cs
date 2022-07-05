using NUnit.Framework;
using PredatorPreySimulatorLib;
using System;

namespace PredatorPreySimulatorTestLib
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanGenerateAnts()
        {
            var simulator = new Simulator();
            Random rnd = new Random();
            int NoOfAnts = rnd.Next(10, 20);
            int NoOfDoodlebugs = rnd.Next(5, 40);
            simulator.GenerateCritters(NoOfAnts, NoOfDoodlebugs);
            Assert.AreEqual(NoOfAnts, simulator.GetNoOfAnts());
            //Assert.Pass();
        }

        [Test]
        public void CanGenerateDoodlebugs()
        {
            var simulator = new Simulator();
            Random rnd = new Random();
            int NoOfAnts = rnd.Next(10, 20);
            int NoOfDoodlebugs = rnd.Next(5, 40);
            simulator.GenerateCritters(NoOfAnts, NoOfDoodlebugs);
            Assert.AreEqual(NoOfDoodlebugs, simulator.GetNoOfDoodlebugs());
            //Assert.Pass();
        }
    }
}