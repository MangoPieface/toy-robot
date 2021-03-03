using Microsoft.AspNetCore.Mvc;
using System;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeftController : ControllerBase
    {
        private readonly ILeftCommand _leftCommand;
        private readonly IRobotCommander _commander;

        public LeftController(ILeftCommand leftCommand, IRobotCommander commander)
        {
             _leftCommand= leftCommand;
             _commander = commander;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello left";
        }
        [HttpPost]
        public IRobot Post(Robot robot)
        {
            var command = (RobotCommand) _leftCommand;
            if (command == null) throw new ArgumentNullException(nameof(command));

            command.SetRobot(robot);
            _commander.Commands.Enqueue(command);
            _commander.ExecuteCommands();

            return command.GetRobot();
        }
    }
}
