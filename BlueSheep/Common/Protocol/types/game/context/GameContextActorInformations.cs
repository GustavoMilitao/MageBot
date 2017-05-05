


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameContextActorInformations
    {

        public new const int ID = 150;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public ulong contextualId;
        public Types.EntityLook look;
        public Types.EntityDispositionInformations disposition;


        public GameContextActorInformations()
        {
        }

        public GameContextActorInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition)
        {
            this.contextualId = contextualId;
            this.look = look;
            this.disposition = disposition;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteULong(contextualId);
            look.Serialize(writer);
            writer.WriteShort((short)disposition.TypeId);
            disposition.Serialize(writer);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            contextualId = reader.ReadULong();
            look = new Types.EntityLook();
            look.Deserialize(reader);
            disposition = Types.ProtocolTypeManager.GetInstance<EntityDispositionInformations>(reader.ReadUShort());
            disposition.Deserialize(reader);


        }


    }


}