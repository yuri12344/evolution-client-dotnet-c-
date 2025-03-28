using System;

namespace Evolution.ClientAPI.Models // << Namespace Ajustado
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

        // Webhook settings (exemplo, adicione conforme a API)
        // public string Webhook { get; set; }
        // public bool? WebhookEvents { get; set; } // Ou uma lista de eventos string/enum

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
            bool qrcode = false,
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
            Qrcode = qrcode;
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