using System.Drawing;

namespace ToyRobot.Logic
{
    public class Tabletop
    {
        public Point TableDimention { get; set; }

        public Tabletop(int tableHeight, int tableWidth)
        {
            TableDimention = new Point(tableWidth, tableHeight);
        }

    }
}