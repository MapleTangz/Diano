using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    class Client
    {
        public static int dataBufferSize = 4096;
        public int id;
        public TCP tcp;
        public UDP udp;
        public bool inroom;
        public int roomid;

        public Client(int _clientId)
        {
            id = _clientId;
            inroom = false;
            tcp = new TCP(id);
            udp = new UDP(id);
            roomid = -1;
        }

        public class TCP
        {
            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            private Packet receivedData;
            private byte[] receiveBuffer;
            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receivedData = new Packet();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallBack, null);

                ServerSend.Welcome(id, "Welcome to the Server!");
            }

            public void SendData(Packet _packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite
(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }
                }
                catch(Exception _ex)
                {
                    Console.WriteLine($"Error sending data to player {id} via TCP: {_ex}");
                }
            }

            private void ReceiveCallBack(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        Server.clients[id].Disconnect();
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    receivedData.Reset(HandleData(_data));
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallBack, null);
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error receiving TCP data:{_ex}");
                    Server.clients[id].Disconnect();
                }
            }

            private bool HandleData(byte[] _data)
            {
                int _PacketLength = 0;
                receivedData.SetBytes(_data);
                if (receivedData.UnreadLength() >= 4)
                {
                    _PacketLength = receivedData.ReadInt();
                    if (_PacketLength <= 0)
                    {
                        return true;
                    }
                }

                while (_PacketLength > 0 && _PacketLength <= receivedData.UnreadLength())
                {
                    byte[] _packetBytes = receivedData.ReadBytes(_PacketLength);
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet _packet = new Packet(_packetBytes))
                        {
                            int _packetId = _packet.ReadInt();
                            Server.packethandlers[_packetId](id,_packet);
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

                if (_PacketLength <= 1)
                {
                    return true;
                }
                return false;
            }
            
            public void Disconnect()
            {
                socket.Close();
                stream = null;
                receivedData = null;
                receiveBuffer = null;
                socket = null;
            }
        }

        public class UDP
        {
            public IPEndPoint endPoint;

            private int id;

            public UDP(int _id)
            {
                id = _id;
            }

            public void Connect(IPEndPoint _endPoint)
            {
                endPoint = _endPoint;
                ServerSend.UDPTest(id);
            }

            public void SendData(Packet _packet)
            {
                Server.SendUDPData(endPoint, _packet);
            }

            public void HandleData(Packet _packetData)
            {
                int _packetLength = _packetData.ReadInt();
                byte[] _packetBytes = _packetData.ReadBytes(_packetLength);

                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        Server.packethandlers[_packetId](id, _packet);
                    }
                });
            }

            public void Disconnect()
            {
                endPoint = null;
            }

        }

        private void Disconnect()
        {
            Console.WriteLine($"{tcp.socket.Client.RemoteEndPoint} has disconnected.");
            //player = null;
            if (inroom)
            {
                Room currentroom = Server.rooms[roomid];
                if(id == currentroom.hostid)
                {
                    if(currentroom.guestid == -1)
                    {
                        Server.rooms.Remove(roomid);
                    }
                    else
                    {
                        currentroom.hostid = currentroom.guestid;
                        currentroom.hostname = currentroom.guestname;
                        ServerSend.PlayerLeaveRoom(currentroom.guestid, currentroom.hostid);
                    }
                }
                else if(id == currentroom.guestid)
                {
                    ServerSend.PlayerLeaveRoom(currentroom.hostid, currentroom.guestid);
                }
            }
            tcp.Disconnect();
            udp.Disconnect();
            
        }

    }
}
