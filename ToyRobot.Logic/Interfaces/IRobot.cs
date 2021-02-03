using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using ToyRobot.Domain.Enums;

namespace ToyRobot.Domain.Interfaces
{
    public interface IRobot
    {
        Point Position { get; set; }
        Facing Direction { get; set; }

        void Move();

        void Turn(string direction);

        void Place(int x, int y);

        string Report();


    }
}