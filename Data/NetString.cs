using System;
using System.Text;
using TerrariaProto.Misc;

namespace TerrariaProto.Data
{
    public class NetString
    {
        public string text;
        public Mode mode;

        public NetString(string text, Mode mode)
        {
            this.text = text;
            this.mode = mode;
        }

        public NetString(string text) : this(text, Mode.Literal) {}
        public NetString():this(null){}//for reading

        public void Write(PacketBinaryWriter writer)
        {
            writer.Write((byte) mode);
            writer.Write(text);
        }

        public override string ToString()
        {
            return String.Format("NetString(text=\"{0}\", mode={1})",text,mode);
        }

        public static NetString Read(PacketBinaryReader reader)
        {
            NetString s = new NetString();
            s.mode = Mode.Literal;
            s.text = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadByte()));
            return s;
        }
    }
    public enum Mode
    {
        Literal,
        Formattable,
        LocalizationKey
    }
}