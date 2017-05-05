









// Generated on 12/11/2014 19:01:41
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ValidateSpellForgetMessage : Message
    {
        public new const uint ID =1700;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public short spellId;
        
        public ValidateSpellForgetMessage()
        {
        }
        
        public ValidateSpellForgetMessage(short spellId)
        {
            this.spellId = spellId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort(spellId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            spellId = reader.ReadVarShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
        }
        
    }
    
}