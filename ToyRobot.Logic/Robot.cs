using System;
using System.Drawing;
using ToyRobot.Logic.Enums;
using ToyRobot.Logic.Exceptions;
using ToyRobot.Logic.Interfaces;

namespace ToyRobot.Logic
{
    public class Robot : IRobot
    {
        public const string Right = "right";
        public const string Left = "left";

        public Point Position { get; set; }
        public Facing Direction { get; set; }
        private bool RobotPlaced { get; set; }

        public Robot()
        {
            RobotPlaced = false;
        }
        public void Move(Direction direction) {
            if (Position == null) throw new ArgumentNullException(nameof(Position));

            var movement = GetMovementDirection(direction);

            switch (Direction)
            {
                case Facing.North:
                    Position = new Point(Position.X, Position.Y + movement);
                    return;
                case Facing.East:
                    Position = new Point(Position.X + movement, Position.Y);
                    return;
                case Facing.South:
                    Position = new Point(Position.X, Position.Y - movement);
                    return;
                case Facing.West:
                    Position = new Point(Position.X - movement, Position.Y);
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

        public void Place(int x, int y)
        {
            Position = new Point(x, y);
            Direction = Facing.North;
            RobotPlaced = true;
        }

        public void UnPlace()
        {
            RobotPlaced = false;
        }

        public bool IsPlaced()
        {
            return RobotPlaced;
        }
    }
}
