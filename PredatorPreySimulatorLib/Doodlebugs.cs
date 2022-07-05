using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredatorPreySimulatorLib
{
    class Doodlebugs : Critter
    {
        public int _Eat { get; set; }

        public List<Doodlebugs> _doodlebugs = new List<Doodlebugs>();
        Critter _ant;

        public override void assignCritterToCell(int cellPosition, int critterID)
        {
            _doodlebugs[critterID]._CellPosition = cellPosition;
        }

        public override void Create(int NoCritters)
        {
            for (int i = 0; i < NoCritters; i++)
            {
                var doodlebugs = new Doodlebugs();
                doodlebugs._NoOfSteps = 0;
                doodlebugs._Eat = 0;
                doodlebugs._CellPosition = 0;
                _doodlebugs.Add(doodlebugs);
            }
            _NoOfCritters = NoCritters;
        }

        public override int[] Breed(int adjacentCellsForNewCritter, int[] isOccupied)
        {
            if (adjacentCellsForNewCritter != -1)
            {
                var newdoodlebug = new Doodlebugs();
                newdoodlebug._NoOfSteps = 0;
                newdoodlebug._Eat = 0;
                newdoodlebug._CellPosition = adjacentCellsForNewCritter;
                _doodlebugs.Add(newdoodlebug);
                _cellSpace[adjacentCellsForNewCritter] = 1;
                _Cell[adjacentCellsForNewCritter] = 'x';
                isOccupied[adjacentCellsForNewCritter] = 1;
                _NoOfCritters += 1;
            }
            return isOccupied;
        }
        public override int GetAdjacentCellForNewCritter(int CellPosition)
        {
            int MoveCritters = -1;
            Random rnd = new Random();

            int[] positions = { CellPosition + 1, CellPosition - 1, CellPosition + 20, CellPosition - 20 };

            int step = rnd.Next(0, 4);

            if (positions[step] >= 0 && positions[step] < _cellSpace.Length)
            {
                if (_cellSpace[positions[step]] == 1)
                {
                    MoveCritters = -1;
                }
                else
                {
                    MoveCritters = positions[step];
                }
            }
            else
            {
                MoveCritters = -1;
            }

            return MoveCritters;
        }

        public override int GetNoOfCritters()
        {
            return _doodlebugs.Count;
        }

        public override char[] GetCell()
        {
            return _Cell;
        }
        public override int[] GetCellSpace()
        {
            return _cellSpace;
        }

        public override int KillCritter(int AntPosition, int doodleBugId)
        {
            int killed =  _ant.KillCritter(AntPosition, doodleBugId);
            if (killed == 1)
            {
                foreach (var doodlebug in _doodlebugs.Where(d => d._CellPosition == doodleBugId).ToList())
                    {
                        doodlebug._Eat += 1;
                    }
            }
            return killed;
        }

        private int lifeSpanOfDoodlebug(int id)
        {
            int lifespan = 1;

            foreach (var doodlebug in _doodlebugs.Where(d => d._CellPosition == id).ToList())
            {
                if (doodlebug._NoOfSteps >= 3 && doodlebug._Eat == 0)
                {
                    _doodlebugs.Remove(doodlebug);
                    _NoOfCritters -= 1;
                    lifespan = -1;
                }
            }
            
            return lifespan;
        }

        public override void assignCritterToNewCell(int i, int[] isOccupied, Random rnd)
        {
            int lifeSpan = lifeSpanOfDoodlebug(i);

            int[] adjacentCells = { i + 1, i - 1, i + 20, i - 20 };

            int step = rnd.Next(0, 4);

            if (adjacentCells[step] >= 0 && adjacentCells[step] < _Cell.Length && _Cell[adjacentCells[step]] != 'x' && lifeSpan == 1)
            {
                _cellSpace[i] = 0;
                _Cell[i] = ' ';
                _cellSpace[adjacentCells[step]] = 1;
                _Cell[adjacentCells[step]] = 'x';
                isOccupied[adjacentCells[step]] = 1;

                KillCritter(adjacentCells[step], i);

                foreach (var doodlebug in _doodlebugs.Where(d => d._CellPosition == i).ToList())
                {
                    doodlebug._CellPosition = adjacentCells[step];
                    doodlebug._NoOfSteps += 1;
                    if (doodlebug._NoOfSteps >= 8)
                    {
                        int adjacentCellsForNewDoodlebug = GetAdjacentCellForNewCritter(adjacentCells[step]);
                        isOccupied = Breed(adjacentCellsForNewDoodlebug, isOccupied);
                        
                        doodlebug._NoOfSteps = 0;
                        doodlebug._Eat = 0;
                    }
                }
            }
            if (lifeSpan == -1)
            {
                _cellSpace[i] = 0;
                _Cell[i] = ' ';
            }
        }

        public override void MoveCritters(int[] cellSpace, char[] Cell, Critter critter)
        {
            _ant = critter;
            _Cell = Cell;
            _cellSpace = cellSpace;
            int[] isOccupied = new int[_Cell.Length];
            Random rnd = new Random();
            for (int i = 0; i < _Cell.Length; i++)
            {
                if (_cellSpace[i] == 1 && _Cell[i] == 'x' && isOccupied[i] != 1)
                {
                    assignCritterToNewCell(i, isOccupied, rnd);
                }
            }
            
        }

    }
}
