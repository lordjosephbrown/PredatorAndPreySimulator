using System;
using System.Collections.Generic;
using System.Text;

namespace PredatorPreySimulatorLib
{
    public abstract class Critter
    {
        public int _NoOfCritters { get; set; }
        public int[] _cellSpace { get; set; }
        public char[] _Cell { get; set; }
        public int _NoOfSteps { get; set; }
        public int _CellPosition { get; set; }
        public abstract void Create(int NoCritters);
        public abstract int[] Breed(int adjacentCellsForNewCritter, int[] isOccupied);
        public abstract void assignCritterToCell(int cellPosition, int critterID);
        public abstract void assignCritterToNewCell(int cellPosition, int[] isOccupied, Random randomCell);
        public abstract int GetNoOfCritters();
        public abstract int GetAdjacentCellForNewCritter(int CellPosition);
        public abstract void MoveCritters(int[] cellSpace, char[] Cell, Critter critter);
        public abstract int[] GetCellSpace();
        public abstract char[] GetCell();
        public abstract int KillCritter(int AntPosition, int doodleBugId);


    }
}
