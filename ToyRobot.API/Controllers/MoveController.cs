using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly IMoveCommand _moveCommand;
        private readonly IRobot _robot;
        private readonly IRobotCommander _commander;

        public MoveController(IMoveCommand moveCommand, IRobot robot, IRobotCommander commander)
        {
            _moveCommand = moveCommand;
            _robot = robot;
            _commander = commander;
        }

        [HttpPost]
        public Robot Post(Robot robot)
        {
            _moveCommand.SetRobot(robot);
            _commander.Commands.Enqueue(_moveCommand as RobotCommand);
            _commander.ExecuteCommands();

            return _robot as Robot;
        }
    }
}
