









// Generated on 12/11/2014 19:01:16
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightCloseCombatMessage : AbstractGameActionFightTargetedAbilityMessage
    {
        public new const uint ID =6116;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int weaponGenericId;
        
        public GameActionFightCloseCombatMessage()
        {
        }
        
        public GameActionFightCloseCombatMessage(int actionId, ulong sourceId, ulong targetId, int destinationCellId, sbyte critical, bool silentCast, int weaponGenericId)
         : base(actionId, sourceId, targetId, destinationCellId, critical, silentCast)
        {
            this.weaponGenericId = weaponGenericId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort((short)weaponGenericId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            weaponGenericId = reader.ReadVarUhShort();
            if (weaponGenericId < 0)
                throw new Exception("Forbidden value on weaponGenericId = " + weaponGenericId + ", it doesn't respect the following condition : weaponGenericId < 0");
        }
        
    }
    
}