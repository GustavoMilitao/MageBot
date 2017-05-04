using DofusBot.Core.Network;
using System;

namespace DofusBot.Network
{
    public class PacketEventArg : EventArgs
    {
        private NetworkMessage _packet;

        public NetworkMessage Packet
        {
            get { return _packet; }
        }

        public PacketEventArg(NetworkMessage packet)
        {
            _packet = packet;
        }
    }
}
