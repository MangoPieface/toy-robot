namespace ToyRobot.Logic.Exceptions;
public class TurnParameterException : ArgumentException
{
    public TurnParameterException(string message)
        : base(message)
    {
    }

}
