 
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Messages.Security
{
    public class CheckIntegrityMessage : Message
    {
        public const int ProtocolId = 6372;
        public override int MessageID { get { return ProtocolId; } }

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
