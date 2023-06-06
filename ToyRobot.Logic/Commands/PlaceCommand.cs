namespace ToyRobot.Logic.Commands;
public class PlaceCommand : RobotCommand, IPlaceCommand
{
    private readonly ITabletop _tabletop;
    public int X { get; set; }
    public int Y { get; set; }

    public string Direction { get; set; }

    public PlaceCommand(IRobot robot, ITabletop tabletop) : base(robot)
    {
        _tabletop = tabletop;
    }

    public override void Execute()
    {
        if (X > _tabletop.TableDimention.X && Y > _tabletop.TableDimention.Y)
        {
            Robot.CommandSuccess = false;
            return;
        }

        var direction = (Facing) Enum.Parse(typeof(Facing),
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Direction.ToLower()));
        Robot.Place(X, Y, direction);
        Robot.CommandSuccess = true;
    }

    public override void Undo()
    {
        Robot.UnPlace();
    }
}