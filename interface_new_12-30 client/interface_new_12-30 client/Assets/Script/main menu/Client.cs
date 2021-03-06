using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;

    public string ip = "";
    public int port = 26950;
    public int myId = 0;
    public TCP tcp;
    public UDP udp;

    private bool isConnected = false;
    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already existed, removing object..");
            Destroy(this);
        } 
    }

    private void Start()
    {
        tcp = new TCP();
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void ConnectToServer()
    {
        InitializeClientData();

        isConnected = true;
        tcp.Connect();
        udp = new UDP();
    }

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;

        private Packet receivedData; 
        private byte[] receiveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
            
        }

        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);

            if (!socket.Connected)
            {
                return;
            }
            stream = socket.GetStream();
            receivedData = new Packet();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        public void SendData(Packet _packet)
        {
            try
            {
                if(socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch(Exception _ex)
            {
                Debug.Log($"Error sending data to server via TCP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _byteLength = stream.EndRead(_result);
                if (_byteLength <= 0)
                {
                    instance.Disconnect();
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer, _data, _byteLength);

                receivedData.Reset(HandleData(_data));

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch
            {
                Disconnect();
            }
        }

        private bool HandleData(byte[] _data)
        {
            int _PacketLength = 0;
            receivedData.SetBytes(_data);
            if(receivedData.UnreadLength() >= 4)
            {
                _PacketLength = receivedData.ReadInt();
                if(_PacketLength <= 0)
                {
                    return true;
                }
            }

            while( _PacketLength > 0 && _PacketLength <= receivedData.UnreadLength())
            {
                byte[] _packetBytes = receivedData.ReadBytes(_PacketLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        packetHandlers[_packetId](_packet);
                    }
                });

                _PacketLength = 0;
                if (receivedData.UnreadLength() >= 4)
                {
                    _PacketLength = receivedData.ReadInt();
                    if (_PacketLength <= 0)
                    {
                        return true;
                    }
                }
            }

            if(_PacketLength <= 1)
            {
                return true;
            }
            return false;
        }

        private void Disconnect()
        {
            instance.Disconnect();

            stream = null;
            receivedData = null;
            receiveBuffer = null;
            socket = null;
        }

    }

    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
        }

        public void Connect(int _localPort)
        {
            socket = new UdpClient(_localPort);

            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);

            using (Packet _packet = new Packet())
            {
                SendData(_packet);
            }

        }

        public void SendData(Packet _packet)
        {
            try
            {
                _packet.InsertInt(instance.myId);
                if(socket != null)
                {
                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via UDP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                byte[] _data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                if(_data.Length < 4)
                {
                    instance.Disconnect();
                    return;
                }

                HandleData(_data);
            }
            catch
            {
                Disconnect();
            }
        }

        private void HandleData(byte[] _data)
        {
            using (Packet _packet = new Packet(_data))
            {
                int _packetLength = _packet.ReadInt();
                _data = _packet.ReadBytes(_packetLength);
            }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_data))
                {
                    int _packetId = _packet.ReadInt();
                    packetHandlers[_packetId](_packet);
                }
            });
        }

        private void Disconnect()
        {
            instance.Disconnect();

            endPoint = null;
            socket = null;
        }

    }

    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.welcome, ClientHandler.Welcome},
            { (int)ServerPackets.udpTest, ClientHandler.UDPTest},
            { (int)ServerPackets.playerlistrequest, ClientHandler.RequestPlayerList},
            { (int)ServerPackets.roomlistrequest, ClientHandler.RequestRoomList},
            { (int)ServerPackets.roomrequest, ClientHandler.RoomRequest },
            { (int)ServerPackets.joinrequest, ClientHandler.JoinRequest },
            { (int)ServerPackets.playerleaveroom, ClientHandler.PlayerLeaveRoom},
            { (int)ServerPackets.joininform, ClientHandler.JoinInformed },
            { (int)ServerPackets.readyinform, ClientHandler.ReadyInform },
            { (int)ServerPackets.startinform, ClientHandler.StartInform },
            { (int)ServerPackets.gamerequest, ClientHandler.EnterGame },
            { (int)ServerPackets.missioncanstart, ClientHandler.MissioncanStart },
            { (int)ServerPackets.revivenow, ClientHandler.ReviveNow },
            { (int)ServerPackets.hostsongid, ClientHandler.hostSongID },
            { (int)ServerPackets.receivedmodeid, ClientHandler.hostModeID },
            { (int)ServerPackets.generatevstrap, ClientHandler.GenerateVsTrap },
            { (int)ServerPackets.scoreresult, ClientHandler.GetOpponentScore },
            { (int)ServerPackets.deliverresult, ClientHandler.ConcludeResult },
            { (int)ServerPackets.endgame, ClientHandler.EndGame}
        };
        Debug.Log("Initialized packets.");
    }

    private void Disconnect(){
        if (isConnected)
        {
            isConnected = false;
            tcp.socket.Close();
            udp.socket.Close();

            Debug.Log("Disconnected from server.");
        }
    }
}
