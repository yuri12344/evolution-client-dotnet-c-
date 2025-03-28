using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Evolution.ClientAPI.Models;
namespace Evolution.ClientAPI.Services
{
    /// <summary>
    /// Fornece métodos para interagir com os endpoints de instância da Evolution API.
    /// </summary>
    public class InstanceService
    {
        private readonly EvolutionClient _client; // Mantém a referência ao cliente principal

        // Construtor interno, chamado pelo EvolutionClient
        internal InstanceService(EvolutionClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Busca todas as instâncias configuradas na API.
        /// </summary>
        /// <returns>Uma lista de objetos Instance.</returns>
        public async Task<List<Instance>> FetchInstances()
        {
            // Chama o método genérico do cliente, especificando o tipo de retorno esperado
            return await _client.GetAsync<List<Instance>>("instance/fetchInstances");
        }

        /// <summary>
        /// Cria uma nova instância na API.
        /// </summary>
        /// <param name="config">Configurações para a nova instância.</param>
        /// <returns>O objeto Instance representando a instância criada (ou existente).</returns>
        public async Task<Instance> CreateInstance(InstanceConfig config)
        {
            // Cria um objeto anônimo com os campos necessários para a API
            var configData = new
            {
                instanceName = config.InstanceName,
                token = config.Token,
                number = config.Number,
                qrcode = config.Qrcode,
                integration = config.Integration ?? "WHATSAPP-BAILEYS",
                reject_call = config.RejectCall,
                msgCall = config.MsgCall,
                groupsIgnore = config.GroupsIgnore,
                alwaysOnline = config.AlwaysOnline,
                readMessages = config.ReadMessages,
                readStatus = config.ReadStatus,
                syncFullHistory = config.SyncFullHistory
                // Nota: Os campos adicionais como proxy, webhook, etc. precisam ser adicionados
                // à classe InstanceConfig antes de serem usados aqui
            };

            // Chama o método POST genérico, passando o objeto de configuração
            return await _client.PostAsync<Instance>("instance/create", configData);
        }

        /// <summary>
        /// Deleta uma instância específica pelo nome.
        /// </summary>
        /// <param name="instanceName">O nome da instância a ser deletada.</param>
        /// <returns>A representação da instância que foi solicitada para deleção (a resposta da API pode variar).</returns>
        public async Task<Instance> DeleteInstance(string instanceName) // O tipo de retorno pode precisar de ajuste (talvez bool ou um objeto de status?)
        {
             // Verifica se instanceName não é nulo ou vazio
             if (string.IsNullOrWhiteSpace(instanceName))
                 throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

            // Chama o método DELETE genérico. A API pode retornar o objeto deletado ou outra coisa.
            // Ajuste <Instance> se a API retornar um tipo diferente (ex: StatusResponse).
            return await _client.DeleteAsync<Instance>($"instance/delete/{instanceName}");
        }

        /// <summary>
        /// Desconecta (logout) uma instância específica pelo nome.
        /// </summary>
        /// <param name="instanceName">O nome da instância a ser desconectada.</param>
        /// <returns>O objeto Instance atualizado após a tentativa de logout.</returns>
        public async Task<Instance> LogoutInstance(string instanceName)
        {
             if (string.IsNullOrWhiteSpace(instanceName))
                 throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

            // Logout geralmente é um DELETE no endpoint de conexão/logout
            return await _client.DeleteAsync<Instance>($"instance/logout/{instanceName}");
        }

        /// <summary>
        /// Reinicia uma instância específica pelo nome.
        /// </summary>
        /// <param name="instanceName">O nome da instância a ser reiniciada.</param>
        /// <returns>O objeto Instance atualizado após a tentativa de reinício.</returns>
        public async Task<Instance> RestartInstance(string instanceName)
        {
             if (string.IsNullOrWhiteSpace(instanceName))
                 throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

            // Reiniciar geralmente é um PUT ou POST sem corpo ou com corpo mínimo
            // A API da Evolution usa PUT sem corpo aqui
            // Passamos null como 'data' para PutAsync
            return await _client.PutAsync<Instance>($"instance/restart/{instanceName}", null); // Passando null para o corpo
        }

        // --- Novos Métodos (Exemplos) ---

        /// <summary>
        /// Obtém o status de conexão de uma instância específica.
        /// </summary>
        /// <param name="instanceName">O nome da instância.</param>
        /// <returns>O objeto Instance com o status atualizado.</returns>
        public async Task<Instance> GetConnectionState(string instanceName)
        {
             if (string.IsNullOrWhiteSpace(instanceName))
                 throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

             // Endpoint comum para obter status
             return await _client.GetAsync<Instance>($"instance/connectionState/{instanceName}");
        }

         /// <summary>
        /// Obtém o QR Code para conectar uma instância (se aplicável).
        /// </summary>
        /// <param name="instanceName">O nome da instância.</param>
        /// <returns>Um objeto contendo a string do QR Code (ou a imagem, dependendo da API).</returns>
        /// <remarks>A API pode retornar um objeto específico para QR Code, ajuste TResponse conforme necessário.</remarks>
        public async Task<object> GetQrCode(string instanceName) // Ajuste 'object' para o tipo de retorno real (ex: QrCodeResponse)
        {
             if (string.IsNullOrWhiteSpace(instanceName))
                 throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

             // Endpoint comum para obter QR Code
             // O tipo de retorno <object> é genérico, idealmente crie uma classe QrCodeResponse
             return await _client.GetAsync<object>($"instance/connect/{instanceName}");
        }

        // Adicione aqui outros métodos conforme a documentação da API Evolution...
        // Ex: SetWebhook, GetSettings, SetSettings, etc.
    }
}