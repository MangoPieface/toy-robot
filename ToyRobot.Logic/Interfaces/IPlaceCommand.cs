namespace ToyRobot.Logic.Interfaces;

public interface IPlaceCommand
{
    int X { get; set; }
    int Y { get; set; }
    string Direction { get; set; }
    void Execute();
}