using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Domain.Commands
{
    public class LeftCommand : RobotCommand
    {

        public LeftCommand(Robot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            _robot.Turn(Robot.Left);
        }
    }
}
