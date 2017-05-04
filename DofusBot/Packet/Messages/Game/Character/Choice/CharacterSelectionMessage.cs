using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Character.Choice
{
    public class CharacterSelectionMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.CharacterSelectionMessage; }
        }

        public const uint ProtocolId = 152;
        public override uint MessageID { get { return ProtocolId; } }

        public ulong ID { get; set; }

        public CharacterSelectionMessage() { }

        public CharacterSelectionMessage(ulong id)
        {
            ID = id;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(ID);
        }

        public override void Deserialize(IDataReader reader)
        {
            ID = reader.ReadVarUhLong();
        }
    }
}
