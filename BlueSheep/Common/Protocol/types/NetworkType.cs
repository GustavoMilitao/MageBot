using System;

namespace DofusBot.Protocol.Network.Types
{
    public abstract class NetworkType : MarshalByRefObject
    {
        public abstract short TypeID { get; }

        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);
    }
}
