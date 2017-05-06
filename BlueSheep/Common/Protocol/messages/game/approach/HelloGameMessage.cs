using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages.Game.Approach
{
    public class HelloGameMessage : Message
    {
        public new const int ID = 101;
        public override int MessageID { get { return ID; } }

        public HelloGameMessage() { }

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
