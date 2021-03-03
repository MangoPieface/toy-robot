using Microsoft.AspNetCore.Mvc;
using System;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RightController : ControllerBase
    {
        private readonly IRightCommand _rightCommand;
        private readonly IRobotCommander _commander;

        public RightController(IRightCommand rightCommand, IRobotCommander commander)
        {
            _rightCommand = rightCommand;
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
            var command = (RobotCommand) _rightCommand;
            if (command == null) throw new ArgumentNullException(nameof(command));

            command.SetRobot(robot);
            _commander.Commands.Enqueue(command);
            _commander.ExecuteCommands();

            return command.GetRobot();
        }
    }
}
