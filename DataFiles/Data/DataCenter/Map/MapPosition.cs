using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFiles.Data.DataCenter.Map
{
        public class MapPosition
        {
            public int capabilities;
            public bool hasPriorityOnWorldmap;
            public ulong id;
            public int nameId;
            public bool outdoor;
            public int posX;
            public int posY;
            //public List<AmbientSound> sounds;
            public int subAreaId;
            public int worldMap;

            public MapPosition()
            {

            }
        }
}
