using System;
using System.Collections.Generic;

namespace Evolution.ClientAPI.Models
{
    public class InstanceConfig
    {
        public string InstanceName { get; set; }
        public string Integration { get; set; }
        public string Token { get; set; }
        public string Number { get; set; }
        public bool Qrcode { get; set; }
        public bool RejectCall { get; set; }
        public string MsgCall { get; set; }
        public bool GroupsIgnore { get; set; }
        public bool AlwaysOnline { get; set; }
        public bool ReadMessages { get; set; }
        public bool ReadStatus { get; set; }
        public bool SyncFullHistory { get; set; }
        
        // Proxy settings
        public string ProxyHost { get; set; }
        public string ProxyPort { get; set; }
        public string ProxyProtocol { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }
        
        // Webhook settings
        public string WebhookUrl { get; set; }
        public bool WebhookByEvents { get; set; }
        public bool WebhookBase64 { get; set; }
        public List<string> WebhookEvents { get; set; }
        
        // RabbitMQ settings
        public bool RabbitmqEnabled { get; set; }
        public List<string> RabbitmqEvents { get; set; }
        
        // SQS settings
        public bool SqsEnabled { get; set; }
        public List<string> SqsEvents { get; set; }
        
        // Chatwoot settings
        public int? ChatwootAccountId { get; set; }
        public string ChatwootToken { get; set; }
        public string ChatwootUrl { get; set; }
        public bool ChatwootSignMsg { get; set; }
        public bool ChatwootReopenConversation { get; set; }
        public bool ChatwootConversationPending { get; set; }
        public bool ChatwootImportContacts { get; set; }
        public string ChatwootNameInbox { get; set; }
        public bool ChatwootMergeBrazilContacts { get; set; }
        public int? ChatwootDaysLimitImportMessages { get; set; }
        public string ChatwootOrganization { get; set; }
        public string ChatwootLogo { get; set; }
        
        // Typebot settings
        public string TypebotUrl { get; set; }
        public string Typebot { get; set; }
        public int? TypebotExpire { get; set; }
        public string TypebotKeywordFinish { get; set; }
        public int? TypebotDelayMessage { get; set; }
        public string TypebotUnknownMessage { get; set; }
        public bool TypebotListeningFromMe { get; set; }

        /// <summary>
        /// Cria uma configuração para uma nova instância.
        /// </summary>
        /// <param name="instanceName">O nome desejado para a instância (obrigatório).</param>
        /// <param name="token">O token específico da instância.</param>
        /// <param name="qrcode">Se deve solicitar QR code na criação/conexão.</param>
        /// <param name="rejectCall">Se deve rejeitar chamadas.</param>
        /// <param name="msgCall">A mensagem a ser enviada ao rejeitar uma chamada.</param>
        /// <param name="groupsIgnore">Se deve ignorar grupos.</param>
        /// <param name="alwaysOnline">Se deve manter sempre online.</param>
        /// <param name="readMessages">Se deve ler mensagens.</param>
        /// <param name="readStatus">Se deve ler o status.</param>
        /// <param name="syncFullHistory">Se deve sincronizar toda a história.</param>
        public InstanceConfig(
            string instanceName,
            string token = null,
            string number = null,
            bool qrcode = false,
            string integration = "WHATSAPP-BAILEYS",
            bool rejectCall = false,
            string msgCall = null,
            bool groupsIgnore = false,
            bool alwaysOnline = false,
            bool readMessages = true,
            bool readStatus = false,
            bool syncFullHistory = false)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));
            
            InstanceName = instanceName;
            Token = token;
            Number = number;
            Qrcode = qrcode;
            Integration = integration;
            RejectCall = rejectCall;
            MsgCall = msgCall;
            GroupsIgnore = groupsIgnore;
            AlwaysOnline = alwaysOnline;
            ReadMessages = readMessages;
            ReadStatus = readStatus;
            SyncFullHistory = syncFullHistory;
        }

        // O construtor longo ainda pode existir, mas usar inicializadores de objeto é mais flexível:
        // var config = new InstanceConfig("MeuNome") {
        //     Qrcode = true,
        //     RejectCall = false,
        //     // ... outras propriedades
        // };
    }
}