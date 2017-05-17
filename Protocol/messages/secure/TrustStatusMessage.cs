namespace MageBot.Protocol.Messages.Secure
{
    class TrustStatusMessage : Message
    {
        protected override int ProtocolId { get; set; } = 6267;
        public override int MessageID { get { return ProtocolId; } }

        public bool Trusted;
        public bool Certified;

        public TrustStatusMessage() { }

        public TrustStatusMessage(bool trusted, bool certified)
        {
            Trusted = trusted;
            Certified = certified;
        }

        public override void Serialize(IDataWriter writer)
        {
            byte flag = new byte();
            BooleanByteWrapper.SetFlag(0, flag, Trusted);
            BooleanByteWrapper.SetFlag(1, flag, Certified);
            writer.WriteByte(flag);
        }

        public override void Deserialize(IDataReader reader)
        {
            byte flag = reader.ReadByte();
            Trusted = BooleanByteWrapper.GetFlag(flag, 0);
            Certified = BooleanByteWrapper.GetFlag(flag, 1);
        }
    }
}
