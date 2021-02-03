using System.Drawing;

namespace ToyRobot.Domain
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