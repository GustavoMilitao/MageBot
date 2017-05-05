









// Generated on 12/11/2014 19:01:24
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ChatClientMultiMessage : ChatAbstractClientMessage
    {
        public new const uint ID =861;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte channel;
        
        public ChatClientMultiMessage()
        {
        }
        
        public ChatClientMultiMessage(string content, sbyte channel)
         : base(content)
        {
            this.channel = channel;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(channel);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            channel = reader.ReadSByte();
            if (channel < 0)
                throw new Exception("Forbidden value on channel = " + channel + ", it doesn't respect the following condition : channel < 0");
        }
        
    }
    
}