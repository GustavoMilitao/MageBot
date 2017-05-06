namespace BlueSheep.Common.Protocol.Messages.Connection
{
    using BlueSheep.Engine.Types;

 	 public class ServerSelectionMessage : Message 
    {
        public new const int ID = 40;
        public override int MessageID { get { return ID; } }

        private ushort _serverId;

        public ServerSelectionMessage() { }

        public ServerSelectionMessage(ushort serverId)
        {
            _serverId = serverId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(_serverId);
        }

        public override void Deserialize(IDataReader reader)
        {
            _serverId = reader.ReadVarUhShort();
        }
    }
}
