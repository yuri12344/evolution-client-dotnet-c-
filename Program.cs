using System;
using System.Threading.Tasks;
using Evolution.ClientAPI.Models;
using Evolution.ClientAPI.Exceptions;

// Program.cs
namespace Evolution.ClientAPI.Examples
{
    class Program
    {
        private static EvolutionClient _client;
        static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load();
            _client = EvolutionClient.CreateClient();
            await GetAllInstances();
        }

        static async Task GetAllInstances()
        {

            // 1. Listar Instâncias
            // await ListInstances();
            
            const string instanceName = "Marindombo";
            // 2. Criar Instância
            await CreateInstance(instanceName);

            // 3. Delete instancia
            // await DeleteInstanceExample(instanceName);
        }
        static async Task ListInstances()
        {
            Console.WriteLine("\nBuscando instâncias...");
            var instances = await _client.Instances.FetchInstances();
            if (instances != null && instances.Count > 0)
            {
                Console.WriteLine($"Total: {instances.Count} instâncias:");
                foreach (var instance in instances)
                {
                    Console.WriteLine($"\nInformações da Instância:");
                    Console.WriteLine($"ID: {instance.Id}");
                    Console.WriteLine($"Nome: {instance.Name}");
                    Console.WriteLine($"Status de Conexão: {instance.ConnectionStatus}");
                    Console.WriteLine($"Owner JID: {instance.OwnerJid}");
                    Console.WriteLine($"Nome do Perfil: {instance.ProfileName}");
                    Console.WriteLine($"URL da Foto: {instance.ProfilePicUrl}");
                    Console.WriteLine($"Integração: {instance.Integration}");
                    Console.WriteLine($"Número: {instance.Number}");
                    Console.WriteLine($"ID do Negócio: {instance.BusinessId}");
                    Console.WriteLine($"Token: {instance.Token}");
                    Console.WriteLine($"Nome do Cliente: {instance.ClientName}");
                    
                    if (instance.DisconnectionAt.HasValue)
                    {
                        Console.WriteLine($"Desconectado em: {instance.DisconnectionAt}");
                        Console.WriteLine($"Código de Desconexão: {instance.DisconnectionReasonCode}");
                        Console.WriteLine($"Objeto de Desconexão: {instance.DisconnectionObject}");
                    }
                    
                    Console.WriteLine($"Criado em: {instance.CreatedAt}");
                    Console.WriteLine($"Atualizado em: {instance.UpdatedAt}");
                    Console.WriteLine("----------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma instância encontrada ou ocorreu um erro silencioso.");
            }
        }

        static async Task CreateInstance(string instanceName)
        {
            Console.WriteLine("\nCreating instance: '" + instanceName + "'...");
            var instanceConfig = new InstanceConfig(
                instanceName: instanceName,
                qrcode: true,
                rejectCall: false,
                groupsIgnore: false,
                alwaysOnline: false,
                readMessages: true,
                readStatus: false,
                syncFullHistory: false
            );

            var createdInstance = await _client.Instances.CreateInstance(instanceConfig);
            Console.WriteLine($"\nInformações da Instância Criada:");
            Console.WriteLine($"ID: {createdInstance.Id}");
            Console.WriteLine($"Nome: {createdInstance.Name}");
            Console.WriteLine($"Status de Conexão: {createdInstance.ConnectionStatus}");
            Console.WriteLine($"Owner JID: {createdInstance.OwnerJid}");
            Console.WriteLine($"Nome do Perfil: {createdInstance.ProfileName}");
            Console.WriteLine($"URL da Foto: {createdInstance.ProfilePicUrl}");
            Console.WriteLine($"Integração: {createdInstance.Integration}");
            Console.WriteLine($"Número: {createdInstance.Number}");
            Console.WriteLine($"ID do Negócio: {createdInstance.BusinessId}");
            Console.WriteLine($"Token: {createdInstance.Token}");
            Console.WriteLine($"Nome do Cliente: {createdInstance.ClientName}");
            
            if (createdInstance.DisconnectionAt.HasValue)
            {
                Console.WriteLine($"Desconectado em: {createdInstance.DisconnectionAt}");
                Console.WriteLine($"Código de Desconexão: {createdInstance.DisconnectionReasonCode}");
                Console.WriteLine($"Objeto de Desconexão: {createdInstance.DisconnectionObject}");
            }
            
            Console.WriteLine($"Criado em: {createdInstance.CreatedAt}");
            Console.WriteLine($"Atualizado em: {createdInstance.UpdatedAt}");
            
            Console.WriteLine($"Rejeitar Chamadas: {createdInstance.RejectCall}");
            Console.WriteLine($"Mensagem de Chamada: {createdInstance.MsgCall}");
            Console.WriteLine($"Ignorar Grupos: {createdInstance.GroupsIgnore}");
            Console.WriteLine($"Sempre Online: {createdInstance.AlwaysOnline}");
            Console.WriteLine($"Ler Mensagens: {createdInstance.ReadMessages}");
            Console.WriteLine($"Ler Status: {createdInstance.ReadStatus}");
            Console.WriteLine($"Sincronizar Histórico Completo: {createdInstance.SyncFullHistory}");
            Console.WriteLine("----------------------------------------");
        }

        static async Task LogoutInstanceExample(string instanceName)
        {
            Console.WriteLine($"\nTentando desconectar a instância '{instanceName}'...");
            var result = await _client.Instances.LogoutInstance(instanceName);
            Console.WriteLine($"Resultado da desconexão para '{instanceName}': Status={result.ConnectionStatus}");
        }

        static async Task DeleteInstanceExample(string instanceName)
        {
            Console.WriteLine($"\nTentando deletar a instância '{instanceName}'...");
            var result = await _client.Instances.DeleteInstance(instanceName);
            Console.WriteLine($"Instância '{instanceName}' deletada (ou solicitação enviada). Resposta: {result}");
        }

        static async Task RestartInstanceExample(string instanceName)
        {
            Console.WriteLine($"\nTentando reiniciar a instância '{instanceName}'...");
            var result = await _client.Instances.RestartInstance(instanceName);
            Console.WriteLine($"Resultado do reinício para '{instanceName}': Status={result.ConnectionStatus}");
        }

    }
}