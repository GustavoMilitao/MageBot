using DofusBot.Packet;
using System;

namespace DofusBot.Network
{
    public class NullPacketEventArg : EventArgs
    {
        private ServerPacketEnum _packetType;

        public ServerPacketEnum PacketType
        {
            get { return _packetType; }
        }

        public NullPacketEventArg(ServerPacketEnum packetType)
        {
            _packetType = packetType;
        }
    }
}
