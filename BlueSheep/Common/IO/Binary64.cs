using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSheep.Common.IO
{
    public class Binary64
    {
        public uint low;
        public uint high;

        public Binary64(uint param1 = 0, uint param2 = 0)
        {
            low = param1;
            high = param2;
        }
    }
}
