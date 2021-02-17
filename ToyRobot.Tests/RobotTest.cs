using System;
using ToyRobot.Logic;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Enums;
using ToyRobot.Logic.Exceptions;
using ToyRobot.Logic.Interfaces;
using Xunit;

namespace ToyRobot.Tests
{
    public class RobotTest
    {
        [Fact]
        public void Robot_RobotFacesNorthAndTurnsRight_RobotFacesEast()
        {
            IRobot robot = new Robot
            {
                Direction = Facing.North
            };

            robot.Turn("right");
            Assert.Equal(Facing.East, robot.Direction);
        }

        [Fact]
        public void Robot_RobotFacesNorthAndTurnsRightTwice_RobotFacesSouth()
        {
            IRobot robot = new Robot
            {
                Direction = Facing.North
            };
            robot.Turn("right");
            robot.Turn("right");

            Assert.Equal(Facing.South, robot.Direction);
        }

        [Fact]
        public void Robot_RobotFacesNorthhAndTurnsLeftOnce_RobotFacesWest()
        {
            IRobot robot = new Robot
            {
                Direction = Facing.North
            };
            robot.Turn("left");

            Assert.Equal(Facing.West, robot.Direction);
        }

        [Fact]
        public void Robot_RobotFacesNorthAndPassesNull_RobotReportsException()
        {
            IRobot robot = new Robot
            {
                Direction = Facing.North
            };

            Assert.Throws<ArgumentNullException>(() => robot.Turn(null));
        }

        [Fact]
        public void Robot_RobotFacesNorthAndPassesInvalidInput_RobotReportsException()
        {
            IRobot robot = new Robot
            {
                Direction = Facing.North
            };

            Assert.Throws<TurnParameterException>(() => robot.Turn("abc"));
        }

        [Fact]
        public void Robot_TestMultipleMovement_RobotReportsCorrectLocation()
        {
            Robot robot = new Robot();
            Tabletop table = new Tabletop(5, 5);
            RobotCommander commander = new RobotCommander();

            PlaceCommand place = new PlaceCommand(robot, table);
            place.Direction = "North";
            MoveCommand move = new MoveCommand(robot, table);
            RightCommand right = new RightCommand(robot);
            LeftCommand left = new LeftCommand(robot);


            commander.Commands.Enqueue(place);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(right);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(left);
            commander.Commands.Enqueue(left);

            commander.ExecuteCommands();

            Assert.Equal(Facing.West,robot.Direction);
            Assert.Equal(2, robot.Position.Y);
            Assert.Equal(1, robot.Position.X);
        }

        [Fact]
        public void Robot_TestMultipleMovementWithUndos_RobotReportsOrignalPosition()
        {
            Robot robot = new Robot();
            Tabletop table = new Tabletop(5, 5);
            RobotCommander commander = new RobotCommander();

            PlaceCommand place = new PlaceCommand(robot, table);
            place.Direction = "North";
            MoveCommand move = new MoveCommand(robot, table);
            RightCommand right = new RightCommand(robot);
            LeftCommand left = new LeftCommand(robot);


            commander.Commands.Enqueue(place);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(right);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(left);
            commander.Commands.Enqueue(left);
            commander.Commands.Enqueue(left);
            commander.Commands.Enqueue(move);

            commander.ExecuteCommands();
            commander.UndoCommands(8);

            Assert.Equal(Facing.North, robot.Direction);
            Assert.Equal(0, robot.Position.Y);
            Assert.Equal(0, robot.Position.X);
        }
        
        [Fact]
        public void Robot_TestUndoWhenWhenRobotIsStuckAgainstNorthWall_RobotReportsOrignalPosition()
        {
            Robot robot = new Robot();
            Tabletop table = new Tabletop(5, 5);
            RobotCommander commander = new RobotCommander();

            PlaceCommand place = new PlaceCommand(robot, table);
            place.Direction = "North";
            MoveCommand move = new MoveCommand(robot, table);
            RightCommand right = new RightCommand(robot);
            LeftCommand left = new LeftCommand(robot);


            commander.Commands.Enqueue(place);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);

            commander.ExecuteCommands();
            commander.UndoCommands(7);

            Assert.Equal(Facing.North, robot.Direction);
            Assert.Equal(0, robot.Position.Y);
            Assert.Equal(0, robot.Position.X);
        }

        [Fact]
        public void Robot_TestUndoWhenWhenRobotIsStuckAgainstWestWall_RobotReportsOrignalPosition()
        {
            Robot robot = new Robot();
            Tabletop table = new Tabletop(5, 5);
            RobotCommander commander = new RobotCommander();

            PlaceCommand place = new PlaceCommand(robot, table);
            place.Direction = "North";
            MoveCommand move = new MoveCommand(robot, table);
            RightCommand right = new RightCommand(robot);
            LeftCommand left = new LeftCommand(robot);


            commander.Commands.Enqueue(place);
            commander.Commands.Enqueue(left);
            commander.Commands.Enqueue(move);

            commander.ExecuteCommands();
            commander.UndoCommands(2);

            Assert.Equal(Facing.North, robot.Direction);
            Assert.Equal(0, robot.Position.Y);
            Assert.Equal(0, robot.Position.X);
        }

        [Fact]
        public void Robot_TestUndoWhenWhenRobotIsStuckAgainstEastWall_RobotReportsOrignalPosition()
        {
            Robot robot = new Robot();
            Tabletop table = new Tabletop(5, 5);
            RobotCommander commander = new RobotCommander();

            PlaceCommand place = new PlaceCommand(robot, table);
            place.Direction = "North";
            MoveCommand move = new MoveCommand(robot, table);
            RightCommand right = new RightCommand(robot);
            LeftCommand left = new LeftCommand(robot);


            commander.Commands.Enqueue(place);
            commander.Commands.Enqueue(right);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);
            commander.Commands.Enqueue(move);

            commander.ExecuteCommands();
            commander.UndoCommands(6);

            Assert.Equal(0, robot.Position.Y);
            Assert.Equal(0, robot.Position.X);
        }

        [Fact]
        public void Robot_TestUndoWhenWhenRobotIsStuckAgainstSouthWall_RobotReportsOrignalPosition()
        {
            Robot robot = new Robot();
            Tabletop table = new Tabletop(5, 5);
            RobotCommander commander = new RobotCommander();

            PlaceCommand place = new PlaceCommand(robot, table);
            place.Direction = "North";
            MoveCommand move = new MoveCommand(robot, table);
            RightCommand right = new RightCommand(robot);
            LeftCommand left = new LeftCommand(robot);


            commander.Commands.Enqueue(place);
            commander.Commands.Enqueue(right);
            commander.Commands.Enqueue(right);
            commander.Commands.Enqueue(move);

            commander.ExecuteCommands();
            commander.UndoCommands(1);

            Assert.Equal(0, robot.Position.Y);
            Assert.Equal(0, robot.Position.X);
        }
    }
}
