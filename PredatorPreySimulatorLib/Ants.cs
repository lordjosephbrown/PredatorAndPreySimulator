using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredatorPreySimulatorLib
{
    class Ants : Critter
    {

        public List<Ants> _ants = new List<Ants>();

        public List<Ants> GetAnts()
        {
            return _ants;
        }
        public override void assignCritterToCell(int cellPosition, int critterID)
        {
            _ants[critterID]._CellPosition = cellPosition;
        }

        public override void Create(int NoCritters)
        {
            for (int i = 0; i < NoCritters; i++)
            {
                var ant = new Ants();
                ant._NoOfSteps = 0;
                ant._CellPosition = 0;
                _ants.Add(ant);
            }
            
           _NoOfCritters = NoCritters;
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

        public override int[] Breed(int adjacentCellsForNewCritter, int[] isOccupied)
        {
            if (adjacentCellsForNewCritter != -1)
            {
                var newAnt = new Ants();
                newAnt._NoOfSteps = 0;
                newAnt._CellPosition = adjacentCellsForNewCritter;
                _ants.Add(newAnt);
                _cellSpace[adjacentCellsForNewCritter] = 1;
                _Cell[adjacentCellsForNewCritter] = 'o';
                isOccupied[adjacentCellsForNewCritter] = 1;
                _NoOfCritters += 1;
            }
            return isOccupied;
        }

        public override int GetNoOfCritters()
        {
            return _ants.Count;
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
            int killed = 0;
            foreach (var ant in _ants.Where(a => a._CellPosition == AntPosition).ToList())
            {
                _ants.Remove(ant);
                killed = 1;
                _NoOfCritters -= 1;
            }
            
            return killed;
        }

        public override void assignCritterToNewCell(int i, int[] isOccupied, Random rnd)
        {
            int[] adjacentCells = { i + 1, i - 1, i + 20, i - 20 };

            int step = rnd.Next(0, 4);
            if (adjacentCells[step] >= 0 && adjacentCells[step] < _Cell.Length && _Cell[adjacentCells[step]] != 'x' && _Cell[adjacentCells[step]] != 'o')
            {
                _cellSpace[i] = 0;
                _Cell[i] = ' ';
                _cellSpace[adjacentCells[step]] = 1;
                _Cell[adjacentCells[step]] = 'o';
                isOccupied[adjacentCells[step]] = 1;
                foreach (var ant in _ants.Where(w => w._CellPosition == i).ToList())
                {
                    if (ant._NoOfSteps > 2)
                    {
                        ant._CellPosition = adjacentCells[step];
                        ant._NoOfSteps = 0;

                        //new ant
                        int adjacentCellsForNewAnt = GetAdjacentCellForNewCritter(adjacentCells[step]);
                        isOccupied = Breed(adjacentCellsForNewAnt, isOccupied);
                        
                    }
                    else
                    {
                        ant._CellPosition = adjacentCells[step];
                        ant._NoOfSteps += 1;
                    }

                }
            }
        }

        public override void MoveCritters(int[] cellSpace, char[] Cell, Critter critter)
        {
            _Cell = Cell;
            _cellSpace = cellSpace;
            int[] isOccupied = new int[_Cell.Length];
            Random rnd = new Random();
            for (int i = 0; i < _Cell.Length; i++)
            {
                if (_cellSpace[i] == 1 && _Cell[i] == 'o' && isOccupied[i] != 1)
                {
                    assignCritterToNewCell(i, isOccupied, rnd);
                }
            }
           
        }

    }
}
