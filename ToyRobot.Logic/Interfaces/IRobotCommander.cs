using System.Collections.Generic;
using ToyRobot.Logic.Commands;

namespace ToyRobot.Logic.Interfaces
{
    public interface IRobotCommander
    {
        Queue<RobotCommand> Commands { get; set; }
        void ExecuteCommands();
        void UndoCommands(int numberToUndo);
    }
}