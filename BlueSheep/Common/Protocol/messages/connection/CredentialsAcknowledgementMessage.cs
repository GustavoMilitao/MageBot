namespace BlueSheep.Common.Protocol.Messages.Connection
{
    using BlueSheep.Engine.Types;

 	 public class CredentialsAcknowledgementMessage : Message 
    {
        public new const int ID = 6314;
        public override int MessageID { get { return ID; } }

        public CredentialsAcknowledgementMessage() { }

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