using System;
using ToyRobot.Domain;
using ToyRobot.Domain.Enums;
using ToyRobot.Domain.Exceptions;
using ToyRobot.Domain.Interfaces;
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
        public void TestPlaceandMove()
        {
            IRobot robot = new Robot
            {
                Direction = Facing.North
            };

            robot.Place(0, 0);
            robot.Move();
            var a = robot.Report();
            robot.Turn("right");
            robot.Move();
            robot.Move();
            a = robot.Report();


        }
    }
}
