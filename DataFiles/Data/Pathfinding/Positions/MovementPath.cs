using System.Collections.Generic;

namespace BlueSheep.Data.Pathfinding.Positions
{
    public class MovementPath
    {
        // Methods
        public void Compress()
        {
            if ((Cells.Count > 0))
            {
                int i = (Cells.Count - 1);
                while ((i > 0))
                {
                    if ((Cells[i].Orientation == Cells[(i - 1)].Orientation))
                    {
                        Cells.RemoveAt(i);
                        i -= 1;
                    }
                    i -= 1;
                }
            }
        }

        // Fields
        public MapPoint CellEnd;
        public List<PathElement> Cells = new List<PathElement>();
        public MapPoint CellStart;
    }
}