using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Enums;
using ToyRobot.Logic.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToyRobot.ConsoleUI
{
    class Program
    {
        static readonly HttpClient Client = new HttpClient();
        const string BaseApiUrl = "https://localhost:44386/api/";
        static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IRobot, Robot>(s => new Robot(false));
            serviceCollection.AddSingleton<IRobotCommander, RobotCommander>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var robot = serviceProvider.GetService<IRobot>();
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
                   Position position = ExtractPlacementCommandFromInput(userInput);

                   robot = ProcessPlaceWithAPI(position, "place").GetAwaiter().GetResult();

                    if (robot.CommandSuccess)
                    {
                        Console.WriteLine("PLACED");
                    }
               }

               switch (userInput.ToUpper())
                {
                    case "MOVE":
                        robot = ProcessMovementWithAPI(robot, "move").GetAwaiter().GetResult();
                        if (robot.CommandSuccess) Console.WriteLine("MOVED");
                        break;
                    case "RIGHT":
                        robot = ProcessMovementWithAPI(robot, "right").GetAwaiter().GetResult();
                        if (robot.CommandSuccess) Console.WriteLine("TURNED RIGHT");
                        break;
                    case "LEFT":
                        robot = ProcessMovementWithAPI(robot, "left").GetAwaiter().GetResult();
                        if (robot.CommandSuccess) Console.WriteLine("TURNED LEFT");
                        break;
                    case "UNDO":
                        //todo this needs to be reimplemented in another way with APIs
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

        
        private static Position ExtractPlacementCommandFromInput(string placeString)
        {

            var positionFromInput = placeString.Split(" ").Skip(1).ToList()[0].Split(",");

            var facing = (Facing)Enum.Parse(typeof(Facing), positionFromInput[2], true);

            return new Position
            {
                Coordinates = new Point(int.Parse(positionFromInput[0]), int.Parse(positionFromInput[1])),
                Direction = facing
            };

        }


        static async Task<IRobot> ProcessPlaceWithAPI(Position position, string apiEndPoint)
        {
            HttpResponseMessage response = await Client.PostAsync(BaseApiUrl + apiEndPoint,
                new StringContent(JsonConvert.SerializeObject(position), Encoding.UTF8, "application/json"));

            return await ProcessResponse(response);
        }

        static async Task<IRobot> ProcessMovementWithAPI(IRobot robot, string apiEndPoint)
        {
            HttpResponseMessage response = await Client.PostAsync(BaseApiUrl + apiEndPoint,
                new StringContent(JsonConvert.SerializeObject(robot), Encoding.UTF8, "application/json"));

            return await ProcessResponse(response);
        }

        private static async Task<IRobot> ProcessResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseAsText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Robot>(responseAsText);
            }

            return new Robot {CommandSuccess = false};
        }

        private static bool IsPlaceCommand(string placeString)
        {
            return Regex.IsMatch(placeString, @"^PLACE\b\s\d,{1}\d,{1}(?:NORTH|EAST|SOUTH|WEST)$");
        }
    }
}
