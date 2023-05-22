namespace ToyRobot.Logic.Commands;
    public class MoveCommand : RobotCommand, IMoveCommand
    {
        private readonly ITabletop _tabletop;


        public MoveCommand(IRobot robot, ITabletop tabletop) : base(robot)
        {
            _tabletop = tabletop;
        }

        public override void Execute()
        {
            if (!_robot.IsPlaced())
            {
                _robot.CommandSuccess = false;
                return;
            }

            if (_robot.Position.Y < _tabletop.TableDimention.Y && _robot.Direction == Facing.North)
            {
                _robot.Move(Direction.Forward);
                _robot.CommandSuccess = true;
                return;
            }

            if (_robot.Position.X < _tabletop.TableDimention.X && _robot.Direction == Facing.East)
            {
                _robot.Move(Direction.Forward);
                _robot.CommandSuccess = true;
                return;
            }

            if (_robot.Position.Y > 0 && _robot.Direction == Facing.South)
            {
                _robot.Move(Direction.Forward);
                _robot.CommandSuccess = true;
                return;
            }


            if (_robot.Position.X > 0 && _robot.Direction == Facing.West)
            {
                _robot.Move(Direction.Forward);
                _robot.CommandSuccess = true;
                return;
            }

            _robot.CommandSuccess = false;
        }


        public override void Undo()
        {
            if (!_robot.IsPlaced())
            {
                return;
            }


            _robot.Move(Direction.Backward);

        }
    }

    public interface IMoveCommand
    {
    }
