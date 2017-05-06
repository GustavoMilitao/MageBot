using BlueSheep.Common.IO;

namespace BlueSheep.Engine.Types
{
    public abstract class Message
    {
        #region Properties
        public virtual int MessageID
        {
            get
            {
                return 0;
            }
        }
        #endregion

        #region Public methods
        private const byte BIT_RIGHT_SHIFT_LEN_PACKET_ID = 2;
        private const byte BIT_MASK = 3;

        public void Unpack(BigEndianReader reader)
        {
            Deserialize(reader);
        }

        public void Pack(BigEndianWriter writer)
        {
            Serialize(writer);
            WritePacket(writer);
        }

        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);

        public virtual void WritePacket(IDataWriter writer)
        {
            byte[] packet = writer.Data;

            writer.Clear();

            byte typeLen = ComputeTypeLen(packet.Length);
            var header = (int)SubComputeStaticHeader((uint)MessageID, typeLen);
            writer.WriteShort((short)header);

            switch (typeLen)
            {
                case 0:
                    break;
                case 1:
                    writer.WriteByte((byte)packet.Length);
                    break;
                case 2:
                    writer.WriteShort((short)(int)packet.Length);
                    break;
                case 3:
                    writer.WriteByte((byte)(packet.Length >> 16 & 255));
                    writer.WriteShort((short)(int)(packet.Length & 65535));
                    break;
                default:
                    throw new System.Exception("Packet's length can't be encoded on 4 or more bytes");
            }
            writer.WriteBytes(packet);
        }

        protected static byte ComputeTypeLen(int param1)
        {
            if (param1 > 65535)
                return 3;

            if (param1 > 255)
                return 2;

            if (param1 > 0)
                return 1;

            return 0;
        }

        protected static uint SubComputeStaticHeader(uint id, byte typeLen)
        {
            return id << BIT_RIGHT_SHIFT_LEN_PACKET_ID | typeLen;
        }

        public override string ToString()
        {
            return GetType().Name;
        }

        #endregion
    }
}
