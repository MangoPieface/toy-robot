namespace ToyRobot.Logic.Commands;
public class LeftCommand : RobotCommand, ILeftCommand
{

    public LeftCommand(IRobot robot) : base(robot)
    {
    }

    public override void Execute()
    {
        if (!TheRobot.IsPlaced())
        {
            TheRobot.CommandSuccess = false;
            return;
        }

        TheRobot.Turn(Turning.Left);
        TheRobot.CommandSuccess = true;
    }

    public override void Undo()
    {
        if (!TheRobot.IsPlaced()) return;

        TheRobot.Turn(Turning.Right);
    }
}

public interface ILeftCommand
{
}
