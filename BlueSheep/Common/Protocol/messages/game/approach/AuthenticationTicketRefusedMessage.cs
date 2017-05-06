using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages.Game.Approach
{
    public class AuthenticationTicketRefusedMessage : Message
    {
        public new const int ID = 112;
        public override int MessageID { get { return ID; } }

        public AuthenticationTicketRefusedMessage() { }

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
