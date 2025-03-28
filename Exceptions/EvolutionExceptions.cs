using System;

namespace Evolution.ClientAPI.Exceptions
{
    public class EvolutionAPIError : Exception
    {
        public EvolutionAPIError(string message) : base(message) { }
        public EvolutionAPIError(string message, Exception innerException) : base(message, innerException) { }
    }

    public class EvolutionAuthenticationError : EvolutionAPIError
    {
        public EvolutionAuthenticationError(string message) : base(message) { }
    }

    public class EvolutionNotFoundError : EvolutionAPIError
    {
        public EvolutionNotFoundError(string message) : base(message) { }
    }
} 