namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay
{
    using BlueSheep.Engine.Types;

 	 public class CurrentMapMessage : Message 
    {
        public new const int ID = 220;
        public override int MessageID { get { return ID; } }

        public int MapId { get; set; }
        public string MapKey { get; set; }

        public CurrentMapMessage() { }

        public CurrentMapMessage(int mapId, string mapKey)
        {
            MapId = mapId;
            MapKey = mapKey;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(MapId);
            writer.WriteUTF(MapKey);
        }

        public override void Deserialize(IDataReader reader)
        {
            _mapIdFunc(reader);
            _mapKeyFunc(reader);
        }

        private void _mapIdFunc(IDataReader reader)
        {
            MapId = reader.ReadInt();
        }

        private void _mapKeyFunc(IDataReader reader)
        {
            MapKey = reader.ReadUTF();
        }
    }
}
