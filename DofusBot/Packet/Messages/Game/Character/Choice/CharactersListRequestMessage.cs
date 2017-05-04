using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Character.Choice
{
    public class CharactersListRequestMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.CharactersListRequestMessage; }
        }

        public const uint ProtocolId = 150;
        public override uint MessageID { get { return ProtocolId; } }

        public CharactersListRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            //
        }

        public override void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
