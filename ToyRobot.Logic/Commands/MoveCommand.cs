using System.Drawing;
using System.Security.Cryptography;
using ToyRobot.Logic.Enums;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.Logic.Commands
{
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

            if (_robot.RobotPosition.Coordinates.Y < _tabletop.TableDimention.Y && _robot.RobotPosition.Direction == Facing.North)
            {
                _robot.Move(Direction.Forward);
                _robot.CommandSuccess = true;
                return;
            }

            if (_robot.RobotPosition.Coordinates.X < _tabletop.TableDimention.X && _robot.RobotPosition.Direction == Facing.East)
            {
                _robot.Move(Direction.Forward);
                _robot.CommandSuccess = true;
                return;
            }

            if (_robot.RobotPosition.Coordinates.Y > 0 && _robot.RobotPosition.Direction == Facing.South)
            {
                _robot.Move(Direction.Forward);
                _robot.CommandSuccess = true;
                return;
            }


            if (_robot.RobotPosition.Coordinates.X > 0 && _robot.RobotPosition.Direction == Facing.West)
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

        public override IRobot GetRobot()
        {
            return _robot;
        }

        public override void SetRobot(IRobot robot)
        {
            _robot = robot;
        }
    }
}
