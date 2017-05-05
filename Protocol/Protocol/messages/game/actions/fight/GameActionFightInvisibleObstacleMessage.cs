









// Generated on 12/11/2014 19:01:17
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightInvisibleObstacleMessage : AbstractGameActionMessage
    {
        public new const uint ID =5820;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int sourceSpellId;
        
        public GameActionFightInvisibleObstacleMessage()
        {
        }
        
        public GameActionFightInvisibleObstacleMessage(short actionId, int sourceId, int sourceSpellId)
         : base(actionId, sourceId)
        {
            this.sourceSpellId = sourceSpellId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(sourceSpellId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            sourceSpellId = reader.ReadVarInt();
            if (sourceSpellId < 0)
                throw new Exception("Forbidden value on sourceSpellId = " + sourceSpellId + ", it doesn't respect the following condition : sourceSpellId < 0");
        }
        
    }
    
}