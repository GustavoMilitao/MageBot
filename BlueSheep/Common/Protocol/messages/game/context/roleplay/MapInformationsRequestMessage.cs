namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay
{
    using BlueSheep.Engine.Types;

 	 public class MapInformationsRequestMessage : Message 
    {
        public new const int ID = 225;
        public override int MessageID { get { return ID; } }

        public int MapId { get; set; }

        public MapInformationsRequestMessage() { }

        public MapInformationsRequestMessage(int mapId)
        {
            MapId = mapId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(MapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            _mapIdFunc(reader);
        }

        private void _mapIdFunc(IDataReader reader)
        {
            MapId = reader.ReadInt();
        }
    }
}
