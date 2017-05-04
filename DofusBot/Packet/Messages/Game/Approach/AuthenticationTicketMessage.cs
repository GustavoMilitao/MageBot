using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Approach
{
    public class AuthenticationTicketMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.AuthenticationTicketMessage; }
        }

        public const uint ProtocolId = 110;
        public override uint MessageID { get { return ProtocolId; } }


        public string Lang { get; set; }
        public string Ticket { get; set; }


        public AuthenticationTicketMessage() { }

        public AuthenticationTicketMessage(string lang, string ticket)
        {
            Lang = lang;
            Ticket = ticket;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Lang);
            writer.WriteUTF(Ticket);
        }

        public override void Deserialize(IDataReader reader)
        {
            Lang = reader.ReadUTF();
            Ticket = reader.ReadUTF();
        }
    }
}
