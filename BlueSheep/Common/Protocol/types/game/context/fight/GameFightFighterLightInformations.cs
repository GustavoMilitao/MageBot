


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameFightFighterLightInformations
    {

        public new const int ID = 413;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public ulong id;
        public sbyte wave;
        public int level;
        public sbyte breed;
        public bool sex;

        public bool alive;


        public GameFightFighterLightInformations()
        {
        }

        public GameFightFighterLightInformations(ulong id, sbyte wave, int level, sbyte breed)
        {
            this.id = id;
            this.wave = wave;
            this.level = level;
            this.breed = breed;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {
            byte b = 0;
            b = BooleanByteWrapper.SetFlag(b, 0, sex);
            b = BooleanByteWrapper.SetFlag(b, 1, alive);
            writer.WriteByte(b);
            writer.WriteULong(id);
            writer.WriteSByte(wave);
            writer.WriteVarShort((short)level);
            writer.WriteSByte(breed);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            byte _loc2_ = reader.ReadByte();
            sex = BooleanByteWrapper.GetFlag(_loc2_, 0);
            alive = BooleanByteWrapper.GetFlag(_loc2_, 1);
            id = reader.ReadULong();
            wave = reader.ReadSByte();
            if (wave < 0)
                throw new Exception("Forbidden value on wave = " + wave + ", it doesn't respect the following condition : wave < 0");
            level = reader.ReadVarUhShort();
            if (level < 0)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0");
            breed = reader.ReadSByte();


        }


    }


}