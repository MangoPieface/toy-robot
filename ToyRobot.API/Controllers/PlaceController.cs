using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {

        private readonly ILogger<PlaceController> _logger;
        private readonly IRobot _robot;
        private readonly IPlaceCommand _placeCommand;
        private readonly IRobotCommander _commander;

        public PlaceController(ILogger<PlaceController> logger, IRobot robot, IPlaceCommand placeCommand, IRobotCommander commander)
        {
            _logger = logger;
            _robot = robot;
            _placeCommand = placeCommand;
            _commander = commander;
        }

        //[HttpGet]
        //public Robot Get()
        //{
        //    return new Robot();
        //}

        [HttpGet]
        public Robot Get()
        {
            return new Robot(false);
        }



        [HttpPost]
        public Robot Post(Position position)
        {
            _placeCommand.PlacePosition = position;
            _commander.Commands.Enqueue(_placeCommand as RobotCommand);
            _commander.ExecuteCommands();
            

            return _robot as Robot;
        }

    }
}
