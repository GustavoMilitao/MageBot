









// Generated on 12/11/2014 19:01:51
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeSetCraftRecipeMessage : Message
    {
        public new const uint ID =6389;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int objectGID;
        
        public ExchangeSetCraftRecipeMessage()
        {
        }
        
        public ExchangeSetCraftRecipeMessage(int objectGID)
        {
            this.objectGID = objectGID;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)objectGID);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            objectGID = reader.ReadVarUhShort();
            if (objectGID < 0)
                throw new Exception("Forbidden value on objectGID = " + objectGID + ", it doesn't respect the following condition : objectGID < 0");
        }
        
    }
    
}