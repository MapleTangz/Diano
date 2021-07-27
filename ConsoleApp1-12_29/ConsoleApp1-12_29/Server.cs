using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int MaxRooms { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public static Dictionary<Client, string> clientname = new Dictionary<Client, string>();
        public static Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandler> packethandlers;

        private static TcpListener tcpListener;
        private static UdpClient udpListener;

        public static void Start(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;
            MaxRooms = 50;

            Console.WriteLine("Starting Server..");
            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

            udpListener = new UdpClient(Port);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            Console.WriteLine($"Server Started On {Port}.");

        }
        
        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if(clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
        }

        private static void UDPReceiveCallback(IAsyncResult _result)
        {
            try
            {
                IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] _data = udpListener.EndReceive(_result, ref _clientEndPoint);
                udpListener.BeginReceive(UDPReceiveCallback, null);

                if(_data.Length < 4)
                {
                    return;
                }

                using (Packet _packet = new Packet(_data))
                {
                    int _clientId = _packet.ReadInt();

                    if(_clientId == 0)
                    {
                        return;
                    }

                    if(clients[_clientId].udp.endPoint == null)
                    {
                        clients[_clientId].udp.Connect(_clientEndPoint);
                        return;
                    }

                    if(clients[_clientId].udp.endPoint.ToString() == _clientEndPoint.ToString())
                    {
                        clients[_clientId].udp.HandleData(_packet);
                    }
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error receiving UDP data: {_ex}");
            }
        }

        public static void SendUDPData(IPEndPoint _clientEndPoint, Packet _packet)
        {
            try
            {
                if(_clientEndPoint != null)
                {
                    udpListener.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error sending data to {_clientEndPoint} via UDP: {_ex}");
            }
        }

        public static int AddRooms(int hostid)
        {
            for(int i = 1; i <= MaxRooms; i++)
            {
                if(rooms[i].hostid == -1)
                {
                    rooms[i].hostid = hostid;
                    rooms[i].hostname = clientname[clients[hostid]];
                    return i;
                }
                else if(i == MaxRooms && rooms[i].hostid != -1)
                {
                    return 0;
                }
            }
            return 0;
        }

        public static bool JoinRooms(int guestid, int roomid)
        {
            if (rooms[roomid].hostid != -1 && rooms[roomid].guestid == -1)
            {
                rooms[roomid].guestid = guestid;
                rooms[roomid].guestname = clientname[clients[guestid]];
                return true;
            }
            else return false;
        }

        private static void InitializeServerData()
        {
            for(int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }
            for(int i = 1; i <= MaxRooms; i++)
            {
                rooms.Add(i, new Room(i));
            }
            packethandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)ClientPackets.welcomeReceived, ServerHandler.WelcomeReceived},
                {(int)ClientPackets.udpTestReceived, ServerHandler.UDPTestReceived},
                {(int)ClientPackets.requestplayerlist, ServerHandler.SendPlayerList},
                {(int)ClientPackets.requestroomlist, ServerHandler.SendRoomList},
                {(int)ClientPackets.requestroom, ServerHandler.RoomRequested},
                {(int)ClientPackets.requestjoin, ServerHandler.JoinRequest },
                {(int)ClientPackets.informjoin, ServerHandler.InformJoin },
                {(int)ClientPackets.informready, ServerHandler.ReadyInform },
                {(int)ClientPackets.requestgame, ServerHandler.GameRequest },
                {(int)ClientPackets.senddeadsignal, ServerHandler.DeadSignalReceived},
                {(int)ClientPackets.sendrevivedsignal, ServerHandler.RevivedSignalReceived},
                {(int)ClientPackets.sendsongid, ServerHandler.SongIDReceived},
                {(int)ClientPackets.sendmode, ServerHandler.ModeIDReceived},
                {(int)ClientPackets.vscomboreach, ServerHandler.VsComboReceived},
                {(int)ClientPackets.sendscore,ServerHandler.ScoreReceived },
                {(int)ClientPackets.countscore,ServerHandler.FinalResult }
            };
            Console.WriteLine("Initialized packets.");
        }
    }
}
