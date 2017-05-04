using DofusBot.Core;
using DofusBot.Core.Network;
using DofusBot.Packet;
using System;

namespace DofusBot.Network
{
    public class DofusBotPacketDeserializer
    {
        public event EventHandler<PacketEventArg> ReceivePacket;
        public delegate void ReceivePacketEventHandler(PacketEventArg e);
        public event EventHandler<NullPacketEventArg> ReceiveNullPacket;
        public delegate void ReceiveNullPacketEventHandler(NullPacketEventArg e);

        protected virtual void OnReceivePacket(ServerPacketEnum packetType, PacketEventArg e)
        {
            if (e.Packet == null)
                ReceiveNullPacket.Raise(this, new NullPacketEventArg(packetType));
            else
                ReceivePacket.Raise(this, e);
        }

        public void GetPacket(object obj, PacketBufferEventArg e)
        {
            ServerPacketEnum packetType = (ServerPacketEnum) e.PacketId;
            BigEndianReader reader = new BigEndianReader(e.Data);
            NetworkMessage msg = MessageReceiver.BuildMessage((uint) packetType, reader);
            OnReceivePacket(packetType, new PacketEventArg(msg));
        }
    }
}
