using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerHandler
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Server.clientname[Server.clients[_fromClient]] = _username;
            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint}(known as {_username}) connected successfully and is now player {_fromClient}.");
            if(_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
        }

        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received packet via UDP. Contains message: {_msg}");
        }

        public static void SendPlayerList(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client: {_msg}");
            ServerSend.SendPlayerList(_fromClient);
        }

        public static void SendRoomList(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client: {_msg}");
            ServerSend.SendRoomList(_fromClient);
        }

        public static void RoomRequested(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            ServerSend.RoomCreate(_fromClient,Server.AddRooms(_fromClient));
        }

        public static void JoinRequest(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            int roomid = Convert.ToInt32(_msg);
            Console.WriteLine($"Request from Client:{_msg}");
            ServerSend.RoomJoin(_fromClient, Server.JoinRooms(_fromClient, roomid), roomid);
        }

        public static void InformJoin(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            ServerSend.InformJoin(Server.rooms[roomid].GetHostID(), roomid);
        }

        public static void ReadyInform(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            bool ready = _packet.ReadBool();
            if (_clientId == Server.rooms[roomid].GetHostID())
            {
                if (ready)
                {
                    Server.rooms[roomid].hostready = true;
                    if (Server.rooms[roomid].GetGuestID() != -1)
                        ServerSend.InformReady(Server.rooms[roomid].GetGuestID(), true);
                }
                else 
                {
                    Server.rooms[roomid].hostready = false;
                    if (Server.rooms[roomid].GetGuestID() != -1)
                        ServerSend.InformReady(Server.rooms[roomid].GetGuestID(), false);
                }
            }
            else if(_clientId == Server.rooms[roomid].GetGuestID())
            {
                if (ready)
                {
                    Server.rooms[roomid].guestready = true;
                    if (Server.rooms[roomid].GetHostID() != -1)
                        ServerSend.InformReady(Server.rooms[roomid].GetHostID(), true);
                }
                else
                {
                    Server.rooms[roomid].guestready = false;
                    if (Server.rooms[roomid].GetHostID() != -1)
                        ServerSend.InformReady(Server.rooms[roomid].GetHostID(), false);
                }
            }
            if(Server.rooms[roomid].hostready == true && Server.rooms[roomid].guestready == true)
            {
                if (!Server.rooms[roomid].readytostart)
                {
                    Server.rooms[roomid].readytostart = true;
                    ServerSend.InformStart(Server.rooms[roomid].GetHostID(), true);
                }
            }
            else
            {
                if (Server.rooms[roomid].readytostart)
                {
                    Server.rooms[roomid].readytostart = false;
                    ServerSend.InformStart(Server.rooms[roomid].GetHostID(), false);
                }
            }
        }

        public static void GameRequest(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            ServerSend.EnterGame(Server.rooms[roomid].GetHostID());
            ServerSend.EnterGame(Server.rooms[roomid].GetGuestID());
        }

        public static void DeadSignalReceived(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            if (_clientId == Server.rooms[roomid].GetHostID()) //send to guest
            {
                Server.rooms[roomid].hostdead = true;
                if (Server.rooms[roomid].GetGuestID() != -1 && !Server.rooms[roomid].guestdead)
                    ServerSend.MissionStart(Server.rooms[roomid].GetGuestID(), true);
                else if(Server.rooms[roomid].guestdead)
                    ServerSend.FinishGame(Server.rooms[roomid].GetHostID(), Server.rooms[roomid].GetGuestID());
            }
            else if(_clientId == Server.rooms[roomid].GetGuestID()) //send to host
            {
                Server.rooms[roomid].guestdead = true;
                if (Server.rooms[roomid].GetHostID() != -1 && !Server.rooms[roomid].hostdead)
                    ServerSend.MissionStart(Server.rooms[roomid].GetHostID(), true);
                else if (Server.rooms[roomid].hostdead)
                    ServerSend.FinishGame(Server.rooms[roomid].GetHostID(), Server.rooms[roomid].GetGuestID());
            }
        }

        public static void RevivedSignalReceived(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            if (_clientId == Server.rooms[roomid].GetHostID()) //send to guest
            {
                if (Server.rooms[roomid].GetGuestID() != -1)
                    ServerSend.PlayerRevived(Server.rooms[roomid].GetGuestID(), true);
            }
            else if(_clientId == Server.rooms[roomid].GetGuestID()) //send to host
            {
                if (Server.rooms[roomid].GetHostID() != -1)
                    ServerSend.PlayerRevived(Server.rooms[roomid].GetHostID(), true);           
            }
        }

        public static void SongIDReceived(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            int songId = _packet.ReadInt();
            if (_clientId == Server.rooms[roomid].GetHostID()) //send to guest
            {
                if (Server.rooms[roomid].GetGuestID() != -1)
                    ServerSend.hostSongID(Server.rooms[roomid].GetGuestID(), songId);
            }
        }

        public static void ModeIDReceived(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            int modeId = _packet.ReadInt();
            if (_clientId == Server.rooms[roomid].GetHostID()) //send to guest
            {
                if (Server.rooms[roomid].GetGuestID() != -1)
                    ServerSend.sendModeToClient(Server.rooms[roomid].GetGuestID(), modeId);
            }
        }

        public static void VsComboReceived(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            if (_clientId == Server.rooms[roomid].GetHostID()) //send to guest
            {
                if (Server.rooms[roomid].GetGuestID() != -1)
                    ServerSend.GenerateVsTrap(Server.rooms[roomid].GetGuestID(), true);
            }
            else if(_clientId == Server.rooms[roomid].GetGuestID()) //send to host
            {
                if (Server.rooms[roomid].GetHostID() != -1)
                    ServerSend.GenerateVsTrap(Server.rooms[roomid].GetHostID(), true);           
            }
        }

        public static void ScoreReceived(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            int score = _packet.ReadInt();
            if (_clientId == Server.rooms[roomid].GetHostID())
            {
                Server.rooms[roomid].hostscore = score;
                ServerSend.DeliverScore(Server.rooms[roomid].GetGuestID(), score);
            }
            else if(_clientId == Server.rooms[roomid].GetGuestID())
            {
                Server.rooms[roomid].guestscore = score;
                ServerSend.DeliverScore(Server.rooms[roomid].GetHostID(), score);
            }
        }

        public static void FinalResult(int _fromClient, Packet _packet)
        {
            int _clientId = _packet.ReadInt();
            string _msg = _packet.ReadString();
            Console.WriteLine($"Request from Client:{_msg}");
            int roomid = _packet.ReadInt();
            int score = _packet.ReadInt();
            int flag = 0;
            if (_clientId == Server.rooms[roomid].GetHostID())
            {
                Server.rooms[roomid].hostscore = score;
                Server.rooms[roomid].hostfinish = true;
                if(Server.rooms[roomid].hostfinish == true && Server.rooms[roomid].guestfinish == true)
                {
                    if (Server.rooms[roomid].hostscore > Server.rooms[roomid].guestscore) flag = 0;
                    else if (Server.rooms[roomid].hostscore < Server.rooms[roomid].guestscore) flag = 1;
                    else flag = 2;
                    ServerSend.InformResult(Server.rooms[roomid].hostscore, Server.rooms[roomid].guestscore,Server.rooms[roomid].GetHostID(), Server.rooms[roomid].GetGuestID(), flag);
                }
            }
            else if (_clientId == Server.rooms[roomid].GetGuestID())
            {
                Server.rooms[roomid].guestscore = score;
                Server.rooms[roomid].guestfinish = true;
                if (Server.rooms[roomid].hostfinish == true && Server.rooms[roomid].guestfinish == true)
                {
                    if (Server.rooms[roomid].hostscore > Server.rooms[roomid].guestscore) flag = 0;
                    else if (Server.rooms[roomid].hostscore < Server.rooms[roomid].guestscore) flag = 1;
                    else flag = 2;
                    ServerSend.InformResult(Server.rooms[roomid].hostscore, Server.rooms[roomid].guestscore,Server.rooms[roomid].GetHostID(), Server.rooms[roomid].GetGuestID(), flag);
                }
            }
        }
    }
}
