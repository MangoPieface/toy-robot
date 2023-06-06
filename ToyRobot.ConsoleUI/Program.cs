using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ToyRobot.Logic.Helpers;


var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IRobot, Robot>();
serviceCollection.AddSingleton<ITabletop, Tabletop>(s => new Tabletop(4,4));
serviceCollection.AddSingleton<IMoveCommand, MoveCommand>();
serviceCollection.AddSingleton<IPlaceCommand, PlaceCommand>();
serviceCollection.AddSingleton<IRightCommand, RightCommand>();
serviceCollection.AddSingleton<ILeftCommand, LeftCommand>();
serviceCollection.AddSingleton<IRobotCommander, RobotCommander>();
serviceCollection.AddSingleton<IKafkaConsumer, KafkaConsumer>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var robot = serviceProvider.GetService<IRobot>();
var moveCommand = serviceProvider.GetService<IMoveCommand>();
var rightCommand = serviceProvider.GetService<IRightCommand>();
var leftCommand = serviceProvider.GetService<ILeftCommand>();
var placeCommand = serviceProvider.GetService<IPlaceCommand>();
var commander = serviceProvider.GetService<IRobotCommander>();
var kafkaConsumer = serviceProvider.GetService<IKafkaConsumer>();

var builder = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true);

var configFile = builder.Build();

var kafkaServer = configFile["Kafka:Server"];
var username  = configFile["Kafka:SaslUsername"];
var password  = configFile["Kafka:SaslPassword"];

var config = new ProducerConfig { 
    BootstrapServers = kafkaServer, 
    SaslUsername = username, 
    SaslPassword  = password,
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SaslMechanism = SaslMechanism.Plain,
    RequestTimeoutMs = 4500
};

while (true)
{
    if (!robot.IsPlaced())
    {
        Console.WriteLine("Robot is not placed on the table - use PLACE command");
    }
                
    Console.Write("Command: ");
    var userInput = Console.ReadLine()?.ToUpper() ?? "";


    //TODO MOVE THIS SOMEWHERE ELSE WHEN IT'S NOT BEDTIME
    var isPlaceCommand = PlaceHelper.IsPlaceCommand(userInput);
    if (isPlaceCommand)
    {

        
        //commander.Commands.Enqueue(placeCommand as RobotCommand);
        await QueueCommandToKafka(config, userInput.ToUpper());
        commander.ExecuteCommands();

        if (robot.CommandSuccess)
            Console.WriteLine("PLACED");
    }
    
    switch (userInput.ToUpper())
    {
        case "MOVE":
            await QueueCommandToKafka(config, "MOVE");
            commander.ExecuteCommands();
            break;
        case "RIGHT":
            await QueueCommandToKafka(config, "RIGHT");
            break;
        case "LEFT":
            await QueueCommandToKafka(config, "LEFT");
            break;
        case "UNDO":
            commander.UndoCommands(1);
            if (robot.CommandSuccess) Console.WriteLine("UNDID LAST COMMAND");
            break;
        case "REPORT":
            if (robot.IsPlaced())
                Console.WriteLine($"Robot is facing {Enum.GetName(typeof(Facing), robot.Direction)} X position {robot.Position.X} Y position {robot.Position.Y}");
            break;
    }
}

async Task QueueCommandToKafka(ProducerConfig producerConfig, string robotCommand)
{
    const string topic = "moves";
    
    using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    try
    {
        var dr = await producer.ProduceAsync(topic, new Message<Null, string> { Value = robotCommand });
        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
    }
    catch (ProduceException<Null, string> e)
    {
        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
    }
}