









// Generated on 12/11/2014 19:01:34
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class JobCrafterDirectoryDefineSettingsMessage : Message
    {
        public new const uint ID =5649;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.JobCrafterDirectorySettings settings;
        
        public JobCrafterDirectoryDefineSettingsMessage()
        {
        }
        
        public JobCrafterDirectoryDefineSettingsMessage(Types.JobCrafterDirectorySettings settings)
        {
            this.settings = settings;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            settings.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            settings = new Types.JobCrafterDirectorySettings();
            settings.Deserialize(reader);
        }
        
    }
    
}