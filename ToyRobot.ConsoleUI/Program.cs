using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Enums;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IRobot, Robot>();
            serviceCollection.AddSingleton<ITabletop, Tabletop>(s => new Tabletop(4,4));
            serviceCollection.AddSingleton<IMoveCommand, MoveCommand>();
            serviceCollection.AddSingleton<IPlaceCommand, PlaceCommand>();
            serviceCollection.AddSingleton<IRightCommand, RightCommand>();
            serviceCollection.AddSingleton<ILeftCommand, LeftCommand>();
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
                var movementCommand = Console.ReadLine()?.ToUpper() ?? "";


                //TODO MOVE THIS SOMEWHERE ELSE WHEN IT'S NOT BEDTIME
                var isPlaceCommand = Regex.IsMatch(movementCommand, @"^PLACE\b\s\d,{1}\d,{1}(?:NORTH|EAST|SOUTH|WEST)$");
                if (isPlaceCommand)
                {
                    var position = movementCommand.Split(" ").Skip(1).ToList()[0].Split(",");
                    placeCommand.X = int.Parse(position[0]);
                    placeCommand.Y = int.Parse(position[1]);
                    placeCommand.Direction = position[2];

                    commander.Commands.Enqueue(placeCommand as RobotCommand);
                    commander.ExecuteCommands();

                    if (robot.CommandSuccess)
                        Console.WriteLine("PLACED");
                }

                switch (movementCommand.ToUpper())
                {
                    case "MOVE":
                        commander.Commands.Enqueue(moveCommand as RobotCommand);
                        commander.ExecuteCommands();
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
                            Console.WriteLine($"Robot is facing {Enum.GetName(typeof(Facing), robot.Direction)} X position {robot.Position.X} Y position {robot.Position.Y}");
                        break;
                }
            }

        }
    }
}
