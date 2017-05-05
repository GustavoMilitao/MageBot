









// Generated on 12/11/2014 19:01:55
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class SetUpdateMessage : Message
    {
        public new const uint ID =5503;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int setId;
        public int[] setObjects;
        public Types.ObjectEffect[] setEffects;
        
        public SetUpdateMessage()
        {
        }
        
        public SetUpdateMessage(int setId, int[] setObjects, Types.ObjectEffect[] setEffects)
        {
            this.setId = setId;
            this.setObjects = setObjects;
            this.setEffects = setEffects;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)setId);
            writer.WriteUShort((ushort)setObjects.Length);
            foreach (var entry in setObjects)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)setEffects.Length);
            foreach (var entry in setEffects)
            {
                 writer.WriteShort((short)entry.TypeId);
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            setId = reader.ReadVarUhShort();
            if (setId < 0)
                throw new Exception("Forbidden value on setId = " + setId + ", it doesn't respect the following condition : setId < 0");
            var limit = reader.ReadUShort();
            setObjects = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 setObjects[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            setEffects = new Types.ObjectEffect[limit];
            for (int i = 0; i < limit; i++)
            {
                 setEffects[i] = Types.ProtocolTypeManager.GetInstance<Types.ObjectEffect>(reader.ReadUShort());
                 setEffects[i].Deserialize(reader);
            }
        }
        
    }
    
}