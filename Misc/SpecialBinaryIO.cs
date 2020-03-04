using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using TerrariaProto.Data;

namespace TerrariaProto.Misc
{
    public sealed class PacketBinaryReader : BinaryReader
    {
        public int PacketLength { get; }
        public byte PacketId { get; }

        public PacketBinaryReader(int size, Stream input) : base(new BufferedStream(input),Encoding.ASCII,true)
        {
            PacketLength = ReadInt16();
            PacketId = ReadByte();
        }

        public Color ReadColor()
        {
            var colorBytes = ReadBytes(3);
            return Color.FromArgb(colorBytes[0],colorBytes[1],colorBytes[2]);
        }

        public new NetString ReadString()
        {
            return NetString.Read(this);
        }
    }

    public sealed class PacketBinaryWriter : BinaryWriter
    {
        private byte PacketId;

        public PacketBinaryWriter(byte packet) : base(new MemoryStream(256), Encoding.ASCII, true)
        {
            PacketId = packet;
            Seek(3, SeekOrigin.Begin);
        }

        public void AddBeginningBytes()
        {
            MemoryStream stream = (MemoryStream) BaseStream;
            Seek(0, SeekOrigin.Begin);
            Write((short) (stream.Length - 2));
            Write(PacketId);
            stream.Flush();

        }

        public void Write(Color value)
        {
            Write(value.R);
            Write(value.G);
            Write(value.B);
        }

        public void Write(NetString value)
        {
            value.Write(this);
        }
    }
}