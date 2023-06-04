namespace ToyRobot.Logic.Commands;
    public class RightCommand : RobotCommand, IRightCommand
    {

        public RightCommand(IRobot theRobot) : base(theRobot)
        {
        }

        public override void Execute()
        {
            if (!TheRobot.IsPlaced())
            {
                TheRobot.CommandSuccess = false;
                return;
            }

            TheRobot.Turn(Turning.Right);
            TheRobot.CommandSuccess = true;
        }

        public override void Undo()
        {
            if (!TheRobot.IsPlaced()) return;

            TheRobot.Turn(Turning.Left);
        }
    }

    public interface IRightCommand
    {
    }