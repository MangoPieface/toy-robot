namespace ToyRobot.Logic.Commands;
public class LeftCommand : RobotCommand, ILeftCommand
{

    public LeftCommand(IRobot robot) : base(robot)
    {
    }

    public override void Execute()
    {
        if (!_robot.IsPlaced())
        {
            _robot.CommandSuccess = false;
            return;
        }

        _robot.Turn(Robot.Left);
        _robot.CommandSuccess = true;
    }

    public override void Undo()
    {
        if (!_robot.IsPlaced()) return;

        _robot.Turn(Robot.Right);
    }
}

public interface ILeftCommand
{
}
