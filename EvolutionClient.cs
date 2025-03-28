using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Evolution.ClientAPI.Services;
using Evolution.ClientAPI.Exceptions;

namespace Evolution.ClientAPI
{
    public class EvolutionClient
    {
        private static readonly HttpClient _sharedHttpClient = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _apiToken;

        internal static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        // Serviços disponíveis
        public InstanceService Instances { get; private set; }
        // public InstanceOperationsService InstanceOperations { get; private set; }
        // public MessageService Messages { get; private set; }
        // public CallService Calls { get; private set; }
        // public ChatService Chat { get; private set; }
        // public LabelService Label { get; private set; }
        // public ProfileService Profile { get; private set; }
        // public GroupService Group { get; private set; }


        public EvolutionClient(string baseUrl, string apiToken, HttpClient httpClient = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl), "A URL base da API é obrigatória.");
            if (string.IsNullOrWhiteSpace(apiToken))
                throw new ArgumentNullException(nameof(apiToken), "O token da API é obrigatório.");

            _baseUrl = baseUrl.TrimEnd('/');
            _apiToken = apiToken;

            var client = httpClient ?? _sharedHttpClient;

            if (httpClient == null && _sharedHttpClient.DefaultRequestHeaders.Accept.Count == 0)
            {
                 _sharedHttpClient.DefaultRequestHeaders.Accept.Clear();
                 _sharedHttpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                 _sharedHttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Evolution-ClientAPI-CSharp/1.0");
            }

            Instances = new InstanceService(this);
            // InstanceOperations = new InstanceOperationsService(this);
            // Messages = new MessageService(this);
            // Calls = new CallService(this);
            // Chat = new ChatService(this);
            // Label = new LabelService(this);
            // Profile = new ProfileService(this);
            // Group = new GroupService(this);
        }

        public static EvolutionClient CreateClient()
        {
            var baseUrl = Environment.GetEnvironmentVariable("EVOLUTION_BASE_URL");
            var apiToken = Environment.GetEnvironmentVariable("EVOLUTION_API_TOKEN");

            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new InvalidOperationException("Variável de ambiente EVOLUTION_BASE_URL não definida.");
            if (string.IsNullOrWhiteSpace(apiToken))
                 throw new InvalidOperationException("Variável de ambiente EVOLUTION_API_TOKEN não definida.");

            return new EvolutionClient(baseUrl, apiToken);
        }

        /// <summary>
        /// Obtém a URL completa para um endpoint.
        /// </summary>
        internal string GetFullUrl(string endpoint) => $"{_baseUrl}/{endpoint}";
        internal Dictionary<string, string> GetHeaders(string instanceToken = null) =>
            new Dictionary<string, string>
            {
                { "apikey", instanceToken ?? _apiToken }
            };

        /// <summary>
        /// Deserializa a resposta HTTP para o tipo especificado.
        /// </summary>
        internal async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
             if (string.IsNullOrWhiteSpace(json))
            {
                 if (typeof(T) == typeof(string)) return (T)(object)string.Empty;
                 return default(T);
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(json, JsonSerializerSettings);
            }
            catch (JsonException ex)
            {
                throw new EvolutionAPIError($"Falha ao processar a resposta da API: {ex.Message}. Resposta recebida: {json}", ex);
            }
        }

        /// <summary>
        /// Processa a resposta HTTP e trata erros.
        /// </summary>
        internal async Task<string> HandleResponseAndReadContent(HttpResponseMessage response)
        {
             var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                string errorDetail = string.IsNullOrWhiteSpace(responseContent)
                    ? "(sem corpo na resposta)"
                    : $"- Detalhe: {responseContent}";

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                         throw new EvolutionAuthenticationError($"Falha na autenticação (401). {errorDetail}");
                    case HttpStatusCode.Forbidden:
                         throw new EvolutionAuthenticationError($"Acesso proibido (403). Verifique as permissões do token. {errorDetail}");
                    case HttpStatusCode.NotFound:
                        throw new EvolutionNotFoundError($"Recurso não encontrado (404). Verifique o endpoint ou o nome da instância. {errorDetail}");
                    case HttpStatusCode.Conflict:
                         throw new EvolutionAPIError($"Conflito (409). O recurso pode já existir ou há um estado inválido. {errorDetail}");
                     case HttpStatusCode.BadRequest:
                        throw new EvolutionAPIError($"Requisição inválida (400). Verifique os parâmetros enviados. {errorDetail}");
                    case HttpStatusCode.InternalServerError:
                         throw new EvolutionAPIError($"Erro interno do servidor da API (500). {errorDetail}");
                    default:
                        throw new EvolutionAPIError($"Erro na requisição: {(int)response.StatusCode} {response.ReasonPhrase}. {errorDetail}");
                }
            }
            return responseContent;
        }

        /// <summary>
        /// Método base para enviar requisições HTTP.
        /// </summary>
        private async Task<TResponse> SendRequestAsync<TResponse>(
            HttpMethod method, 
            string endpoint, 
            object data = null, 
            string instanceToken = null,
            Dictionary<string, Tuple<string, Stream, string>> files = null)
        {
            var url = GetFullUrl(endpoint);
            using (var request = new HttpRequestMessage(method, url))
            {
                var headers = GetHeaders(instanceToken);
                request.Headers.Add("apikey", headers["apikey"]);

                if (files != null && files.Count > 0 && method != HttpMethod.Get)
                {
                    var multipartContent = new MultipartFormDataContent($"----EvolutionBoundary{DateTime.Now.Ticks:x}");

                    foreach (var file in files)
                    {
                        if (file.Value?.Item2 == null) continue;

                        var streamContent = new StreamContent(file.Value.Item2);
                        if (!string.IsNullOrWhiteSpace(file.Value.Item3))
                        {
                            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.Value.Item3);
                        }
                        multipartContent.Add(streamContent, file.Key, file.Value.Item1);
                    }

                    if (data != null)
                    {
                        try
                        {
                            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                            var jsonDataForForm = JsonConvert.SerializeObject(data, settings);
                            var dataDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonDataForForm);

                            if (dataDict != null)
                            {
                                foreach (var kvp in dataDict)
                                {
                                    if(kvp.Value != null)
                                        multipartContent.Add(new StringContent(kvp.Value), kvp.Key);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"WARN: Não foi possível adicionar o objeto 'data' como campos no formulário multipart. Erro: {ex.Message}");
                        }
                    }
                    request.Content = multipartContent;
                }
                else if (data != null && method != HttpMethod.Get)
                {
                    var jsonData = JsonConvert.SerializeObject(data, JsonSerializerSettings);
                    request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                }

                var response = await _sharedHttpClient.SendAsync(request);
                await HandleResponseAndReadContent(response);
                return await DeserializeResponse<TResponse>(response);
            }
        }

        /// <summary>
        /// Envia uma requisição GET para o endpoint especificado.
        /// </summary>
        /// <typeparam name="TResponse">O tipo esperado para deserializar a resposta JSON.</typeparam>
        /// <param name="endpoint">O caminho do endpoint (ex: "instance/fetchInstances").</param>
        /// <param name="instanceToken">Opcional: Token específico da instância.</param>
        /// <returns>A resposta deserializada.</returns>
        public async Task<TResponse> GetAsync<TResponse>(string endpoint, string instanceToken = null)
        {
            return await SendRequestAsync<TResponse>(HttpMethod.Get, endpoint, instanceToken: instanceToken);
        }

        /// <summary>
        /// Envia uma requisição POST para o endpoint especificado.
        /// </summary>
        /// <typeparam name="TResponse">O tipo esperado para deserializar a resposta JSON.</typeparam>
        /// <param name="endpoint">O caminho do endpoint.</param>
        /// <param name="data">Opcional: Objeto a ser serializado como corpo JSON ou dados de formulário (se houver arquivos).</param>
        /// <param name="instanceToken">Opcional: Token específico da instância.</param>
        /// <param name="files">Opcional: Dicionário de arquivos para upload (multipart/form-data).</param>
        /// <returns>A resposta deserializada.</returns>
        public async Task<TResponse> PostAsync<TResponse>(
            string endpoint,
            object data = null,
            string instanceToken = null,
            Dictionary<string, Tuple<string, Stream, string>> files = null)
        {
            return await SendRequestAsync<TResponse>(HttpMethod.Post, endpoint, data, instanceToken, files);
        }

        /// <summary>
        /// Envia uma requisição PUT para o endpoint especificado.
        /// </summary>
        /// <typeparam name="TResponse">O tipo esperado para deserializar a resposta JSON.</typeparam>
        /// <param name="endpoint">O caminho do endpoint.</param>
        /// <param name="data">O objeto a ser serializado como corpo JSON.</param>
        /// <param name="instanceToken">Opcional: Token específico da instância.</param>
        /// <returns>A resposta deserializada.</returns>
        public async Task<TResponse> PutAsync<TResponse>(string endpoint, object data, string instanceToken = null)
        {
            return await SendRequestAsync<TResponse>(HttpMethod.Put, endpoint, data, instanceToken);
        }

        /// <summary>
        /// Envia uma requisição DELETE para o endpoint especificado.
        /// </summary>
        /// <typeparam name="TResponse">O tipo esperado para deserializar a resposta JSON.</typeparam>
        /// <param name="endpoint">O caminho do endpoint.</param>
        /// <param name="data">Opcional: Objeto a ser serializado como corpo JSON (menos comum para DELETE).</param>
        /// <param name="instanceToken">Opcional: Token específico da instância.</param>
        /// <returns>A resposta deserializada.</returns>
        public async Task<TResponse> DeleteAsync<TResponse>(
            string endpoint,
            object data = null,
            string instanceToken = null)
        {
            return await SendRequestAsync<TResponse>(HttpMethod.Delete, endpoint, data, instanceToken);
        }
    }
}