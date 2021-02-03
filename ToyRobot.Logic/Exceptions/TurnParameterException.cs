using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Domain.Exceptions
{
    public class TurnParameterException : ArgumentException
    {
        public TurnParameterException(string message)
            : base(message)
        {
        }

    }
}
