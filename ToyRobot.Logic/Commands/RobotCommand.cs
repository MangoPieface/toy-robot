namespace ToyRobot.Logic.Commands;

public abstract class RobotCommand
{
    protected readonly IRobot Robot;

    protected RobotCommand(IRobot robot)
    {
        Robot = robot;
    }
    public abstract void Execute();
    public abstract void Undo();

    public virtual bool HasExecutedSuccessfully()
    {
        return Robot.CommandSuccess;
    }
}

