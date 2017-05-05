









// Generated on 12/11/2014 19:01:57
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismFightAttackerAddMessage : Message
    {
        public new const uint ID =5893;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int subAreaId;
        public int fightId;
        public Types.CharacterMinimalPlusLookInformations attacker;
        
        public PrismFightAttackerAddMessage()
        {
        }
        
        public PrismFightAttackerAddMessage(int subAreaId, int fightId, Types.CharacterMinimalPlusLookInformations attacker)
        {
            this.subAreaId = subAreaId;
            this.fightId = fightId;
            this.attacker = attacker;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)subAreaId);
            writer.WriteVarShort((short)fightId);
            writer.WriteShort((short)attacker.TypeId);
            attacker.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            fightId = reader.ReadVarUhShort();
            if (fightId < 0)
                throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
            attacker = Types.ProtocolTypeManager.GetInstance<Types.CharacterMinimalPlusLookInformations>(reader.ReadUShort());
            attacker.Deserialize(reader);
        }
        
    }
    
}