// Evolution.ClientAPI/Services/MessageService.cs
using System;
using System.Threading.Tasks;
using System.IO;
using Evolution.ClientAPI.Models;

namespace Evolution.ClientAPI.Services
{
    /// <summary>
    /// Fornece métodos para enviar diferentes tipos de mensagens através da Evolution API.
    /// </summary>
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
        /// <param name="message">Objeto contendo os detalhes da mensagem de texto.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendTextMessage(string instanceName, TextMessage message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendText/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia uma mensagem com mídia (imagem, vídeo, documento, etc).
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes da mensagem de mídia.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <param name="file">Arquivo opcional a ser enviado junto com a mensagem.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendMediaMessage(string instanceName, MediaMessage message, string instanceToken = null, Stream file = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendMedia/{instanceName}";
            
            // Se tiver um arquivo, usa o método que suporta upload de arquivos
            if (file != null)
            {
                return await _client.PostWithFileAsync<MessageSentResponse>(endpoint, message, file, instanceToken);
            }
            
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia um vídeo de visualização única (PTV - Play-to-View).
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes da mensagem PTV.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <param name="file">Arquivo opcional a ser enviado junto com a mensagem.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendPtvMessage(string instanceName, object message, string instanceToken = null, Stream file = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendPtv/{instanceName}";
            
            if (file != null)
            {
                return await _client.PostWithFileAsync<MessageSentResponse>(endpoint, message, file, instanceToken);
            }
            
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia um áudio no formato WhatsApp.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes da mensagem de áudio.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <param name="file">Arquivo opcional a ser enviado junto com a mensagem.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendWhatsAppAudio(string instanceName, object message, string instanceToken = null, Stream file = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendWhatsAppAudio/{instanceName}";
            
            if (file != null)
            {
                return await _client.PostWithFileAsync<MessageSentResponse>(endpoint, message, file, instanceToken);
            }
            
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia uma mensagem de status.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes da mensagem de status.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendStatusMessage(string instanceName, StatusMessage message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendStatus/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia um sticker.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes do sticker.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendSticker(string instanceName, object message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendSticker/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia uma localização.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes da localização.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendLocation(string instanceName, LocationMessage message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendLocation/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia um contato.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes do contato.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendContact(string instanceName, ContactMessage message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendContact/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia uma reação a uma mensagem.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a reação.</param>
        /// <param name="message">Objeto contendo os detalhes da reação.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendReaction(string instanceName, ReactionMessage message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendReaction/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia uma enquete.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes da enquete.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendPoll(string instanceName, PollMessage message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendPoll/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia uma mensagem com lista de opções.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes da lista.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendList(string instanceName, ListMessage message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendList/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }

        /// <summary>
        /// Envia uma mensagem com botões.
        /// </summary>
        /// <param name="instanceName">Nome da instância que enviará a mensagem.</param>
        /// <param name="message">Objeto contendo os detalhes da mensagem com botões.</param>
        /// <param name="instanceToken">Token opcional específico da instância.</param>
        /// <returns>Resposta da API após o envio.</returns>
        public async Task<MessageSentResponse> SendButtons(string instanceName, ButtonMessage message, string instanceToken = null)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string endpoint = $"message/sendButtons/{instanceName}";
            return await _client.PostAsync<MessageSentResponse>(endpoint, message, instanceToken);
        }
    }
}