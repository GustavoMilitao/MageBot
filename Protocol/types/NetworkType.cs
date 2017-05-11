using System;

namespace BlueSheep.Protocol.Types
{
    public abstract class NetworkType : MarshalByRefObject
    {
        public abstract int TypeID { get; }

        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);
    }
}
