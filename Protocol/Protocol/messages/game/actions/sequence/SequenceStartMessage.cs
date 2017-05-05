









// Generated on 12/11/2014 19:01:19
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class SequenceStartMessage : Message
    {
        public new const uint ID =955;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte sequenceType;
        public int authorId;
        
        public SequenceStartMessage()
        {
        }
        
        public SequenceStartMessage(sbyte sequenceType, int authorId)
        {
            this.sequenceType = sequenceType;
            this.authorId = authorId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(sequenceType);
            writer.WriteInt(authorId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            sequenceType = reader.ReadSByte();
            authorId = reader.ReadInt();
        }
        
    }
    
}