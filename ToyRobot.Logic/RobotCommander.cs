using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Domain.Commands;

namespace ToyRobot.Domain
{
    public class RobotCommander
    {
        public Queue<RobotCommand> Commands;

        public RobotCommander()
        {
            Commands = new Queue<RobotCommand>();
        }

        public void ExecuteCommands()
        {
            while (Commands.Count > 0)
            {
                RobotCommand command = Commands.Dequeue();
                command.Execute();
            }
        }
    }
}
