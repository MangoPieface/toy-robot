using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Enums;
using ToyRobot.Logic.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace ToyRobot.ConsoleUI
{
    class Program
    {
        static HttpClient _client = new HttpClient();
        const string BaseApiUrl = "https://localhost:44386/api";
        static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IRobot, Robot>(s => new Robot(false));
            serviceCollection.AddSingleton<ITabletop, Tabletop>(s => new Tabletop(4,4));
            serviceCollection.AddSingleton<IMoveCommand, MoveCommand>();
            serviceCollection.AddSingleton<IPlaceCommand, PlaceCommand>();
            serviceCollection.AddSingleton<IRightCommand, RightCommand>();
            serviceCollection.AddScoped<ILeftCommand, LeftCommand>();
            serviceCollection.AddSingleton<IRobotCommander, RobotCommander>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var robot = serviceProvider.GetService<IRobot>();
            var moveCommand = serviceProvider.GetService<IMoveCommand>();
            var rightCommand = serviceProvider.GetService<IRightCommand>();
            var leftCommand = serviceProvider.GetService<ILeftCommand>();
            var placeCommand = serviceProvider.GetService<IPlaceCommand>();
            var commander = serviceProvider.GetService<IRobotCommander>();


            while (true)
            {
                if (!robot.IsPlaced())
                {
                    Console.WriteLine("Robot is not placed on the table - use PLACE command");
                }
                
                Console.Write("Command: ");
                var userInput = Console.ReadLine()?.ToUpper() ?? "";


               if (IsPlaceCommand(userInput))
               { 
                   robot = Place(userInput, robot).GetAwaiter().GetResult();

                   if (robot.CommandSuccess)
                   {
                       Console.WriteLine("PLACED");
                   }
               }

               switch (userInput.ToUpper())
                {
                    case "MOVE":
                        robot = Move(robot).GetAwaiter().GetResult();
                        if (robot.CommandSuccess) Console.WriteLine("MOVED");
                        break;
                    case "RIGHT":
                        commander.Commands.Enqueue(rightCommand as RobotCommand);
                        commander.ExecuteCommands();
                        if (robot.CommandSuccess) Console.WriteLine("TURNED RIGHT");
                        break;
                    case "LEFT":
                        commander.Commands.Enqueue(leftCommand as RobotCommand);
                        commander.ExecuteCommands();
                        if (robot.CommandSuccess) Console.WriteLine("TURNED LEFT");
                        break;
                    case "UNDO":
                        commander.UndoCommands(1);
                        if (robot.CommandSuccess) Console.WriteLine("UNDID LAST COMMAND");
                        break;
                    case "REPORT":
                        if (robot.IsPlaced())
                            Console.WriteLine($"Robot is facing {Enum.GetName(typeof(Facing), robot.RobotPosition.Direction)} X position {robot.RobotPosition.Coordinates.X} Y position {robot.RobotPosition.Coordinates.Y}");
                        break;
                }
            }

        }

        
        static async Task<IRobot> Place(string placeString, IRobot robot)
        {
            
            Position position = ExtractPlacementCommandFromInput(placeString) ?? throw new ArgumentNullException("ExtractPlacementCommandFromInput(placeString, robot)");

            HttpResponseMessage response = await _client.PostAsync(BaseApiUrl + "/place",
                new StringContent(JsonConvert.SerializeObject(position), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseAsText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Robot>(responseAsText);
            }

            robot.CommandSuccess = false;

            return robot;


        }

        static async Task<IRobot> Move(IRobot robot)
        {

            HttpResponseMessage response = await _client.PostAsync(BaseApiUrl + "/move",
                new StringContent(JsonConvert.SerializeObject(robot), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseAsText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Robot>(responseAsText);
            }

            robot.CommandSuccess = false;

            return robot;
        }

        private static Position ExtractPlacementCommandFromInput(string placeString)
        {
            if (!IsPlaceCommand(placeString))
                return null;

            var positionFromInput = placeString.Split(" ").Skip(1).ToList()[0].Split(",");

            var facing = (Facing)Enum.Parse(typeof(Facing), positionFromInput[2], true);

            return new Position
            {
                Coordinates = new Point(int.Parse(positionFromInput[0]), int.Parse(positionFromInput[1])),
                Direction = facing
            };

        }

        private static bool IsPlaceCommand(string placeString)
        {
            return Regex.IsMatch(placeString, @"^PLACE\b\s\d,{1}\d,{1}(?:NORTH|EAST|SOUTH|WEST)$");
        }
    }
}
