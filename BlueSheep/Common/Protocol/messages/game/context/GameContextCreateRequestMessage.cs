namespace BlueSheep.Common.Protocol.Messages.Game.Context
{
    using BlueSheep.Engine.Types;

 	 public class GameContextCreateRequestMessage : Message 
    {
        public new const int ID = 250;
        public override int MessageID { get { return ID; } }

        public GameContextCreateRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            //
        }

        public override void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
