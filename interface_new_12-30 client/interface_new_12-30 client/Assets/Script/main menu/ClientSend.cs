using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void welcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void UDPTestReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.udpTestReceived))
        {
            _packet.Write("Received a UDP Packet.");
            SendUDPData(_packet);
        }
    }

    public static void PlayerList()
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestplayerlist))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Requesting Player List.");
            SendUDPData(_packet);
        }
    }

    public static void RoomList()
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestroomlist))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Requesting Player List.");
            SendUDPData(_packet);
        }
    }


    public static void CreateRoom()
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestroom))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Request To Create A Room.");
            SendUDPData(_packet);
        }
    }

    public static void JoinRoom()
    {
        if (UIManager.instance.RoomID.text != null)
        {
            using (Packet _packet = new Packet((int)ClientPackets.requestjoin))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(UIManager.instance.RoomID.text);
                _packet.Write("Request To Join A Room.");
                SendUDPData(_packet);
            }
        }
    }

    public static void InformJoin()
    {
        using (Packet _packet = new Packet((int)ClientPackets.informjoin))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Joined A Room , Inform host.");
            _packet.Write(RoomManager.roommanager.RoomID);
            SendUDPData(_packet);
        }
    }

    public static void InformReady()
    {
        using (Packet _packet = new Packet((int)ClientPackets.informready))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("A player's ready status changed. Inform the other player.");
            _packet.Write(RoomManager.roommanager.RoomID);
            _packet.Write(RoomManager.roommanager.P1Ready);
            SendUDPData(_packet);
        }
    }

    public static void RequestGame()
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestgame))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Request to start game.");
            _packet.Write(RoomManager.roommanager.RoomID);
            SendUDPData(_packet);
        }
    }

    public static void SendDeadSignal()
    {
        using (Packet _packet = new Packet((int)ClientPackets.senddeadsignal))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Is dead.");
            _packet.Write(RoomManager.roommanager.RoomID);
            SendUDPData(_packet);
        }
    }
    
    public static void SendRevivedSignal()
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendrevivedsignal))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Is revived.");
            _packet.Write(RoomManager.roommanager.RoomID);
            SendUDPData(_packet);
        }
    }

    public static void SendSongId(int song_id)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendsongid))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Send song ID.");
            _packet.Write(RoomManager.roommanager.RoomID);
            _packet.Write(song_id);
            SendUDPData(_packet);
        }
    }

    public static void SendModeID(int modeID)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendmode))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Send song ID.");
            _packet.Write(RoomManager.roommanager.RoomID);
            _packet.Write(modeID);
            SendUDPData(_packet);
        }
    }

    public static void VsComboReach(bool flag)
    {
        using (Packet _packet = new Packet((int)ClientPackets.vscomboreach))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Generate trap.");
            _packet.Write(RoomManager.roommanager.RoomID);
            _packet.Write(flag);
            SendUDPData(_packet);
        }
    }

    public static void SendScore(int score)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendscore))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Sending score");
            _packet.Write(RoomManager.roommanager.RoomID);
            _packet.Write(score);
            SendUDPData(_packet);
        }
    }

    public static void ConcludeScore(int score)
    {
        using (Packet _packet = new Packet((int)ClientPackets.countscore))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("Final score sending");
            _packet.Write(RoomManager.roommanager.RoomID);
            _packet.Write(score);
            SendUDPData(_packet);
        }
    }

    #endregion
}
