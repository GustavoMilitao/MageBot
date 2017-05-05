


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayPortalInformations : GameRolePlayActorInformations
    {

        public new const int ID = 467;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.PortalInformation portal;


        public GameRolePlayPortalInformations()
        {
        }

        public GameRolePlayPortalInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, Types.PortalInformation portal)
                 : base(contextualId, look, disposition)
        {
            this.portal = portal;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteShort((short)portal.TypeId);
            portal.Serialize(writer);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            portal = Types.ProtocolTypeManager.GetInstance<PortalInformation>(reader.ReadUShort());
            portal.Deserialize(reader);


        }


    }


}