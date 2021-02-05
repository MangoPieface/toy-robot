using System;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Enums;

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
                if (!robot.IsPlaced())
                {
                    Console.WriteLine("Robot is not placed on the table - use PLACE command");
                }

               

                Console.Write("Command: ");
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
                        Console.WriteLine("PLACED");
                        break;
                    case "MOVE":
                        MoveCommand move = new MoveCommand(robot, table);
                        commander.Commands.Enqueue(move);
                        if (robot.IsPlaced()) Console.WriteLine("MOVED");
                        break;
                    case "RIGHT":
                        RightCommand right = new RightCommand(robot);
                        commander.Commands.Enqueue(right);
                        if (robot.IsPlaced()) Console.WriteLine("TURNED RIGHT");
                        break;
                    case "LEFT":
                        LeftCommand left = new LeftCommand(robot);
                        commander.Commands.Enqueue(left);
                        if (robot.IsPlaced()) Console.WriteLine("TURNED LEFT");
                        break;
                    case "UNDO":
                        commander.UndoCommands(1);
                        if (robot.IsPlaced()) Console.WriteLine("UNDID LAST COMMAND");
                        break;
                    case "REPORT":
                        if (robot.IsPlaced())
                            Console.WriteLine($"Robot is facing {Enum.GetName(typeof(Facing), robot.Direction)} X position {robot.Position.X} Y position {robot.Position.Y}");
                        break;

                }

                commander.ExecuteCommands();

            }

        }
    }
}
