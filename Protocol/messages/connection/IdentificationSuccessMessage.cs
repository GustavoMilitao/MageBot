namespace MageBot.Protocol.Messages.Connection
{
    public class IdentificationSuccessMessage : Message
    {
        public string Login;
        public string Nickname;
        public int AccountId;
        public byte CommunityId;
        public bool HasRights;
        public string SecretQuestion;
        public double AccountCreation;
        public double SubscriptionElapsedDuration;
        public double SubscriptionEndDate;
        public bool WasAlreadyConnected;
        public uint HavenbagAvailableRoom;

        protected override int ProtocolId { get; set; } = 22;
        public override int MessageID { get { return ProtocolId; } }

        public IdentificationSuccessMessage() { }

        public IdentificationSuccessMessage(string login, string nickname, int accountId, byte communityId, bool hasRights, string secretQuestion, double accountCreation, double subscriptionElapsedDuration, double subscriptionEndDate, bool wasAlreadyConnected, uint havenbagAvailableRoom)
        {
            Login = login;
            Nickname = nickname;
            AccountId = accountId;
            CommunityId = communityId;
            HasRights = hasRights;
            SecretQuestion = secretQuestion;
            AccountCreation = accountCreation;
            SubscriptionElapsedDuration = subscriptionElapsedDuration;
            SubscriptionEndDate = subscriptionEndDate;
            WasAlreadyConnected = wasAlreadyConnected;
            HavenbagAvailableRoom = havenbagAvailableRoom;
        }

        public override void Serialize(IDataWriter writer)
        {
            byte flag = new byte();
            BooleanByteWrapper.SetFlag(0, flag, HasRights);
            BooleanByteWrapper.SetFlag(1, flag, WasAlreadyConnected);
            writer.WriteByte(flag);
            writer.WriteUTF(Login);
            writer.WriteUTF(Nickname);
            writer.WriteInt(AccountId);
            writer.WriteByte(CommunityId);
            writer.WriteUTF(SecretQuestion);
            writer.WriteDouble(AccountCreation);
            writer.WriteDouble(SubscriptionElapsedDuration);
            writer.WriteDouble(SubscriptionEndDate);
        }

        public override void Deserialize(IDataReader reader)
        {
            byte flag = reader.ReadByte();
            HasRights = BooleanByteWrapper.GetFlag(flag, 0);
            WasAlreadyConnected = BooleanByteWrapper.GetFlag(flag, 1);
            Login = reader.ReadUTF();
            Nickname = reader.ReadUTF();
            AccountId = reader.ReadInt();
            CommunityId = reader.ReadByte();
            SecretQuestion = reader.ReadUTF();
            AccountCreation = reader.ReadDouble();
            SubscriptionElapsedDuration = reader.ReadDouble();
            SubscriptionEndDate = reader.ReadDouble();
        }
    }
}