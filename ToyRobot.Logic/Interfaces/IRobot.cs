using ToyRobot.Logic.Commands;
using ToyRobot.Logic.Enums;

namespace ToyRobot.Logic.Interfaces
{
    public interface IRobot
    {
        Position RobotPosition { get; set; }
        
        void Move(Direction direction);

        void Turn(string direction);

        void Place(Position robotPosition);

        bool IsPlaced();

        bool CommandSuccess { get; set; }

        void UnPlace();


    }
}