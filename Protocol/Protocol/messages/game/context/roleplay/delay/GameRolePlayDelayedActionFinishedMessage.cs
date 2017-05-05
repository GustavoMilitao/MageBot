









// Generated on 12/11/2014 19:01:32
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayDelayedActionFinishedMessage : Message
    {
        public new const uint ID =6150;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int delayedCharacterId;
        public sbyte delayTypeId;
        
        public GameRolePlayDelayedActionFinishedMessage()
        {
        }
        
        public GameRolePlayDelayedActionFinishedMessage(int delayedCharacterId, sbyte delayTypeId)
        {
            this.delayedCharacterId = delayedCharacterId;
            this.delayTypeId = delayTypeId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(delayedCharacterId);
            writer.WriteSByte(delayTypeId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            delayedCharacterId = reader.ReadInt();
            delayTypeId = reader.ReadSByte();
            if (delayTypeId < 0)
                throw new Exception("Forbidden value on delayTypeId = " + delayTypeId + ", it doesn't respect the following condition : delayTypeId < 0");
        }
        
    }
    
}