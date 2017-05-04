using DofusBot.Core.Network;
using System.Collections.Generic;

namespace DofusBot.Packet.Messages.Game.Approach
{
    class ServerOptionalFeaturesMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.ServerOptionalFeaturesMessage; }
        }

        public List<byte> Features;

        public const uint ProtocolId = 6305;
        public override uint MessageID { get { return ProtocolId; } }

        public ServerOptionalFeaturesMessage() { }

        public ServerOptionalFeaturesMessage(List<byte> features)
        {
            Features = features;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Features.Count);
            for (int i = 0; i < Features.Count; i++)
            {
                writer.WriteByte(Features[i]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            ushort lenght = reader.ReadUShort();
            Features = new List<byte>();
            for (int i = 0; i < lenght; i++)
            {
                Features.Add(reader.ReadByte());
            }
        }
    }
}
