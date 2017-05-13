using System;
using System.IO;
using System.Text;

namespace BlueSheep.Util.IO
{
    [Serializable]
    public class BigEndianWriter : BotForgeAPI.IO.BigEndianWriter, IDisposable
    {

        #region new BigEndianWriter

        #region Properties
        public byte[] Content
        {
            get
            {
                byte[] content = new byte[BaseStream.Length];

                BaseStream.Position = 0;
                BaseStream.Read(content, 0, (int)BaseStream.Length);

                return content;
            }
            set
            {
                BaseStream.Position = 0;
                BaseStream.Write(value, 0, value.Length);
            }
        }

        public BigEndianWriter()
        {
        }

        public BigEndianWriter(Stream stream) : base(stream)
        {
        }

        public BigEndianWriter(byte[] buffer) : base(buffer)
        {
        }
        #endregion

        #endregion
    }
}
