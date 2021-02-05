namespace ToyRobot.Logic.Commands
{
    public class PlaceCommand : RobotCommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PlaceCommand(Robot robot) : base(robot)
        {

        }

        public override void Execute()
        {
            _robot.Place(X, Y);
        }

        public override void Undo()
        {
            _robot.UnPlace();
        }
    }
}
