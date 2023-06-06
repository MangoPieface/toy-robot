
namespace ToyRobot.Logic.Commands;

public class RobotCommander : IRobotCommander
    {
        private readonly IKafkaConsumer _kafkaConsumer;
        public Queue<RobotCommand> Commands { get; set; }
        private readonly Stack<RobotCommand> _undoStack;

        
        public RobotCommander(IKafkaConsumer kafkaConsumer)
        {
            _kafkaConsumer = kafkaConsumer;
            Commands = new Queue<RobotCommand>();
            _undoStack = new Stack<RobotCommand>();
        }

        public void ExecuteCommands()
        {
            _kafkaConsumer.ProcessFromKafka(this);
            //RobotCommand command = Commands.Dequeue();
            //command.Execute();
            // if (command.HasExecutedSuccessfully())
            // {
            //     _undoStack.Push(command);
            // }
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