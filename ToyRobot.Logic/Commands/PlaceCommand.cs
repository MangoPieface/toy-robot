using System;
using System.Globalization;
using ToyRobot.Logic.Enums;

namespace ToyRobot.Logic.Commands
{
    public class PlaceCommand : RobotCommand
    {
        private readonly Tabletop _tabletop;
        public int X { get; set; }
        public int Y { get; set; }

        public string Direction { get; set; }

        public PlaceCommand(Robot robot, Tabletop tabletop) : base(robot)
        {
            _tabletop = tabletop;
        }

        public override void Execute()
        {
            if (X > _tabletop.TableDimention.X && Y > _tabletop.TableDimention.Y)
            {
                _robot.CommandSuccess = false;
                return;
            }

            var direction = (Facing) Enum.Parse(typeof(Facing),
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Direction.ToLower()));
            _robot.Place(X, Y, direction);
            _robot.CommandSuccess = true;
        }

        public override void Undo()
        {
            _robot.UnPlace();
        }
    }
}
