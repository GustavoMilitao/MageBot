


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class PortalInformation
    {

        public new const int ID = 466;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int portalId;
        public int areaId;


        public PortalInformation()
        {
        }

        public PortalInformation(int portalId, int areaId)
        {
            this.portalId = portalId;
            this.areaId = areaId;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteInt(portalId);
            writer.WriteShort((short)areaId);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            portalId = reader.ReadInt();
            if (portalId < 0)
                throw new Exception("Forbidden value on portalId = " + portalId + ", it doesn't respect the following condition : portalId < 0");
            areaId = reader.ReadShort();


        }


    }


}