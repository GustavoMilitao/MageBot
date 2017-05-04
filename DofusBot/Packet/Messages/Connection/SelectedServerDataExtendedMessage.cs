using System.Collections.Generic;

namespace DofusBot.Packet.Messages.Connection
{
    public class SelectedServerDataExtendedMessage : SelectedServerDataMessage
    {
        public new ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.SelectedServerDataExtendedMessage; }
        }

        public List<ushort> ServerIds;

        public new const uint ProtocolId = 6469;
        public override uint MessageID { get { return ProtocolId; } }

        public SelectedServerDataExtendedMessage() { }

        public SelectedServerDataExtendedMessage(List<ushort> serverIds)
        {
            ServerIds = serverIds;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)ServerIds.Count);
            for (int i = 0; i < ServerIds.Count; i++)
            {
                writer.WriteVarShort(ServerIds[i]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ushort length = reader.ReadUShort();
            ServerIds = new List<ushort>();
            for (int i = 0; i < length; i++)
            {
                ServerIds.Add(reader.ReadVarUhShort());
            }
        }
    }
}
