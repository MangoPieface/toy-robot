using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Domain.Commands
{
    public abstract class RobotCommand
    {
        protected Robot _robot;

        protected RobotCommand(Robot robot)
        {
            _robot = robot;
           
        }

        public abstract void Execute();
    }
}
