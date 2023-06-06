namespace ToyRobot.Logic.Commands;
    public class RightCommand : RobotCommand, IRightCommand
    {

        public RightCommand(IRobot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            if (!Robot.IsPlaced())
            {
                Robot.CommandSuccess = false;
                return;
            }

            Robot.Turn(Turning.Right);
            Robot.CommandSuccess = true;
        }

        public override void Undo()
        {
            if (!Robot.IsPlaced()) return;

            Robot.Turn(Turning.Left);
        }
    }