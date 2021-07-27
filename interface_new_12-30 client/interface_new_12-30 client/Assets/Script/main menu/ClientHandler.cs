using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClientHandler : MonoBehaviour
{

    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myid = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myid;
        ClientSend.welcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void UDPTest(Packet _packet)
    {
        string _msg = _packet.ReadString();

        Debug.Log($"Received packet via UDP. Contains message: {_msg}");
        ClientSend.UDPTestReceived();
    }

    public static void RequestPlayerList(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Received packet via UDP. Contains message: {_msg}");
        var sb = new System.Text.StringBuilder();
        while(true)
        {
            try
            {
                string _name = _packet.ReadString();
                sb.AppendLine(_name);
                Debug.Log($"Player Name: {_name}");
            }
            catch
            {
                break;
            }
        }
        UIManager.instance.playerOnline.text = sb.ToString();
    }

    public static void RequestRoomList(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Received packet via UDP. Contains message: {_msg}");
        var sb = new System.Text.StringBuilder();
        int roomcount = 0;
        while (true)
        {
            try
            {
                int roomid = _packet.ReadInt();
                string _name = _packet.ReadString();
                sb.AppendLine("Room ID :" + roomid.ToString() + " H :" + _name);
                Debug.Log($"Room ID: {roomid.ToString()} with Host: {_name}");
                roomcount++;
            }
            catch
            {
                break;
            }
        }
        if(roomcount == 0)
        {
            try
            {
                string _name = _packet.ReadString();
                sb.AppendLine(_name);
            }
            catch
            {
                ;
            }
        }
        UIManager.instance.RoomExist.text = sb.ToString();
    }


    public static void RoomRequest(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int flag = _packet.ReadInt();
        Debug.Log($"Room Request Status:{_msg}");
        if (flag == 1)
        {
            int roomid = _packet.ReadInt();
            string hostname = _packet.ReadString();
            RoomManager.roommanager.RoomCreate(roomid,hostname);
        }
    }

    public static void PlayerLeaveRoom(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        RoomManager.roommanager.HandlePlayerLeave();
    }

    public static void JoinRequest(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int flag = _packet.ReadInt();
        Debug.Log($"Room Status:{_msg}");
        if(flag == 1)
        {
            int roomid = _packet.ReadInt();
            string host = _packet.ReadString();
            string guest = _packet.ReadString();
            RoomManager.roommanager.RoomJoin(host, guest, roomid);
            ClientSend.InformJoin();
        }
    }

    public static void JoinInformed(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        string guest = _packet.ReadString();
        RoomManager.roommanager.InformJoin(guest);
    }

    public static void ReadyInform(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        bool readystatus = _packet.ReadBool();
        RoomManager.roommanager.InformReady(readystatus);
    }

    public static void StartInform(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        bool readystatus = _packet.ReadBool();
        RoomManager.roommanager.InformStart(readystatus);
    }

    public static void EnterGame(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        UIManager.instance.StartGame();
    }

    public static void MissioncanStart(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        //start mission
        Scoring.mission=true;
    }

    public static void ReviveNow(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        //player revive
        LifeBarScript.revived_static=true;
    }    

    public static void hostSongID(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        int songId = _packet.ReadInt();
        //set multi game interface song
        if(songId==0){
            GameObject song_selected=GameObject.FindGameObjectWithTag("Song Selected");    
            song_selected.GetComponent<Text>().text="Break My Fucking Sky";
            AudioSelect.s_clip = Resources.Load<AudioClip>("audio/Break My Fucking Sky - Eviscerate Soul");
            difficulty.id = 0;
        }
        else if(songId==1){
            GameObject song_selected=GameObject.FindGameObjectWithTag("Song Selected");  
            song_selected.GetComponent<Text>().text="Detective Conan";
            AudioSelect.s_clip = Resources.Load<AudioClip>("audio/Detective Conan");
            difficulty.id = 1;
        }
    }    
    
    public static void hostModeID(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        int modeId = _packet.ReadInt();
        //set multi game interface
        dropdown.modeFromHost=modeId;
    }   

    public static void GenerateVsTrap(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        //generate vs trap
        VSTrapGenerator.GenerateTrapNow=true;
    }

    public static void GetOpponentScore(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        int score = _packet.ReadInt();
        if (Identifier.mpC_flag)
        {
            Scoring.ChangeOpponentScore(score);
        }
        else if (Identifier.mpV_flag)
        {
            ScoringVS.ChangeOpponentScore(score);
        }
    }

    public static void ConcludeResult(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        int score = _packet.ReadInt();
        int flag = _packet.ReadInt();
        if (Identifier.mpC_flag) {
            SceneManager.LoadScene("coopresult");
            Scoring.opponentscoreVal = score;
        }
        else if (Identifier.mpV_flag)
        {
            SceneManager.LoadScene("vsresult");
            ScoringVS.opponentscoreVal = score;
            if (flag == 0) WinLose.winloseflag = 0;
            else if (flag == 1) WinLose.winloseflag = 1;
            else WinLose.winloseflag = 2;
        }
    }

    public static void EndGame(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Room Status:{_msg}");
        SceneManager.LoadScene("coopresult");
    }
}
