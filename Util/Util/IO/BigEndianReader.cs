using System;
using System.IO;
using System.Text;

namespace BlueSheep.Util.IO
{
    public class BigEndianReader : BotForgeAPI.IO.BigEndianReader, IDisposable
    {

        #region new Big Endian Reader

        #region Public Methods

        public void Seek(int position)
        {
            BaseStream.Position = position;
        }

        public BigEndianReader() : base()
        {
        }
        public BigEndianReader(Stream stream) : base(stream)
        {

        }
        public BigEndianReader(byte[] tab) : base(tab)
        {

        }

        #endregion

        #endregion
    }
}
