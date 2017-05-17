namespace MageBot.DataFiles.Data.Pathfinding
{
    public class OpenSquare
    {
        public int X { get; set; }
        public int Y { get; set; }

        public OpenSquare(int y, int x)
        {
            X = x;
            Y = y;
        }
    }

}
