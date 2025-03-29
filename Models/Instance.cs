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
        connecting,
        SUCCESS // Added to handle the SUCCESS status returned by the API
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

    public class QrCodeDetails
    {
        [JsonProperty("pairingCode")]
        public string PairingCode { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("base64")]
        public string Base64 { get; set; }
    }

    public class Instance
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public InstanceConnectionStatus ConnectionStatus { get; set; }

        [JsonProperty("owner")]
        public string OwnerJid { get; set; }

        [JsonProperty("profileName")]
        public string ProfileName { get; set; }

        [JsonProperty("profilePictureUrl")]
        public string ProfilePicUrl { get; set; }

        [JsonProperty("integration")]
        public string Integration { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("businessId")]
        public string BusinessId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("clientName")]
        public string ClientName { get; set; }

        [JsonProperty("disconnectionCode")]
        public int? DisconnectionReasonCode { get; set; }

        [JsonProperty("disconnectionObject")]
        public string DisconnectionObject { get; set; }

        [JsonProperty("disconnectedAt")]
        public DateTime? DisconnectionAt { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
        
        // Configurações adicionais
        [JsonProperty("qrcode")]
        public QrCodeDetails QrCode { get; set; }

        [JsonProperty("rejectCall")]
        public bool RejectCall { get; set; }

        [JsonProperty("msgCall")]
        public string MsgCall { get; set; }

        [JsonProperty("groupsIgnore")]
        public bool GroupsIgnore { get; set; }

        [JsonProperty("alwaysOnline")]
        public bool AlwaysOnline { get; set; }

        [JsonProperty("readMessages")]
        public bool ReadMessages { get; set; }

        [JsonProperty("readStatus")]
        public bool ReadStatus { get; set; }

        [JsonProperty("syncFullHistory")]
        public bool SyncFullHistory { get; set; }
        
        // Configurações avançadas
        [JsonProperty("webhook")]
        public WebhookConfig Webhook { get; set; }

        [JsonProperty("events")]
        public EventsConfig Events { get; set; }

        [JsonProperty("chatwoot")]
        public ChatwootConfig Chatwoot { get; set; }
    }

    public class CreateInstanceResponse
    {
        [JsonProperty("instance")]
        public Instance Instance { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("qrcode")]
        public QrCodeDetails QrCode { get; set; }

        [JsonProperty("settings")]
        public Instance Settings { get; set; }
    }
}