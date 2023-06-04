namespace ToyRobot.Logic.Commands;

    public class RobotCommander : IRobotCommander
    {
        public Queue<RobotCommand> Commands { get; set; }
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
                if (command.HasExecutedSuccessfully())
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

    public interface IRobotCommander
    {
        Queue<RobotCommand> Commands { get; set; }
        void ExecuteCommands();
        void UndoCommands(int numberToUndo);
    }