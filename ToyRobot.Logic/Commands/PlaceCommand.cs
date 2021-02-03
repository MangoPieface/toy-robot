using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Domain.Commands
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
    }
}
