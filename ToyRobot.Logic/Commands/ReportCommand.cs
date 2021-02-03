using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Domain.Commands
{
    public  class ReportCommand :RobotCommand
    {
        public ReportCommand(Robot robot) : base(robot)
        {
        }

        public override void Execute()
        {
           _robot.Report();
        }
    }
}
