









// Generated on 12/11/2014 19:01:34
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class JobCrafterDirectoryAddMessage : Message
    {
        public new const uint ID =5651;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.JobCrafterDirectoryListEntry listEntry;
        
        public JobCrafterDirectoryAddMessage()
        {
        }
        
        public JobCrafterDirectoryAddMessage(Types.JobCrafterDirectoryListEntry listEntry)
        {
            this.listEntry = listEntry;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            listEntry.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            listEntry = new Types.JobCrafterDirectoryListEntry();
            listEntry.Deserialize(reader);
        }
        
    }
    
}