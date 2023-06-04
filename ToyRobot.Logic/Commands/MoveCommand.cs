namespace ToyRobot.Logic.Commands;

public class MoveCommand : RobotCommand, IMoveCommand
{
    private readonly ITabletop _tabletop;


    public MoveCommand(IRobot robot, ITabletop tabletop) : base(robot)
    {
        _tabletop = tabletop;
    }

    public override void Execute()
    {
        if (!TheRobot.IsPlaced())
        {
            TheRobot.CommandSuccess = false;
            return;
        }

        if (TheRobot.Position.Y < _tabletop.TableDimention.Y && TheRobot.Direction == Facing.North)
        {
            TheRobot.Move(Direction.Forward);
            TheRobot.CommandSuccess = true;
            return;
        }

        if (TheRobot.Position.X < _tabletop.TableDimention.X && TheRobot.Direction == Facing.East)
        {
            TheRobot.Move(Direction.Forward);
            TheRobot.CommandSuccess = true;
            return;
        }

        if (TheRobot.Position.Y > 0 && TheRobot.Direction == Facing.South)
        {
            TheRobot.Move(Direction.Forward);
            TheRobot.CommandSuccess = true;
            return;
        }


        if (TheRobot.Position.X > 0 && TheRobot.Direction == Facing.West)
        {
            TheRobot.Move(Direction.Forward);
            TheRobot.CommandSuccess = true;
            return;
        }

        TheRobot.CommandSuccess = false;
    }


    public override void Undo()
    {
        if (!TheRobot.IsPlaced())
        {
            return;
        }


        TheRobot.Move(Direction.Backward);

    }
}

public interface IMoveCommand
{
}
