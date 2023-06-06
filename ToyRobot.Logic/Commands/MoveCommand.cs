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
        if (!Robot.IsPlaced())
        {
            Robot.CommandSuccess = false;
            return;
        }

        if (Robot.Position.Y < _tabletop.TableDimention.Y && Robot.Direction == Facing.North)
        {
            Robot.Move(Direction.Forward);
            Robot.CommandSuccess = true;
            return;
        }

        if (Robot.Position.X < _tabletop.TableDimention.X && Robot.Direction == Facing.East)
        {
            Robot.Move(Direction.Forward);
            Robot.CommandSuccess = true;
            return;
        }

        if (Robot.Position.Y > 0 && Robot.Direction == Facing.South)
        {
            Robot.Move(Direction.Forward);
            Robot.CommandSuccess = true;
            return;
        }
        
        if (Robot.Position.X > 0 && Robot.Direction == Facing.West)
        {
            Robot.Move(Direction.Forward);
            Robot.CommandSuccess = true;
            return;
        }
        
        Robot.CommandSuccess = false;
    }


    public override void Undo()
    {
        if (!Robot.IsPlaced())
        {
            return;
        }
        
        Robot.Move(Direction.Backward);

    }
}