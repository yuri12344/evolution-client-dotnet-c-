// Evolution.ClientAPI/Services/MessageService.cs
using System.Threading.Tasks;
using Evolution.ClientAPI.Models; // Para os models de request/response

namespace Evolution.ClientAPI.Services
{
    public class MessageService
    {
        private readonly EvolutionClient _client;

        internal MessageService(EvolutionClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Envia uma mensagem de texto simples.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="request">Objeto contendo os detalhes da mensagem (número, texto).</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendTextMessage(string instanceName, SendMessageRequest request)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                 throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
             if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Endpoint hipotético: /message/sendText/{instanceName}
            // Note que o token da instância pode ser necessário aqui
            string endpoint = $"message/sendText/{instanceName}";

            // Usa o PostAsync genérico, passando o request e esperando um MessageSentResponse
            return await _client.PostAsync<MessageSentResponse>(endpoint, request, instanceToken: null); // Passe o instanceToken se for diferente do global
        }

        // Adicione outros métodos: SendMediaMessage, SendButtonMessage, etc.
    }
}