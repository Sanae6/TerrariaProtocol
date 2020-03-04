using System;
using System.IO;
using System.Text;
using TerrariaProto.Data;
using TerrariaProto.Misc;

namespace TerrariaProto.Packets
{
    /***
     * For packets that are used in the login process
     */
    [Packet(0x01)]
    public class ConnectRequest : IPacket
    {
        public NetString Version;
        public override void Read(PacketBinaryReader reader)
        {
            Version = reader.ReadString();
        }

        public override void Write(PacketBinaryWriter writer)
        {
            writer.Write(Version);
        }
    }
    [Packet(0x02)]
    public class Disconnect : IPacket
    {
        public NetString Reason;
        public override void Read(PacketBinaryReader reader)
        {
            Reason = reader.ReadString();
        }

        public override void Write(PacketBinaryWriter writer)
        {
            writer.Write(Reason);
        }
    }
    [Packet(0x03)]
    public class ConnectApproval : IPacket
    {
        public byte PlayerSlot;
        public override void Read(PacketBinaryReader reader)
        {
            PlayerSlot = reader.ReadByte();
        }

        public override void Write(PacketBinaryWriter writer)
        {
            writer.Write(PlayerSlot);
        }
    }

    [Packet(0x04)]
    public class PlayerInfo : IPacket
    {
        public int PlayerSlot;
        public NetString PlayerName;
        public PlayerAppearance Appearance = new PlayerAppearance();
        public override void Read(PacketBinaryReader reader)
        {
            PlayerSlot = reader.ReadByte();
            Appearance.skinVariant = reader.ReadByte();
            Appearance.hairType = reader.ReadByte();
            Appearance.hideVisual = new bool[10];
            var hideVisual =reader.ReadUInt16();
            for (int i = 0; i < 10; i++)
            {
                Appearance.hideVisual[i] = (hideVisual & (1 << i)) != 0;
            }
            var hideMisc =reader.ReadByte();
            for (int i = 0; i < 10; i++)
            {
                Appearance.hideVisual[i] = (hideVisual & (1 << i)) != 0;
            }
        }

        public override void Write(PacketBinaryWriter writer)
        {
            writer.Write(PlayerSlot);
        }
    }

    [Packet(0x05)]
    public class InventorySlot : IPacket
    {
        public override void Read(PacketBinaryReader reader)
        {
            
        }

        public override void Write(PacketBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    [Packet(0x44)]
    public class ClientUUID : IPacket
    {
        public NetString UUID;
        public override void Read(PacketBinaryReader reader)
        {
            UUID = reader.ReadString();
        }

        public override void Write(PacketBinaryWriter writer)
        {
            writer.Write(UUID);
        }
    }
}