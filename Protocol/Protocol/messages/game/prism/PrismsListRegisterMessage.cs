









// Generated on 12/11/2014 19:01:58
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismsListRegisterMessage : Message
    {
        public new const uint ID =6441;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte listen;
        
        public PrismsListRegisterMessage()
        {
        }
        
        public PrismsListRegisterMessage(sbyte listen)
        {
            this.listen = listen;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(listen);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            listen = reader.ReadSByte();
            if (listen < 0)
                throw new Exception("Forbidden value on listen = " + listen + ", it doesn't respect the following condition : listen < 0");
        }
        
    }
    
}