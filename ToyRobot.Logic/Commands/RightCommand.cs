namespace ToyRobot.Logic.Commands
{
    public class RightCommand : RobotCommand
    {

        public RightCommand(Robot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            if (!_robot.IsPlaced()) return;

            _robot.Turn(Robot.Right);
        }

        public override void Undo()
        {
            if (!_robot.IsPlaced()) return;

            _robot.Turn(Robot.Left);
        }
    }
}
