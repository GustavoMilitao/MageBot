using System;
using System.IO;
using System.Text;

public class BigEndianReader : IDataReader, IDisposable
{
    #region Dispose

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        m_reader.Dispose();
        m_reader = null;
    }

    #endregion

    #region Private Methods

    /// <summary>
    ///     Read bytes in big endian format
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private byte[] ReadBigEndianBytes(int count)
    {
        var bytes = new byte[count];
        int i;
        for (i = count - 1; i >= 0; i--)
            bytes[i] = (byte)BaseStream.ReadByte();
        return bytes;
    }

    #endregion

    #region Properties

    private BinaryReader m_reader;

    /// <summary>
    ///     Gets availiable bytes number in the buffer
    /// </summary>
    public long BytesAvailable
    {
        get { return m_reader.BaseStream.Length - m_reader.BaseStream.Position; }
    }

    public long Position
    {
        get { return m_reader.BaseStream.Position; }
    }


    public Stream BaseStream
    {
        get { return m_reader.BaseStream; }
    }

    #endregion

    #region Initialisation

    /// <summary>
    ///     Initializes a new instance of the <see cref="BigEndianReader" /> class.
    /// </summary>
    public BigEndianReader()
    {
        m_reader = new BinaryReader(new MemoryStream(), Encoding.UTF8);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BigEndianReader" /> class.
    /// </summary>
    /// <param name="stream">The stream.</param>
    public BigEndianReader(Stream stream)
    {
        m_reader = new BinaryReader(stream, Encoding.UTF8);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BigEndianReader" /> class.
    /// </summary>
    /// <param name="tab">Memory buffer.</param>
    public BigEndianReader(byte[] tab)
    {
        m_reader = new BinaryReader(new MemoryStream(tab), Encoding.UTF8);
    }

    #endregion

    #region Public Method

    /// <summary>
    ///     Read a Short from the Buffer
    /// </summary>
    /// <returns></returns>
    public short ReadShort()
    {
        return BitConverter.ToInt16(ReadBigEndianBytes(2), 0);
    }

    /// <summary>
    ///     Read a int from the Buffer
    /// </summary>
    /// <returns></returns>
    public int ReadInt()
    {
        return BitConverter.ToInt32(ReadBigEndianBytes(4), 0);
    }

    /// <summary>
    ///     Read a long from the Buffer
    /// </summary>
    /// <returns></returns>
    public long ReadLong()
    {
        return BitConverter.ToInt64(ReadBigEndianBytes(8), 0);
    }

    /// <summary>
    ///     Read a Float from the Buffer
    /// </summary>
    /// <returns></returns>
    public float ReadFloat()
    {
        return BitConverter.ToSingle(ReadBigEndianBytes(4), 0);
    }

    /// <summary>
    ///     Read a UShort from the Buffer
    /// </summary>
    /// <returns></returns>
    public ushort ReadUShort()
    {
        return BitConverter.ToUInt16(ReadBigEndianBytes(2), 0);
    }

    /// <summary>
    ///     Read a int from the Buffer
    /// </summary>
    /// <returns></returns>
    public uint ReadUInt()
    {
        return BitConverter.ToUInt32(ReadBigEndianBytes(4), 0);
    }

    /// <summary>
    ///     Read a long from the Buffer
    /// </summary>
    /// <returns></returns>
    public ulong ReadULong()
    {
        return BitConverter.ToUInt64(ReadBigEndianBytes(8), 0);
    }

    /// <summary>
    ///     Read a byte from the Buffer
    /// </summary>
    /// <returns></returns>
    public byte ReadByte()
    {
        return m_reader.ReadByte();
    }

    public sbyte ReadSByte()
    {
        return m_reader.ReadSByte();
    }

    public byte[] Data
    {
        get
        {
            var pos = BaseStream.Position;

            var data = new byte[BaseStream.Length];
            BaseStream.Position = 0;
            BaseStream.Read(data, 0, (int)BaseStream.Length);

            BaseStream.Position = pos;

            return data;
        }
    }

    /// <summary>
    ///     Returns N bytes from the buffer
    /// </summary>
    /// <param name="n">Number of read bytes.</param>
    /// <returns></returns>
    public byte[] ReadBytes(int n)
    {
        return m_reader.ReadBytes(n);
    }

    /// <summary>
    ///     Returns N bytes from the buffer
    /// </summary>
    /// <param name="n">Number of read bytes.</param>
    /// <returns></returns>
    public BigEndianReader ReadBytesInNewBigEndianReader(int n)
    {
        return new BigEndianReader(m_reader.ReadBytes(n));
    }

    /// <summary>
    ///     Read a Boolean from the Buffer
    /// </summary>
    /// <returns></returns>
    public bool ReadBoolean()
    {
        return m_reader.ReadByte() == 1;
    }

    /// <summary>
    ///     Read a Char from the Buffer
    /// </summary>
    /// <returns></returns>
    public char ReadChar()
    {
        return (char)ReadUShort();
    }

    /// <summary>
    ///     Read a Double from the Buffer
    /// </summary>
    /// <returns></returns>
    public double ReadDouble()
    {
        return BitConverter.ToDouble(ReadBigEndianBytes(8), 0);
    }

    /// <summary>
    ///     Read a Single from the Buffer
    /// </summary>
    /// <returns></returns>
    public float ReadSingle()
    {
        return BitConverter.ToSingle(ReadBigEndianBytes(4), 0);
    }

    /// <summary>
    ///     Read a string from the Buffer
    /// </summary>
    /// <returns></returns>
    public string ReadUTF()
    {
        var length = ReadUShort();

        var bytes = ReadBytes(length);
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    ///     Read a string from the Buffer
    /// </summary>
    /// <returns></returns>
    public string ReadUTF7BitLength()
    {
        var length = ReadInt();

        var bytes = ReadBytes(length);
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    ///     Read a string from the Buffer
    /// </summary>
    /// <returns></returns>
    public string ReadUTFBytes(ushort len)
    {
        var bytes = ReadBytes(len);

        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    ///     Skip bytes
    /// </summary>
    /// <param name="n"></param>
    public void SkipBytes(int n)
    {
        int i;
        for (i = 0; i < n; i++)
        {
            m_reader.ReadByte();
        }
    }

    public void SetPosition(int Position)
    {
        Seek(Position, SeekOrigin.Begin);
    }

    public void Seek(int offset, SeekOrigin seekOrigin)
    {
        m_reader.BaseStream.Seek(offset, seekOrigin);
    }

    /// <summary>
    ///     Add a bytes array to the end of the buffer
    /// </summary>
    public void Add(byte[] data, int offset, int count)
    {
        var pos = m_reader.BaseStream.Position;

        m_reader.BaseStream.Position = m_reader.BaseStream.Length;
        m_reader.BaseStream.Write(data, offset, count);
        m_reader.BaseStream.Position = pos;
    }

    public void Close()
    {
        BaseStream.Close();
    }

    #endregion

    #region Alternatives Methods

    public short ReadInt16()
    {
        return ReadShort();
    }

    public int ReadInt32()
    {
        return ReadInt();
    }

    public long ReadInt64()
    {
        return ReadLong();
    }

    public ushort ReadUInt16()
    {
        return ReadUShort();
    }

    public uint ReadUInt32()
    {
        return ReadUInt();
    }

    public ulong ReadUInt64()
    {
        return ReadULong();
    }

    public string ReadString()
    {
        return ReadUTF();
    }

    #endregion

    #region Custom Methods

    public short ReadVarShort()
    {
        return (short)ReadVar<ushort>();
    }

    public ushort ReadVarUhShort()
    {
        return ReadVar<ushort>();
    }

    public int ReadVarInt()
    {
        return (int)ReadVar<uint>();
    }

    public uint ReadVarUhInt()
    {
        return ReadVar<uint>();
    }

    public long ReadVarLong()
    {
        return (long)ReadVar<ulong>();
    }

    public ulong ReadVarUhLong()
    {
        return ReadVar<ulong>();
    }

    private T ReadVar<T>()
    {
        if (typeof(T) == typeof(short) || typeof(T) == typeof(int) || typeof(T) == typeof(long))
            throw new Exception("Type must be unsigned");

        var size = 0;
        if (typeof(T) == typeof(ushort)) size = 8 * 2;
        else if (typeof(T) == typeof(uint)) size = 8 * 4;
        else if (typeof(T) == typeof(ulong)) size = 8 * 8;

        var shift = 0;
        ulong result = 0;
        while (shift < size)
        {
            var b = ReadByte();
            result |= (ulong)(b & 127) << shift;
            if ((b & 128) == 0)
                break;
            shift += 7;
        }

        return (T)Convert.ChangeType(result, typeof(T));
    }

    #endregion
}