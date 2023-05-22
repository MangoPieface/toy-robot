namespace ToyRobot.Logic.Commands;

public abstract class RobotCommand
{
    protected IRobot _robot;

    protected RobotCommand(IRobot robot)
    {
        _robot = robot;
           
    }

    public abstract void Execute();
    public abstract void Undo();

    public virtual bool HasExcecutedSuccesfully()
    {
        return _robot.CommandSuccess;
    }
}

