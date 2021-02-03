using System;
using System.ComponentModel.Design;
using System.Drawing;
using ToyRobot.Domain.Enums;
using ToyRobot.Domain.Exceptions;
using ToyRobot.Domain.Interfaces;

namespace ToyRobot.Domain
{
    public class Robot : IRobot
    {
        public const string Right = "right";
        public const string Left = "left";

        public Point Position { get; set; }
        public Facing Direction { get; set; }

        public void Move() {
            if (Position == null) throw new ArgumentNullException(nameof(Position));

            switch (Direction)
            {
                case Facing.North:
                    Position = new Point(Position.X, Position.Y + 1);
                    return;
                case Facing.East:
                    Position = new Point(Position.X + 1, Position.Y);
                    return;
                case Facing.South:
                    Position = new Point(Position.X, Position.Y - 1);
                    return;
                case Facing.West:
                    Position = new Point(Position.X - 1, Position.Y);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        }

        public string Report()
        {
            return $"Position X {Position.X}, Position Y {Position.Y}";
        }
    }
}
