


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayMerchantInformations : GameRolePlayNamedActorInformations
    {

        public new const int ID = 129;
        public override int TypeId
        {
            get { return ID; }
        }

        public byte sellType;
        public Types.HumanOption[] options;


        public GameRolePlayMerchantInformations()
        {
        }

        public GameRolePlayMerchantInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name, byte sellType, Types.HumanOption[] options)
                 : base(contextualId, look, disposition, name)
        {
            this.sellType = sellType;
            this.options = options;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteByte(sellType);
            writer.WriteUShort((ushort)options.Length);
            foreach (var entry in options)
            {
                writer.WriteShort((short)entry.TypeId);
                entry.Serialize(writer);
            }


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            sellType = reader.ReadByte();
            if (sellType < 0)
                throw new Exception("Forbidden value on sellType = " + sellType + ", it doesn't respect the following condition : sellType < 0");
            var limit = reader.ReadUShort();
            options = new Types.HumanOption[limit];
            for (int i = 0; i < limit; i++)
            {
                options[i] = Types.ProtocolTypeManager.GetInstance<HumanOption>(reader.ReadUShort());
                options[i].Deserialize(reader);
            }


        }


    }


}