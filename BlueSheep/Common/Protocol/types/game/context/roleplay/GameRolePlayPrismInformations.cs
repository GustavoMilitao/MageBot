


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayPrismInformations : GameRolePlayActorInformations
    {

        public new const int ID = 161;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.PrismInformation prism;


        public GameRolePlayPrismInformations()
        {
        }

        public GameRolePlayPrismInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, Types.PrismInformation prism)
                 : base(contextualId, look, disposition)
        {
            this.prism = prism;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteShort((short)prism.TypeId);
            prism.Serialize(writer);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            prism = Types.ProtocolTypeManager.GetInstance<PrismInformation>(reader.ReadUShort());
            prism.Deserialize(reader);


        }


    }


}