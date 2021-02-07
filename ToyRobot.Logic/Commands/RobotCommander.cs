using System.Collections.Generic;

namespace ToyRobot.Logic.Commands
{
    public class RobotCommander
    {
        public Queue<RobotCommand> Commands;
        private Stack<RobotCommand> _undoStack;

        public RobotCommander()
        {
            Commands = new Queue<RobotCommand>();
            _undoStack = new Stack<RobotCommand>();
        }

        public void ExecuteCommands()
        {
            while (Commands.Count > 0)
            {
                RobotCommand command = Commands.Dequeue();
                command.Execute();
                if (command.HasExcecutedSuccesfully())
                {
                    _undoStack.Push(command);
                }
            }
        }

        public void UndoCommands(int numberToUndo)
        {
            while (numberToUndo > 0 && _undoStack.Count > 0)
            {
                RobotCommand command = _undoStack.Pop();
                command.Undo();
                numberToUndo--;
            }
        }
    }
}
