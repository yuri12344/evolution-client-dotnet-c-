using System;
using System.Threading.Tasks;
using Evolution.ClientAPI.Models;
using Evolution.ClientAPI.Exceptions;

namespace Evolution.ClientAPI.Examples
{
    class Program
    {
        private static EvolutionClient _client;
        static async Task Main(string[] args)
        {
            try
            {
                DotNetEnv.Env.Load();
                _client = EvolutionClient.CreateClient();
                await GetAllInstances();
            }
            catch (EvolutionAPIError ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro na API Evolution: {ex.Message}");
            }
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }

        static async Task GetAllInstances()
        {
            // 1. Listar Instâncias
            // await ListInstances();
            // 2. Criar Instância
            await CreateInstance();
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

        static async Task CreateInstance()
        {
            Console.WriteLine("\nTentando criar nova instância 'MinhaInstanciaTeste'...");
            var instanceConfig = new InstanceConfig(
                instanceName: "MinhaInstanciaTeste2",
                qrcode: true,
                rejectCall: false,
                groupsIgnore: false,
                alwaysOnline: false,
                readMessages: true,
                readStatus: false,
                syncFullHistory: false
            );

            try
            {
                var createdInstance = await _client.Instances.CreateInstance(instanceConfig);
                Console.WriteLine($"Instância criada/retornada: Nome={createdInstance.Name}, Status={createdInstance.ConnectionStatus}");
                // Aqui você provavelmente precisaria de lógica para lidar com o QR Code se solicitado
            }
            catch (EvolutionAPIError apiEx)
            {
                 Console.WriteLine($"Erro ao criar instância: {apiEx.Message}");
                 // Pode ser um erro 409 (Conflict) se a instância já existe, etc.
            }
        }

         static async Task LogoutInstanceExample(string instanceName)
        {
            Console.WriteLine($"\nTentando desconectar a instância '{instanceName}'...");
            try
            {
                var result = await _client.Instances.LogoutInstance(instanceName);
                 Console.WriteLine($"Resultado da desconexão para '{instanceName}': Status={result.ConnectionStatus}"); // O objeto retornado pode variar
            }
            catch(EvolutionNotFoundError)
            {
                 Console.WriteLine($"Erro: Instância '{instanceName}' não encontrada para desconectar.");
            }
            catch (EvolutionAPIError apiEx)
            {
                 Console.WriteLine($"Erro ao desconectar instância '{instanceName}': {apiEx.Message}");
            }
        }

        static async Task DeleteInstanceExample(string instanceName)
        {
            Console.WriteLine($"\nTentando deletar a instância '{instanceName}'...");
             try
            {
                // O endpoint delete pode retornar a instância deletada ou uma confirmação
                var result = await _client.Instances.DeleteInstance(instanceName);
                Console.WriteLine($"Instância '{instanceName}' deletada (ou solicitação enviada). Resposta: {result}"); // Ajuste conforme a resposta real da API
            }
            catch(EvolutionNotFoundError)
            {
                 Console.WriteLine($"Erro: Instância '{instanceName}' não encontrada para deletar.");
            }
            catch (EvolutionAPIError apiEx)
            {
                 Console.WriteLine($"Erro ao deletar instância '{instanceName}': {apiEx.Message}");
            }
        }

         static async Task RestartInstanceExample(string instanceName)
        {
            Console.WriteLine($"\nTentando reiniciar a instância '{instanceName}'...");
             try
            {
                var result = await _client.Instances.RestartInstance(instanceName);
                 Console.WriteLine($"Resultado do reinício para '{instanceName}': Status={result.ConnectionStatus}"); // O objeto retornado pode variar
            }
            catch(EvolutionNotFoundError)
            {
                 Console.WriteLine($"Erro: Instância '{instanceName}' não encontrada para reiniciar.");
            }
            catch (EvolutionAPIError apiEx)
            {
                 Console.WriteLine($"Erro ao reiniciar instância '{instanceName}': {apiEx.Message}");
            }
        }

    }
}