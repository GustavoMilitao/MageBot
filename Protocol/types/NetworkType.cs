using System;

namespace MageBot.Protocol.Types
{
    public abstract class NetworkType : MarshalByRefObject
    {
        public abstract int ProtocolId { get; }
        public abstract int TypeID { get; }

        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);
    }
}
