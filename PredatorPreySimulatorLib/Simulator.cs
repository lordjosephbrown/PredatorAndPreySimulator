using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace PredatorPreySimulatorLib
{
    public class Simulator
    {
        int _NoOfAnts;
        int _NoOfDoodlebugs;
        int[] _cellSpace = new int[401];
        char[] _cell = new char[401];
        List<Critter> _critters = new List<Critter>() {new Ants(), new Doodlebugs()};
        Grid _grid = new Grid();
        private Timer timer;

        public void GenerateCritters(int NoOfAnts,int NoOfDoodlebugs) 
        {
            _NoOfAnts = NoOfAnts;
            _NoOfDoodlebugs = NoOfDoodlebugs;

            _critters[0].Create(_NoOfAnts);
            _critters[1].Create(_NoOfDoodlebugs);
        }

        private void assignAntsToCell()
        {
            for (int i = 0; i < _NoOfAnts; i++)
            {
                Random rnd = new Random();
                int antCell = rnd.Next(0, 400);
                if (_cellSpace[antCell] == 0)
                {
                    _cell[antCell] = 'o';
                    _cellSpace[antCell] = 1;
                    _critters[0].assignCritterToCell(antCell, i);
                }
                else
                {
                    antCell = rnd.Next(0, 400);
                    _cell[antCell] = 'o';
                    _cellSpace[antCell] = 1;
                    _critters[0].assignCritterToCell(antCell, i);
                }
            }
        }
        private void assignDoodlebugsToCell()
        {
            for (int i = 0; i < _NoOfDoodlebugs; i++)
            {
                Random rnd = new Random();
                int doodlebugsCell = rnd.Next(0, 400);
                if (_cellSpace[doodlebugsCell] == 0)
                {
                    _cell[doodlebugsCell] = 'x';
                    _cellSpace[doodlebugsCell] = 1;
                    _critters[1].assignCritterToCell(doodlebugsCell, i);
                }
                else
                {
                    doodlebugsCell = rnd.Next(0, 400);
                    _cell[doodlebugsCell] = 'x';
                    _cellSpace[doodlebugsCell] = 1;
                    _critters[1].assignCritterToCell(doodlebugsCell, i);
                }
            }
        }

        public void DistributeCritters() 
        {
            for (int i = 0; i < 401; i++)
            {
                _cellSpace[i] = 0;
                _cell[i] = ' ';
            }

            assignAntsToCell();
            assignDoodlebugsToCell();
        }
        
        public void ShowWorld() 
        {
            Console.WriteLine($"Number of ants(o): {_critters[0].GetNoOfCritters()}.  Number of doodlebugs(x): {_critters[1].GetNoOfCritters()}. \n\n");
            
            _grid.PrintGrid(_cell);
        }

        private void moveCritters(object sender, EventArgs e)
        {
            _critters[0].MoveCritters(_cellSpace, _cell, _critters[0]);
            _cellSpace = _critters[0].GetCellSpace();
            _cell = _critters[0].GetCell();
            _critters[1].MoveCritters(_cellSpace, _cell, _critters[0]);
            _cellSpace = _critters[0].GetCellSpace();
            _cell = _critters[1].GetCell();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Number of ants(o): {_critters[0].GetNoOfCritters()}.  Number of doodlebugs(x): {_critters[1].GetNoOfCritters()}. \n\n");
            _grid.PrintGrid(_cell);
            if (_critters[1].GetNoOfCritters() == 0)
            {
                Console.WriteLine($"\n\n The {_critters[0].GetType().Name} survived the mass extinction which wiped out the {_critters[1].GetType().Name}.");
                Environment.Exit(0);
            }
            if (_critters[0].GetNoOfCritters() == 0)
            {
                Console.WriteLine($"\n\n The {_critters[0].GetType().Name} could not survive the attacts of the {_critters[1].GetType().Name}.");
                Environment.Exit(0);
            }
        }

        public void StartSimulation()
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(moveCritters);
            timer.Interval = 1000; // in miliseconds
            timer.Start();
            Console.ReadKey();
        }

        public int GetNoOfAnts() {

            return _critters[0].GetNoOfCritters();
        }

        public int GetNoOfDoodlebugs()
        {
            return _critters[1].GetNoOfCritters();
        }

    }
}
