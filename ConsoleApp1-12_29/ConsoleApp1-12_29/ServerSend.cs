using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for(int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void UDPTest(int _toClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.udpTest))
            {
                _packet.Write("A test packet for UDP");

                SendUDPData(_toClient, _packet);

            }
        }

        public static void SendPlayerList(int _toClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerlistrequest))
            {
                _packet.Write("Sent Player List");
                for(int i = 1; i<= Server.MaxPlayers; i++)
                {
                    if(Server.clients[i].tcp.socket != null)
                    {
                        _packet.Write(Server.clientname[Server.clients[i]]);
                        Console.WriteLine($"Sending player name:{Server.clientname[Server.clients[i]]}");
                    }
                }
                SendUDPData(_toClient, _packet);
            }
        }

        public static void SendRoomList(int _toClient)
        {
            int roomcount = 0;
            using (Packet _packet = new Packet((int)ServerPackets.roomlistrequest))
            {
                _packet.Write("Sent Room List");
                for (int i = 1; i <= Server.MaxRooms; i++)
                {
                    try
                    {
                        if (Server.rooms[i].GetHostID() != -1)
                        {
                            roomcount++;
                            _packet.Write(Server.rooms[i].id);
                            _packet.Write(Server.rooms[i].GetHostName());
                            Console.WriteLine($"Sending list of room of id {Server.rooms[i].id} with host {Server.rooms[i].GetHostName()}");
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                if(roomcount == 0)
                {
                    _packet.Write("None");
                    Console.WriteLine($"No rooms exists. Sending None message");
                }
                SendUDPData(_toClient, _packet);
            }
        }


        public static void RoomCreate(int _toClient, int _roomId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.roomrequest))
            {
                if (_roomId != 0)
                {
                    _packet.Write("Room Created");
                    _packet.Write(1);
                    _packet.Write(_roomId);
                    _packet.Write(Server.rooms[_roomId].GetHostName());
                    Console.WriteLine($"Room created with id {_roomId} and host {Server.rooms[_roomId].GetHostName()}");
                    SendUDPData(_toClient, _packet);
                }
                else
                {
                    _packet.Write("Room number limit reached");
                    _packet.Write(0);
                    SendUDPData(_toClient, _packet);
                }
            }
        }

        public static void PlayerLeaveRoom(int _toClient, int leftClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerleaveroom))
            {
                _packet.Write("A player has left the room.");
                SendUDPData(_toClient, _packet);
            }
        }

        public static void RoomJoin(int _toClient, bool joinsuccess, int roomid)
        {
            using (Packet _packet = new Packet((int)ServerPackets.joinrequest))
            {
                if(joinsuccess)
                {
                    _packet.Write("Successfully joined the room");
                    _packet.Write(1);
                    _packet.Write(roomid);
                    _packet.Write(Server.rooms[roomid].GetHostName());
                    _packet.Write(Server.rooms[roomid].GetGuestName());
                }
                else
                {
                    _packet.Write("Failed to join room");
                    _packet.Write(0);
                }
                SendUDPData(_toClient, _packet);
            }
        }

        public static void InformJoin(int _toClient, int roomid)
        {
            using (Packet _packet = new Packet((int)ServerPackets.joininform))
            {
                _packet.Write("A client has joined the room.");
                _packet.Write(Server.rooms[roomid].GetGuestName());
                SendUDPData(_toClient, _packet);
            }
        }

        public static void InformReady(int _toClient, bool readystatus)
        {
            using (Packet _packet = new Packet((int)ServerPackets.readyinform))
            {
                _packet.Write("A client's ready status has changed.");
                _packet.Write(readystatus);
                SendUDPData(_toClient, _packet);
            }
        }

        public static void InformStart(int _toClient, bool canstart)
        {
            using (Packet _packet = new Packet((int)ServerPackets.startinform))
            {
                _packet.Write("Sending status for game start");
                _packet.Write(canstart);
                SendUDPData(_toClient, _packet);
            }
        }

        public static void EnterGame(int _toClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.gamerequest))
            {
                _packet.Write("Sending into game");
                SendUDPData(_toClient, _packet);
            }
        }

        public static void MissionStart(int _toClient, bool missionstart)
        {
            using (Packet _packet = new Packet((int)ServerPackets.missioncanstart))
            {
                _packet.Write("Mission Start");
                _packet.Write(missionstart);
                SendUDPData(_toClient, _packet);
            }
        }
        
        public static void PlayerRevived(int _toClient, bool revived)
        {
            using (Packet _packet = new Packet((int)ServerPackets.revivenow))
            {
                _packet.Write("Revived");
                _packet.Write(revived);
                SendUDPData(_toClient, _packet);
            }
        }
        public static void hostSongID(int _toClient, int id)
        {
            using (Packet _packet = new Packet((int)ServerPackets.hostsongid))
            {
                _packet.Write("Song ID received");
                _packet.Write(id);
                SendUDPData(_toClient, _packet);
            }
        }
        
        public static void sendModeToClient(int _toClient, int id)
        {
            using (Packet _packet = new Packet((int)ServerPackets.receivedmodeid))
            {
                _packet.Write("Mode ID received");
                _packet.Write(id);
                SendUDPData(_toClient, _packet);
            }
        }

        public static void GenerateVsTrap(int _toClient, bool flag)
        {
            using (Packet _packet = new Packet((int)ServerPackets.generatevstrap))
            {
                _packet.Write("Need to generate trap now");
                _packet.Write(flag);
                SendUDPData(_toClient, _packet);
            }
        }

        public static void DeliverScore(int _toClient, int score)
        {
            using (Packet _packet = new Packet((int)ServerPackets.scoreresult))
            {
                _packet.Write("Send other player score");
                _packet.Write(score);
                SendUDPData(_toClient, _packet);
            }
        }

        public static void FinishGame(int _toClient1, int _toClient2)
        {
            using (Packet _packet = new Packet((int)ServerPackets.endgame))
            {
                _packet.Write("Both player dead, end game");
                SendUDPData(_toClient1, _packet);
            }
            using (Packet _packet = new Packet((int)ServerPackets.endgame))
            {
                _packet.Write("Both player dead, end game");
                SendUDPData(_toClient2, _packet);
            }
        }

        public static void InformResult(int client1score, int client2score,int _toClient1, int _toClient2, int flag)
        {
            using (Packet _packet = new Packet((int)ServerPackets.deliverresult))
            {
                _packet.Write("Deliver Result");
                _packet.Write(client1score);
                if (flag == 0)
                    _packet.Write(1);
                else if (flag == 1)
                    _packet.Write(0);
                else _packet.Write(2);
                SendUDPData(_toClient2, _packet);
            }
            using (Packet _packet = new Packet((int)ServerPackets.deliverresult))
            {
                _packet.Write("Deliver Result");
                _packet.Write(client2score);
                if (flag == 0)
                    _packet.Write(0);
                else if (flag == 1)
                    _packet.Write(1);
                else _packet.Write(2);
                SendUDPData(_toClient1, _packet);
            }
        }

        #endregion
    }
}
