


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class ObjectItem : Item
    {

        public new const int ID = 37;
        public override int TypeId
        {
            get { return ID; }
        }

        public byte Position;
        public int ObjectGID;
        public Types.ObjectEffect[] Effects;
        public int ObjectUID;
        public int Quantity;


        public ObjectItem()
        {
        }

        public ObjectItem(byte position, int objectGID, Types.ObjectEffect[] effects, int objectUID, int quantity)
        {
            Position = position;
            ObjectGID = objectGID;
            Effects = effects;
            ObjectUID = objectUID;
            Quantity = quantity;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteByte(Position);
            writer.WriteVarShort((short)ObjectGID);
            writer.WriteUShort((ushort)Effects.Length);
            foreach (var entry in Effects)
            {
                writer.WriteShort((short)entry.TypeId);
                entry.Serialize(writer);
            }
            writer.WriteVarInt(ObjectUID);
            writer.WriteVarInt(Quantity);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            Position = reader.ReadByte();
            if (Position < 0 || Position > 255)
                throw new Exception("Forbidden value on position = " + Position + ", it doesn't respect the following condition : position < 0 || position > 255");
            ObjectGID = reader.ReadVarUhShort();
            if (ObjectGID < 0)
                throw new Exception("Forbidden value on objectGID = " + ObjectGID + ", it doesn't respect the following condition : objectGID < 0");
            var limit = reader.ReadUShort();
            Effects = new Types.ObjectEffect[limit];
            for (int i = 0; i < limit; i++)
            {
                Effects[i] = Types.ProtocolTypeManager.GetInstance<Types.ObjectEffect>(reader.ReadUShort());
                Effects[i].Deserialize(reader);
            }
            ObjectUID = reader.ReadVarInt();
            if (ObjectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + ObjectUID + ", it doesn't respect the following condition : objectUID < 0");
            Quantity = reader.ReadVarInt();
            if (Quantity < 0)
                throw new Exception("Forbidden value on quantity = " + Quantity + ", it doesn't respect the following condition : quantity < 0");


        }


    }


}