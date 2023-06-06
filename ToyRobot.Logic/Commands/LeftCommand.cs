namespace ToyRobot.Logic.Commands;
public class LeftCommand : RobotCommand, ILeftCommand
{

    public LeftCommand(IRobot robot) : base(robot)
    {
    }

    public override void Execute()
    {
        if (!Robot.IsPlaced())
        {
            Robot.CommandSuccess = false;
            return;
        }

        Robot.Turn(Turning.Left);
        Robot.CommandSuccess = true;
    }

    public override void Undo()
    {
        if (!Robot.IsPlaced()) return;

        Robot.Turn(Turning.Right);
    }
}