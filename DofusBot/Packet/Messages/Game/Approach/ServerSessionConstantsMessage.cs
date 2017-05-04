using DofusBot.Core.Network;
using DofusBot.Packet.Types.Game.Approach;
using System.Collections.Generic;

namespace DofusBot.Packet.Messages.Game.Approach
{
    class ServerSessionConstantsMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.ServerSessionConstantsMessage; }
        }

        public List<ServerSessionConstant> Variables;

        public const uint ProtocolId = 6434;
        public override uint MessageID { get { return ProtocolId; } }

        public ServerSessionConstantsMessage() { }

        public ServerSessionConstantsMessage(List<ServerSessionConstant> variables)
        {
            Variables = variables;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)(Variables.Count));
            for (int i = 0; i < Variables.Count; i++)
            {
                ServerSessionConstant objectToSend = Variables[i];
                writer.WriteShort(objectToSend.TypeID);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            int length = reader.ReadUShort();
            Variables = new List<ServerSessionConstant>();
            for (int i = 0; i < length; i++)
            {
                ServerSessionConstant objectToAdd = new ServerSessionConstant(reader.ReadUShort());
                objectToAdd.Deserialize(reader);
                Variables.Add(objectToAdd);
            }
        }
    }
}
