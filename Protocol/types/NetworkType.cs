using System;

namespace BlueSheep.Protocol.Types
{
    public abstract class NetworkType : MarshalByRefObject
    {
        protected abstract int ProtocolId { get; set; }
        public abstract int TypeID { get; }

        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);
    }
}
