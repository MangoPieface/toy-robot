using ToyRobot.Logic.Interfaces;

namespace ToyRobot.Logic.Commands
{
    public class RightCommand : RobotCommand, IRightCommand
    {

        public RightCommand(IRobot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            if (!_robot.IsPlaced())
            {
                _robot.CommandSuccess = false;
                return;
            }

            _robot.Turn(Robot.Right);
            _robot.CommandSuccess = true;
        }

        public override void Undo()
        {
            if (!_robot.IsPlaced()) return;

            _robot.Turn(Robot.Left);
        }
    }
}
