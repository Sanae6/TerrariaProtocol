using System;
using System.Drawing;
using System.Net.Sockets;
using TerrariaProto.Packets;

namespace TerrariaProto.Data
{
    public abstract class Player
    {
        public byte PlayerSlot;
        public string PlayerName;
        public Difficulty difficulty;
        public Server.ClientInstance client;
        public PlayerAppearance appearance;
    }
    public class ServerPlayer : Player
    {
        public Server server;
        
        public ServerPlayer(Server server, Server.ClientInstance client)
        {
            this.server = server;
            this.client = client;
        }

        public void SendPacket(IPacket packet)
        {
            server.WriteQueues[client.InstanceId].Add(packet);
        }
    }

    public class ClientPlayer
    {
        public Client client;

        public ClientPlayer()
        {
            
        }
    }
    public enum Difficulty
    {
        SoftCore=0,MediumCore=1,HardCore=2
    }

    public class PlayerAppearance
    {
        public byte skinVariant;
        public bool[] hideVisual;//ten booleans (bitsbyte formatted on the official client when sending)
        public bool hidePet;
        public bool hideLight;
        public byte hairType;//what type of hair the client uses, there are 51 types available
        public Color hairColor;
        public Color skinColor;
        public Color eyeColor;
        public Color shirtColor;
        public Color undershirtColor;
        public Color pantsColor;
        public Color shoeColor;
    }
}