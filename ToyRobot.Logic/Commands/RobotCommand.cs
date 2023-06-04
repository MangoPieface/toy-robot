namespace ToyRobot.Logic.Commands;

public abstract class RobotCommand
{
    protected readonly IRobot TheRobot;

    protected RobotCommand(IRobot theRobot)
    {
        TheRobot = theRobot;
    }

    public abstract void Execute();
    public abstract void Undo();

    public virtual bool HasExecutedSuccessfully()
    {
        return TheRobot.CommandSuccess;
    }
}

