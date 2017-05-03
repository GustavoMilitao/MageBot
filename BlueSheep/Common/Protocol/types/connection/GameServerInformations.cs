


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;
using BlueSheep.Util.Enums.Servers;
using DofusBot.Enums;

namespace BlueSheep.Common.Protocol.Types
{

    public class GameServerInformations
    {

        public new const short ID = 25;
        public virtual short TypeId
        {
            get { return ID; }
        }

        public ushort id;
        public ServerStatusEnum status;
        public byte completion;
        public bool isSelectable;
        public byte charactersCount;
        public double date;
        public byte CharactersSlots { get; set; }
        public GameTypeId ServerType { get; set; }


        public GameServerInformations()
        {
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort(id);
            writer.WriteByte(Convert.ToByte(ServerType));
            writer.WriteByte(Convert.ToByte(status));
            writer.WriteByte(completion);
            writer.WriteBoolean(isSelectable);
            writer.WriteByte(charactersCount);
            writer.WriteByte(CharactersSlots);
            writer.WriteDouble(date);

        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            id = reader.ReadVarUhShort();
            if (id < 0 || id > 65535)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0 || id > 65535");
            ServerType = (GameTypeId)reader.ReadByte();
            if(ServerType < 0)
                throw new Exception("Forbidden value on ServerType = " + ServerType + ", it doesn't respect the following condition : ServerType < 0 ");
            status = (ServerStatusEnum)reader.ReadByte();
            if (status < 0)
                throw new Exception("Forbidden value on status = " + status + ", it doesn't respect the following condition : status < 0");
            completion = reader.ReadByte();
            if (completion < 0)
                throw new Exception("Forbidden value on completion = " + completion + ", it doesn't respect the following condition : completion < 0");
            isSelectable = reader.ReadBoolean();
            charactersCount = reader.ReadByte();
            if (charactersCount < 0)
                throw new Exception("Forbidden value on charactersCount = " + charactersCount + ", it doesn't respect the following condition : charactersCount < 0");
            CharactersSlots = reader.ReadByte();
            if (CharactersSlots < 0)
                throw new Exception("Forbidden value on CharactersSlots = " + CharactersSlots + ", it doesn't respect the following condition : CharactersSlots < 0");
            date = reader.ReadDouble();
            if (date < -9.007199254740992E15 || date > 9.007199254740992E15)
                throw new Exception("Forbidden value on date = " + date + ", it doesn't respect the following condition : date < -9.007199254740992E15 || date > 9.007199254740992E15");


        }


    }


}