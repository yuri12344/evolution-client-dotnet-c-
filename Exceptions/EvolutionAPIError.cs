using System;

namespace Evolution.ClientAPI.Exceptions // << Namespace Ajustado
{
    /// <summary>
    /// Exceção base para erros ocorridos durante a comunicação com a Evolution API.
    /// </summary>
    public class EvolutionAPIError : Exception
    {
        public EvolutionAPIError(string message) : base(message)
        {
        }

        public EvolutionAPIError(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}