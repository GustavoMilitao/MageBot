









// Generated on 12/11/2014 19:01:58
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class URLOpenMessage : Message
    {
        public new const uint ID =6266;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte urlId;
        
        public URLOpenMessage()
        {
        }
        
        public URLOpenMessage(sbyte urlId)
        {
            this.urlId = urlId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(urlId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            urlId = reader.ReadSByte();
            if (urlId < 0)
                throw new Exception("Forbidden value on urlId = " + urlId + ", it doesn't respect the following condition : urlId < 0");
        }
        
    }
    
}