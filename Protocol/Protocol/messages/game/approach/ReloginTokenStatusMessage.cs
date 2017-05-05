









// Generated on 12/11/2014 19:01:21
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ReloginTokenStatusMessage : Message
    {
        public new const uint ID =6539;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool validToken;
        public string token;
        
        public ReloginTokenStatusMessage()
        {
        }
        
        public ReloginTokenStatusMessage(bool validToken, string token)
        {
            this.validToken = validToken;
            this.token = token;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(validToken);
            writer.WriteUTF(token);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            validToken = reader.ReadBoolean();
            token = reader.ReadUTF();
        }
        
    }
    
}