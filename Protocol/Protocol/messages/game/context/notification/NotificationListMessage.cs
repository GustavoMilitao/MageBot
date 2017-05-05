









// Generated on 12/11/2014 19:01:31
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class NotificationListMessage : Message
    {
        public new const uint ID =6087;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] flags;
        
        public NotificationListMessage()
        {
        }
        
        public NotificationListMessage(int[] flags)
        {
            this.flags = flags;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)flags.Length);
            foreach (var entry in flags)
            {
                 writer.WriteVarInt(entry);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            flags = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 flags[i] = reader.ReadVarInt();
            }
        }
        
    }
    
}