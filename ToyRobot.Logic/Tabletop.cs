using System.Drawing;

namespace ToyRobot.Logic;
public class Tabletop : ITabletop
{
    public Point TableDimention { get; set; }

    public Tabletop(int tableHeight, int tableWidth)
    {
        TableDimention = new Point(tableWidth, tableHeight);
    }

}

public interface ITabletop
{
    Point TableDimention { get; set; }
}