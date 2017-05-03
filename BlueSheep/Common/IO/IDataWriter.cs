
public interface IDataWriter
{
    byte[] Data { get; }

    void Clear();

    /// <summary>
    ///     Write a Boolean into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteBoolean(bool @bool);

    /// <summary>
    ///     Write a byte into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteByte(byte @byte);

    /// <summary>
    ///     Write bytes array into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteBytes(byte[] data);

    /// <summary>
    ///     Write bytes array into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteBytes(byte[] data, uint offset, uint length);

    /// <summary>
    ///     Write a Char into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteChar(char @char);

    /// <summary>
    ///     Write a Double into the buffer
    /// </summary>
    void WriteDouble(double @double);

    /// <summary>
    ///     Write a Float into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteFloat(float @float);

    /// <summary>
    ///     Write a int into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteInt(int @int);

    /// <summary>
    ///     Write a long into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteLong(long @long);

    void WriteSByte(sbyte @byte);

    /// <summary>
    ///     Write a Short into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteShort(short @short);

    /// <summary>
    ///     Write a Single into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteSingle(float @single);

    /// <summary>
    ///     Write a int into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteUInt(uint @uint);

    /// <summary>
    ///     Write a long into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteULong(ulong @ulong);

    /// <summary>
    ///     Write a UShort into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteUShort(ushort @ushort);

    /// <summary>
    ///     Write a string into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteUTF(string str);

    /// <summary>
    ///     Write a string into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteUTFBytes(string str);

    /// <summary>
    ///     Write a int array into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteVarInt(int @int);

    /// <summary>
    ///     Write a uint array into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteVarInt(uint @uint);

    /// <summary>
    ///     Write a long array into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteVarLong(long @long);

    /// <summary>
    ///     Write a ulong array into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteVarLong(ulong @ulong);

    /// <summary>
    ///     Write a short array into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteVarShort(short @int);

    /// <summary>
    ///     Write a ushort array into the buffer
    /// </summary>
    /// <returns></returns>
    void WriteVarShort(ushort @uint);
}