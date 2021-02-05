namespace ToyRobot.Logic.Commands
{
    public abstract class RobotCommand
    {
        protected Robot _robot;

        protected RobotCommand(Robot robot)
        {
            _robot = robot;
           
        }

        public abstract void Execute();
        public abstract void Undo();
    }
}
