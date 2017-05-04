using DofusBot.Core.Network;
using System.Collections.Generic;

namespace DofusBot.Packet.Messages.Security
{
    public class CheckIntegrityMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.CheckIntegrityMessage; }
        }

        public const uint ProtocolId = 6372;
        public override uint MessageID { get { return ProtocolId; } }


        public List<int> Data { get; set; }


        public CheckIntegrityMessage() { }

        public CheckIntegrityMessage(List<int> data)
        {
            Data = data;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(Data.Count);
            for (int i = 0; i < Data.Count; i++)
            {
                writer.WriteByte((byte)Data[i]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            int length = reader.ReadVarInt();
            Data = new List<int>();
            for (int i = 0; i < length; i++)
            {
                Data.Add(reader.ReadByte());
            }
        }
    }
}
