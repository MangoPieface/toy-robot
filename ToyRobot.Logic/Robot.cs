using System;
using System.Drawing;
using Newtonsoft.Json;
using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Enums;
using ToyRobot.Logic.Exceptions;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.Logic
{
    public class Robot : IRobot
    {
        public const string Right = "right";
        public const string Left = "left";

        public Position RobotPosition { get; set; }
        public Facing Direction { get; set; }

        //[JsonProperty]
        public  bool RobotPlaced { get; set; }

        public bool CommandSuccess { get; set; }

        public Robot()
        {
          
        }

        public Robot(bool robotPlaced)
        {
            RobotPlaced = robotPlaced;
        }
        public void Move(Direction direction) {
            if (RobotPosition == null) throw new ArgumentNullException(nameof(RobotPosition));

            var movement = GetMovementDirection(direction);

            switch (Direction)
            {
                case Facing.North:
                    RobotPosition.Coordinates = new Point(RobotPosition.Coordinates.X, RobotPosition.Coordinates.Y + movement);
                    return;
                case Facing.East:
                    RobotPosition.Coordinates = new Point(RobotPosition.Coordinates.X + movement, RobotPosition.Coordinates.Y);
                    return;
                case Facing.South:
                    RobotPosition.Coordinates = new Point(RobotPosition.Coordinates.X, RobotPosition.Coordinates.Y - movement);
                    return;
                case Facing.West:
                    RobotPosition.Coordinates = new Point(RobotPosition.Coordinates.X - movement, RobotPosition.Coordinates.Y);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static int GetMovementDirection(Direction direction)
        {
            int movement = 1;
            if (direction == Enums.Direction.Backward)
            {
                movement = -1;
            }

            return movement;
        }

        public void Turn(string turnDirection)
        {
            if (turnDirection == null) throw new ArgumentNullException(nameof(turnDirection));
            if (turnDirection.ToLower() != Right && Left != turnDirection.ToLower())
                throw new TurnParameterException("Turn direction can only be 'left' or 'right");

            turnDirection = turnDirection.ToLower();

            switch (Direction)
            {
                case Facing.North:
                    Direction = turnDirection == Right ? Facing.East : Facing.West;
                    return;
                case Facing.East:
                    Direction = turnDirection == Right ? Facing.South : Facing.North;
                    return;
                case Facing.South:
                    Direction = turnDirection == Right ? Facing.West : Facing.East;
                    return;
                case Facing.West:
                    Direction = turnDirection == Right ? Facing.North : Facing.South;
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Place(Position robotPosition)
        {
            RobotPosition = robotPosition;
            RobotPlaced = true;
        }


        public void UnPlace()
        {
            RobotPlaced = false;
            RobotPosition = null;

        }

        public bool IsPlaced()
        {
            return RobotPlaced;
        }

       
    }
}
