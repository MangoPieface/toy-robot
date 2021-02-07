using System.Drawing;
using ToyRobot.Logic.Enums;

namespace ToyRobot.Logic.Interfaces
{
    public interface IRobot
    {
        Point Position { get; set; }
        Facing Direction { get; set; }

        void Move(Direction direction);

        void Turn(string direction);

        void Place(int x, int y);

        bool IsPlaced();

        bool CommandSuccess { get; set; }


    }
}