using System;

namespace DofusBot.Packet.Types
{
    public abstract class NetworkType : MarshalByRefObject
    {
        public abstract short TypeID { get; }

        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);
    }
}
