using ToyRobot.Logic.Commands;

namespace ToyRobot.Logic.Interfaces
{
    public interface IPlaceCommand
    {
        Position PlacePosition { get; set; }
    }
}