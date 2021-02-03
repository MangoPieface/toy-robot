using System;
using System.ComponentModel.DataAnnotations.Schema;
using ToyRobot.Domain;
using ToyRobot.Domain.Commands;
using ToyRobot.Domain.Enums;

namespace ToyRobot.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot robot = new Robot();
            Tabletop table = new Tabletop(5,5);
            
            RobotCommander commander = new RobotCommander();

            while (true)
            {
                Console.WriteLine("Command?");
                var movementCommand = Console.ReadLine()?.ToUpper();

                switch (movementCommand)
                {
                    case "PLACE":
                        PlaceCommand place = new PlaceCommand(robot)
                        {
                            X = 0,
                            Y = 0
                        };
                        commander.Commands.Enqueue(place);
                        break;
                    case "MOVE":
                        MoveCommand move = new MoveCommand(robot, table);
                        commander.Commands.Enqueue(move);
                        break;
                    case "RIGHT":
                        RightCommand right = new RightCommand(robot);
                        commander.Commands.Enqueue(right);
                        break;
                    case "LEFT":
                        LeftCommand left = new LeftCommand(robot);
                        commander.Commands.Enqueue(left);
                        break;
                    case "REPORT":
                        Console.WriteLine($"Robot is facing {Enum.GetName(typeof(Facing), robot.Direction)} X position {robot.Position.X} Y position {robot.Position.Y} ");
                        break;

                }

                commander.ExecuteCommands();

            }

        }
    }
}
