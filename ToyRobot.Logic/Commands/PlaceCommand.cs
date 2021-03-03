using System;
using System.Globalization;
using ToyRobot.Logic.Enums;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.Logic.Commands
{
    public class PlaceCommand : RobotCommand, IPlaceCommand
    {
        private readonly ITabletop _tabletop;
        public  Position PlacePosition { get; set; }

        public PlaceCommand(IRobot robot, ITabletop tabletop) : base(robot)
        {
            _tabletop = tabletop;
        }

        public override void Execute()
        {

            if (PlacePosition.Coordinates.X > _tabletop.TableDimention.X && PlacePosition.Coordinates.Y > _tabletop.TableDimention.Y)
            {
                _robot.CommandSuccess = false;
                return;
            }

            _robot.Place(PlacePosition);
            _robot.CommandSuccess = true;
        }

        public override void Undo()
        {
            _robot.UnPlace();
        }
    }
}
