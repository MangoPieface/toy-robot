namespace ToyRobot.Logic;

public class Robot : IRobot
{
    // public const string Right = "right";
    // public const string Left = "left";
    public Point Position { get; set; }
    public Facing Direction { get; set; }
    private bool RobotPlaced { get; set; }

    public bool CommandSuccess { get; set; }

    public Robot()
    {
        RobotPlaced = false;
    }
    public void Move(Direction direction) {

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

    public void Turn(Turning turningDirection)
    {
        switch (Direction)
        {
            case Facing.North:
                Direction = turningDirection == Turning.Right ? Facing.East : Facing.West;
                return;
            case Facing.East:
                Direction = turningDirection == Turning.Right ? Facing.South : Facing.North;
                return;
            case Facing.South:
                Direction = turningDirection == Turning.Right ? Facing.West : Facing.East;
                return;
            case Facing.West:
                Direction = turningDirection == Turning.Right ? Facing.North : Facing.South;
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Place(int x, int y, Facing direction)
    {
        Position = new Point(x, y);
        Direction = direction;
        RobotPlaced = true;
    }

    public void UnPlace()
    {
        RobotPlaced = false;
        Position = Point.Empty;

    }

    public bool IsPlaced()
    {
        return RobotPlaced;
    }

    
}
