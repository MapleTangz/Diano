using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public static RoomManager roommanager;
    public int RoomID;
    public string P1Name;
    public string P1Status;
    public bool P1Ready;
    public string P2Name;
    public string P2Status;
    public bool P2Ready;
    public Button startbutton;
    public Dropdown modeselect;

    private void Awake()
    {
        if (roommanager == null)
        {
            roommanager = this;
        }
        else if (roommanager != this)
        {
            Debug.Log("Instance already existed, removing object..");
            Destroy(this);
        }
    }

    public void ResetDefault()
    {
        roommanager.P1Status = "Not Ready";
        roommanager.P2Status = "Not Ready";
        roommanager.P1Ready = false;
        roommanager.P2Ready = false;
        roommanager.startbutton = GameObject.Find("StartButton").GetComponent<Button>();
        roommanager.startbutton.interactable = false;
        roommanager.modeselect = GameObject.Find("ModeSelect").GetComponent<Dropdown>();
        roommanager.modeselect.value = 0;
    }

    public string getp1Name()
    {
        return roommanager.P1Name;
    }

    public string getp2Name()
    {
        return roommanager.P2Name;
    }

    public void RoomCreate(int roomid, string hostname)
    {
        roommanager.RoomID = roomid;
        roommanager.P1Name = hostname;
        roommanager.P1Status = "Not Ready";
        roommanager.P2Name = "None";
        roommanager.P2Status = "Not Ready";
        roommanager.P1Ready = false;
        roommanager.P2Ready = false;
        roommanager.startbutton.interactable = false;
    }

    public void RoomJoin(string hostname, string guestname, int roomid)
    {
        roommanager.RoomID = roomid;
        roommanager.P1Name = guestname;
        roommanager.P1Status = "Not Ready";
        roommanager.P1Ready = false;
        roommanager.P2Name = hostname;
        roommanager.P2Status = "Not Ready";
        roommanager.P2Ready = false;
        roommanager.startbutton.interactable = false;
    }

    public void InformJoin(string guestname)
    {
        roommanager.P2Name = guestname;
        roommanager.P2Status = "Not Ready";
        roommanager.P2Ready = false;
    }

    public void InformReady(bool ready)
    {
        if (ready)
        {
            roommanager.P2Status = "Ready";
          //  roommanager.P2Status.color = Color.green;
            roommanager.P2Ready = true;
        }
        else
        {
            roommanager.P2Status = "Not Ready";
            //roommanager.P2Status.color = Color.red;
            roommanager.P2Ready = false;
        }
    }

    public void InformStart(bool canstart)
    {
        if (canstart)
        {
            roommanager.startbutton.interactable = true;
        }
        else
        {
            roommanager.startbutton.interactable = false;
        }
    }

    public void SelfReady()
    {
        if (roommanager.P1Ready)
        {
            roommanager.P1Status = "Not Ready";
            //roommanager.P1Status.color = Color.red;
            roommanager.P1Ready = false;
        }
        else
        {
            roommanager.P1Status = "Ready";
           // roommanager.P1Status = Color.green;
            roommanager.P1Ready = true;
        }
        ClientSend.InformReady();
    }

    public void HandlePlayerLeave()
    {
        roommanager.P2Name = "None";
        roommanager.P2Status = "Not Ready";
        roommanager.P2Ready = false;
    }
}
