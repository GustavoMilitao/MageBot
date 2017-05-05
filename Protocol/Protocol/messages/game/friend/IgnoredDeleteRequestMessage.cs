









// Generated on 12/11/2014 19:01:43
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class IgnoredDeleteRequestMessage : Message
    {
        public new const uint ID =5680;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int accountId;
        public bool session;
        
        public IgnoredDeleteRequestMessage()
        {
        }
        
        public IgnoredDeleteRequestMessage(int accountId, bool session)
        {
            this.accountId = accountId;
            this.session = session;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(accountId);
            writer.WriteBoolean(session);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
            session = reader.ReadBoolean();
        }
        
    }
    
}