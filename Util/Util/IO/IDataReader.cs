using System;
using System.IO;

public interface IDataReader : IDisposable
{
    long BytesAvailable { get; }
    long Position { get; }

    /// <summary>
    ///     Read a Boolean from the Buffer
    /// </summary>
    /// <returns></returns>
    bool ReadBoolean();

    /// <summary>
    ///     Read a byte from the Buffer
    /// </summary>
    /// <returns></returns>
    byte ReadByte();

    /// <summary>
    ///     Returns N bytes from the buffer
    /// </summary>
    /// <param name="n">Number of read bytes.</param>
    /// <returns></returns>
    byte[] ReadBytes(int n);

    /// <summary>
    ///     Read a Char from the Buffer
    /// </summary>
    /// <returns></returns>
    char ReadChar();

    /// <summary>
    ///     Read a Double from the Buffer
    /// </summary>
    /// <returns></returns>
    double ReadDouble();

    /// <summary>
    ///     Read a Float from the Buffer
    /// </summary>
    /// <returns></returns>
    float ReadFloat();

    /// <summary>
    ///     Read a int from the Buffer
    /// </summary>
    /// <returns></returns>
    int ReadInt();

    /// <summary>
    ///     Read a long from the Buffer
    /// </summary>
    /// <returns></returns>
    long ReadLong();

    sbyte ReadSByte();

    /// <summary>
    ///     Read a Short from the Buffer
    /// </summary>
    /// <returns></returns>
    short ReadShort();

    /// <summary>
    ///     Read a int from the Buffer
    /// </summary>
    /// <returns></returns>
    uint ReadUInt();

    /// <summary>
    ///     Read a long from the Buffer
    /// </summary>
    /// <returns></returns>
    ulong ReadULong();


    /// <summary>
    ///     Read a UShort from the Buffer
    /// </summary>
    /// <returns></returns>
    ushort ReadUShort();

    /// <summary>
    ///     Read a string from the Buffer
    /// </summary>
    /// <returns></returns>
    string ReadUTF();

    /// <summary>
    ///     Read a string from the Buffer
    /// </summary>
    /// <returns></returns>
    string ReadUTFBytes(ushort len);

    /// <summary>
    ///     Read a int from the Buffer
    /// </summary>
    /// <returns></returns>
    int ReadVarInt();

    /// <summary>
    ///     Read a double from the Buffer
    /// </summary>
    /// <returns></returns>
    long ReadVarLong();

    /// <summary>
    ///     Read a int from the Buffer
    /// </summary>
    /// <returns></returns>
    short ReadVarShort();

    /// <summary>
    ///     Read a uint from the Buffer
    /// </summary>
    /// <returns></returns>
    uint ReadVarUhInt();

    /// <summary>
    ///     Read a double from the Buffer
    /// </summary>
    /// <returns></returns>
    ulong ReadVarUhLong();

    /// <summary>
    ///     Read a uint from the Buffer
    /// </summary>
    /// <returns></returns>
    ushort ReadVarUhShort();

    void Seek(int offset, SeekOrigin seekOrigin);
    void SkipBytes(int n);
}