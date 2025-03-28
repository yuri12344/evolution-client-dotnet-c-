using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Evolution.ClientAPI.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InstanceConnectionStatus
    {
        open,
        close,
        connecting
    }

    public class WebhookConfig
    {
        public string Url { get; set; }
        public bool ByEvents { get; set; } = false;
        public bool Base64 { get; set; } = true;
        public Dictionary<string, string> Headers { get; set; }
        public List<string> Events { get; set; }

        public WebhookConfig(string url = null, bool byEvents = false, bool base64 = true,
                            Dictionary<string, string> headers = null, List<string> events = null)
        {
            Url = url;
            ByEvents = byEvents;
            Base64 = base64;
            Headers = headers;
            Events = events;
        }
    }

    public class EventsConfig
    {
        public bool Enabled { get; set; } = true;
        public List<string> Events { get; set; }

        public EventsConfig(bool enabled = true, List<string> events = null)
        {
            Enabled = enabled;
            Events = events;
        }
    }

    public class ChatwootConfig
    {
        public string ChatwootAccountId { get; set; }
        public string ChatwootToken { get; set; }
        public string ChatwootUrl { get; set; }
        public bool ChatwootSignMsg { get; set; } = true;
        public bool ChatwootReopenConversation { get; set; } = true;
        public bool ChatwootConversationPending { get; set; } = false;
        public bool ChatwootImportContacts { get; set; } = true;
        public string ChatwootNameInbox { get; set; } = "evolution";
        public bool ChatwootMergeBrazilContacts { get; set; } = true;
        public bool ChatwootImportMessages { get; set; } = true;
        public int ChatwootDaysLimitImportMessages { get; set; } = 3;
        public string ChatwootOrganization { get; set; } = "Evolution Bot";
        public string ChatwootLogo { get; set; } = "https://evolution-api.com/files/evolution-api-favicon.png";

        public ChatwootConfig(string accountId = null, string token = null, string url = null,
                             bool signMsg = true, bool reopenConversation = true,
                             bool conversationPending = false, bool importContacts = true,
                             string nameInbox = "evolution", bool mergeBrazilContacts = true,
                             bool importMessages = true, int daysLimitImportMessages = 3,
                             string organization = "Evolution Bot",
                             string logo = "https://evolution-api.com/files/evolution-api-favicon.png")
        {
            ChatwootAccountId = accountId;
            ChatwootToken = token;
            ChatwootUrl = url;
            ChatwootSignMsg = signMsg;
            ChatwootReopenConversation = reopenConversation;
            ChatwootConversationPending = conversationPending;
            ChatwootImportContacts = importContacts;
            ChatwootNameInbox = nameInbox;
            ChatwootMergeBrazilContacts = mergeBrazilContacts;
            ChatwootImportMessages = importMessages;
            ChatwootDaysLimitImportMessages = daysLimitImportMessages;
            ChatwootOrganization = organization;
            ChatwootLogo = logo;
        }
    }

    public class Instance
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public InstanceConnectionStatus ConnectionStatus { get; set; }
        public string OwnerJid { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicUrl { get; set; }
        public string Integration { get; set; }
        public string Number { get; set; }
        public string BusinessId { get; set; }
        public string Token { get; set; }
        public string ClientName { get; set; }
        public int? DisconnectionReasonCode { get; set; }
        public string DisconnectionObject { get; set; }
        public DateTime? DisconnectionAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Configurações adicionais
        public bool Qrcode { get; set; }
        public bool RejectCall { get; set; }
        public string MsgCall { get; set; }
        public bool GroupsIgnore { get; set; }
        public bool AlwaysOnline { get; set; }
        public bool ReadMessages { get; set; }
        public bool ReadStatus { get; set; }
        public bool SyncFullHistory { get; set; }
        
        // Configurações avançadas
        public WebhookConfig Webhook { get; set; }
        public EventsConfig Events { get; set; }
        public ChatwootConfig Chatwoot { get; set; }
    }
}