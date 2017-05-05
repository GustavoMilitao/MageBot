









// Generated on 12/11/2014 19:01:44
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildInvitationStateRecrutedMessage : Message
    {
        public new const uint ID =5548;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte invitationState;
        
        public GuildInvitationStateRecrutedMessage()
        {
        }
        
        public GuildInvitationStateRecrutedMessage(sbyte invitationState)
        {
            this.invitationState = invitationState;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(invitationState);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            invitationState = reader.ReadSByte();
            if (invitationState < 0)
                throw new Exception("Forbidden value on invitationState = " + invitationState + ", it doesn't respect the following condition : invitationState < 0");
        }
        
    }
    
}