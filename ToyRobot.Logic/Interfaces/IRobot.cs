namespace ToyRobot.Logic.Interfaces;
public interface IRobot
{
    Point Position { get; set; }
    Facing Direction { get; set; }

    void Move(Direction direction);

    void Turn(Turning direction);

    void Place(int x, int y, Facing facing);

    bool IsPlaced();

    bool CommandSuccess { get; set; }

    void UnPlace();


}