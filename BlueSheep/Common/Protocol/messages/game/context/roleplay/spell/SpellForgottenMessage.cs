









// Generated on 12/11/2014 19:01:41
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class SpellForgottenMessage : Message
    {
        public new const uint ID =5834;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] spellsId;
        public int boostPoint;
        
        public SpellForgottenMessage()
        {
        }
        
        public SpellForgottenMessage(int[] spellsId, int boostPoint)
        {
            this.spellsId = spellsId;
            this.boostPoint = boostPoint;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)spellsId.Length);
            foreach (var entry in spellsId)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteVarShort((short)boostPoint);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            spellsId = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 spellsId[i] = reader.ReadVarUhShort();
            }
            boostPoint = reader.ReadVarUhShort();
            if (boostPoint < 0)
                throw new Exception("Forbidden value on boostPoint = " + boostPoint + ", it doesn't respect the following condition : boostPoint < 0");
        }
        
    }
    
}