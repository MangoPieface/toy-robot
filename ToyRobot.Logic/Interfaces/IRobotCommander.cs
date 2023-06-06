using ToyRobot.Logic.Commands;

namespace ToyRobot.Logic.Interfaces;

public interface IRobotCommander
{
    Queue<RobotCommand> Commands { get; }
    void ExecuteCommands();
    void UndoCommands(int numberToUndo);
}