


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class MapObstacle
    {

        public new const int ID = 200;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int obstacleCellId;
        public byte state;


        public MapObstacle()
        {
        }

        public MapObstacle(int obstacleCellId, byte state)
        {
            this.obstacleCellId = obstacleCellId;
            this.state = state;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)obstacleCellId);
            writer.WriteByte(state);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            obstacleCellId = reader.ReadVarUhShort();
            if (obstacleCellId < 0 || obstacleCellId > 559)
                throw new Exception("Forbidden value on obstacleCellId = " + obstacleCellId + ", it doesn't respect the following condition : obstacleCellId < 0 || obstacleCellId > 559");
            state = reader.ReadByte();
            if (state < 0)
                throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");


        }


    }


}