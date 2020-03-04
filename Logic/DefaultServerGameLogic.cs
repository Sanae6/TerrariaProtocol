using System;
using System.Collections.Generic;
using System.Linq;
using TerrariaProto.Data;
using TerrariaProto.Packets;

namespace TerrariaProto.Logic
{
    public class DefaultServerGameLogic : ServerGameLogic
    {
        public List<ServerPlayer> Players = new List<ServerPlayer>();
        private Server server;
        public DefaultServerGameLogic(Server server)
        {
            this.server = server;
            PacketReceivedEvent += OnPacketReceivedEvent;
            PlayerJoined += OnPlayerJoined;
        }

        private byte GetNewPlayerSlot()
        {
            if (Players.Count == 0) return 0;
            return (byte) Players.FindIndex(p=>Players[Players.IndexOf(p)+1]==null);
        }

        /**
         * Shorthand for the encapsulated selector
         */
        private ServerPlayer GetPlayer(Server.ClientInstance client)
        {
            return Players.First(x => x.client == client);
        }
        
        private void OnPlayerJoined(object sender, PlayerJoinEventArgs e)
        {
            if(Players.Count >256) server.WriteQueues[e.client.InstanceId].Add(new Disconnect {Reason = new NetString("Too many players")});
            var sp = new ServerPlayer(server,e.client);
            sp.PlayerSlot = GetNewPlayerSlot();
            Players.Add(sp);
            Console.WriteLine("Hello!");
        }
        

        private void OnPacketReceivedEvent(object sender, PacketEventArgs e)
        {
            IPacket packet = e.packet;
            Console.WriteLine(packet);
            ServerPlayer player = GetPlayer(e.client);
            switch (packet.GetType().Name)
            {
                case "ConnectRequest":
                    ConnectRequest request = (ConnectRequest) packet;
                    //GetPlayer(e.client).SendPacket(new Disconnect{Reason = new NetString("Test Disconnect Message",Mode.Literal)});
                    player.SendPacket(new ConnectApproval{PlayerSlot = player.PlayerSlot});
                    Console.WriteLine(request.Version.ToString());
                    break;
                case "ClientUUID":
                    ClientUUID uidpa = (ClientUUID) packet;
                    Console.WriteLine(uidpa.UUID.ToString());
                    break;
                case "PlayerInfo":
                    PlayerInfo plif = (PlayerInfo) packet;
                    
                    break;
                case "InventorySlot":
                    
                    break;
            }
        }
    }
}