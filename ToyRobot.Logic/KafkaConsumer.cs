using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using ToyRobot.Logic.Helpers;

namespace ToyRobot.Logic;

public class KafkaConsumer : IKafkaConsumer
{
    private readonly ILeftCommand _leftCommand;
    private readonly IRightCommand _rightCommand;
    private readonly IMoveCommand _moveCommand;
    private readonly IPlaceCommand _placeCommand;

    public KafkaConsumer(ILeftCommand leftCommand,
        IRightCommand rightCommand, IMoveCommand moveCommand, IPlaceCommand placeCommand)
    {
        _leftCommand = leftCommand;
        _rightCommand = rightCommand;
        _moveCommand = moveCommand;
        _placeCommand = placeCommand;
    }

    public void ProcessFromKafka(IRobotCommander _robotCommand)
    {
        var userInput = "";
        try
        {
            userInput = GetCommandFromKafka();
        }
        catch (Exception ex)
        {
            var a = 1;
        }
        if (PlaceHelper.IsPlaceCommand(userInput))
        {
            var position = userInput.Split(" ").Skip(1).ToList()[0].Split(",");
            _placeCommand.X = int.Parse(position[0]);
            _placeCommand.Y = int.Parse(position[1]);
            _placeCommand.Direction = position[2];
            _placeCommand.Execute();
            return;
        }
        switch (userInput)
        {
            case "MOVE":
                _moveCommand.Execute();
                break;
            case "RIGHT":
                _rightCommand.Execute();
                break;
            case "LEFT":
                _leftCommand.Execute();
                break;
            case "UNDO":
                _robotCommand.UndoCommands(1);
                break;
        }
    }

    private string GetCommandFromKafka()
    {
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
        };

        const string topic = "moves";
        var consumerConfig = new ConsumerConfig(config)
        {
            GroupId = "kafka-dotnet-getting-started",
            AutoOffsetReset = AutoOffsetReset.Earliest, 
        };

        using var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
    
        try
        {
            consumer.Subscribe(topic);
            var cts = SetCancellationToken();
            var cf = consumer.Consume(1000);
            consumer.Close();
            Console.WriteLine($"Received '{cf.Message.Value}' from '{cf.TopicPartitionOffset}'");
            return cf.Message.Value;
        }
        catch (ProduceException<Null, string> e)
        {
            Console.WriteLine($"Received failed: {e.Error.Reason}");
        }

        return null;
    }

    private CancellationTokenSource SetCancellationToken()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true; // prevent the process from terminating.
            cts.Cancel();
        };
        return cts;
    }
}

public interface IKafkaConsumer
{
    void ProcessFromKafka(IRobotCommander commander);
}