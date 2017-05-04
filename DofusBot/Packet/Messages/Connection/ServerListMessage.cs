using DofusBot.Core.Network;
using DofusBot.Packet.Types.Connection;
using System.Collections.Generic;

namespace DofusBot.Packet.Messages.Connection
{
    public class ServerListMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.ServerListMessage; }
        }

        public List<GameServerInformations> Servers;
        public ushort AlreadyConnectedToServerId;
        public bool CanCreateNewCharacter;

        public const uint ProtocolId = 30;
        public override uint MessageID { get { return ProtocolId; } }

        public ServerListMessage()
        {
        }

        public ServerListMessage(List<GameServerInformations> servers, ushort alreadyConnectedToServerId, bool canCreateNewCharacter)
        {
            Servers = servers;
            AlreadyConnectedToServerId = alreadyConnectedToServerId;
            CanCreateNewCharacter = canCreateNewCharacter;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(Servers.Count)));
            for (int serversIndex = 0; (serversIndex < Servers.Count); serversIndex = (serversIndex + 1))
            {
                GameServerInformations objectToSend = Servers[serversIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarShort((short)AlreadyConnectedToServerId);
            writer.WriteBoolean(CanCreateNewCharacter);
        }

        public override void Deserialize(IDataReader reader)
        {
            int serversCount = reader.ReadUShort();
            int serversIndex;
            Servers = new List<GameServerInformations>();
            for (serversIndex = 0; (serversIndex < serversCount); serversIndex = (serversIndex + 1))
            {
                GameServerInformations objectToAdd = new GameServerInformations();
                objectToAdd.Deserialize(reader);
                Servers.Add(objectToAdd);
            }
            AlreadyConnectedToServerId = reader.ReadVarUhShort();
            CanCreateNewCharacter = reader.ReadBoolean();
        }
    }
}
