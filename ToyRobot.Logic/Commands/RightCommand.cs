using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Domain.Commands
{
    public class RightCommand : RobotCommand
    {

        public RightCommand(Robot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            _robot.Turn(Robot.Right);
        }
    }
}
