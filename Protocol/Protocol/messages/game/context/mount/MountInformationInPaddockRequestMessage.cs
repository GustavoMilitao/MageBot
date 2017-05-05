









// Generated on 12/11/2014 19:01:30
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MountInformationInPaddockRequestMessage : Message
    {
        public new const uint ID =5975;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int mapRideId;
        
        public MountInformationInPaddockRequestMessage()
        {
        }
        
        public MountInformationInPaddockRequestMessage(int mapRideId)
        {
            this.mapRideId = mapRideId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(mapRideId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            mapRideId = reader.ReadInt();
        }
        
    }
    
}