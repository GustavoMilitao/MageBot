using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages.Game.Approach
{
    public class AuthenticationTicketAcceptedMessage : Message
    {
        public new const int ID = 111;
        public override int MessageID { get { return ID; } }

        public AuthenticationTicketAcceptedMessage() { }

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
