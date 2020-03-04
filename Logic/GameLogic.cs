using System;
using System.Net.Sockets;
using TerrariaProto.Packets;

namespace TerrariaProto.Logic
{
    public abstract class ServerGameLogic : GameLogic
    {
        public event EventHandler<PlayerJoinEventArgs> PlayerJoined;

        protected internal void OnPlayerJoined(PlayerJoinEventArgs e)
        {
            PlayerJoined?.Invoke(this, e);
        }
    }

    public abstract class ClientGameLogic : GameLogic
    {
        
    }

    public abstract class GameLogic
    {
        public event EventHandler<PacketEventArgs> PacketReceivedEvent;
        public event EventHandler<PacketEventArgs> PacketSentEvent;

        protected internal void OnPacketReceivedEvent(PacketEventArgs e)
        {
            PacketReceivedEvent?.Invoke(this, e);
        }
        protected internal void OnPacketSentEvent(PacketEventArgs e)
        {
            PacketSentEvent?.Invoke(this, e);
        }
    }

    public class PacketEventArgs : EventArgs
    {
        public IPacket packet;
        public Server.ClientInstance client;

        internal PacketEventArgs(Server.ClientInstance client, IPacket packet)
        {
            this.client = client;
            this.packet = packet;
        }
    }

    public class PlayerJoinEventArgs : EventArgs
    {
        public Server.ClientInstance client;

        internal PlayerJoinEventArgs(Server.ClientInstance client)
        {
            this.client = client;
        }
    }
}