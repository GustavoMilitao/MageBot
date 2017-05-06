namespace BlueSheep.Common.Protocol.Messages.Security
{
    using BlueSheep.Engine.Types;

 	 public class ClientKeyMessage : Message 
    {
        public new const int ID = 5607;
        public override int MessageID { get { return ID; } }

        public string Key { get; set; }

        public ClientKeyMessage() { }

        public ClientKeyMessage(string key)
        {
            Key = key;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Key);
        }

        public override void Deserialize(IDataReader reader)
        {
            _keyFunc(reader);
        }

        private void _keyFunc(IDataReader Reader)
        {
            Key = Reader.ReadUTF();
        }
    }
}
