using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Enums;

namespace ToyRobot.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot robot = new Robot();
            Tabletop table = new Tabletop(4, 4);

            RobotCommander commander = new RobotCommander();

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

                    PlaceCommand place = new PlaceCommand(robot, table)
                    {
                        X = (int.Parse(position[0])),
                        Y = (int.Parse(position[1])),
                        Direction = position[2]
                    };
                    commander.Commands.Enqueue(place);
                    commander.ExecuteCommands();

                    if (robot.CommandSuccess)
                        Console.WriteLine("PLACED");
                }



                switch (movementCommand.ToUpper())
                {
                    case "MOVE":
                        MoveCommand move = new MoveCommand(robot, table);
                        commander.Commands.Enqueue(move);
                        commander.ExecuteCommands();
                        if (robot.CommandSuccess) Console.WriteLine("MOVED");
                        break;
                    case "RIGHT":
                        RightCommand right = new RightCommand(robot);
                        commander.Commands.Enqueue(right);
                        commander.ExecuteCommands();
                        if (robot.CommandSuccess) Console.WriteLine("TURNED RIGHT");
                        break;
                    case "LEFT":
                        LeftCommand left = new LeftCommand(robot);
                        commander.Commands.Enqueue(left);
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
