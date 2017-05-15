using System.Collections.Generic;

namespace Core.Engine.Types
{
    public class SelectedServerDataExtendedMessage : SelectedServerDataMessage
    {
        public new const int Id = 6469;

        public List<ushort> ServerIds;

        public SelectedServerDataExtendedMessage() { }

        public SelectedServerDataExtendedMessage(List<ushort> serverIds)
        {
            ServerIds = serverIds;
        }

        public SelectedServerDataExtendedMessage(bool canCreateNewCharacter, ushort serverId, string address, ushort port, List<int> ticket, List<ushort> serverIds)
          : base(canCreateNewCharacter, serverId, address, port, ticket)
        {
            this.ServerIds = serverIds;
        }

        public override void Serialize(BotForgeAPI.IO.IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)ServerIds.Count);
            for (int i = 0; i < ServerIds.Count; i++)
            {
                writer.WriteVarUhShort(ServerIds[i]);
            }
        }

        public override void Deserialize(BotForgeAPI.IO.IDataReader reader)
        {
            base.Deserialize(reader);
            ushort length = reader.ReadUShort();
            ServerIds = new List<ushort>();
            for (int i = 0; i < length; i++)
            {
                ServerIds.Add(reader.ReadVarUhShort());
            }
        }
    }
}
