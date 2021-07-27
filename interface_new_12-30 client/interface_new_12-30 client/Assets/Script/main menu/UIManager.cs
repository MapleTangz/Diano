using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject MainMenu;
    public GameObject startMenu;
    public InputField usernameField;
    public InputField userIP;
    public GameObject PlayerList;
    public Text playerOnline;
    public Text RoomExist;
    public GameObject Room;
    public InputField RoomID;
    public static bool inroom;
    public bool checkflag=false;

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

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Client.instance.ip = userIP.text;
        userIP.interactable = false;
        Client.instance.ConnectToServer();
        Debug.Log(Client.instance.ip);
        PlayerList.SetActive(true);
    }

    public void RequestPlayerList()
    {
        ClientSend.PlayerList();
        ClientSend.RoomList();
    }

    public void CreateRoom()
    {
        ClientSend.CreateRoom();
        PlayerList.SetActive(false);
        Room.SetActive(true);
    }

    public void JoinRoom()
    {
        ClientSend.JoinRoom();
        PlayerList.SetActive(false);
        Room.SetActive(true);
    }

    public void RequestGame()
    {
        ClientSend.RequestGame();
    }

    public void StartGame()
    {
        Room.SetActive(false);
        if(Identifier.mpC_flag){
            SceneManager.LoadScene(sceneName: "multiplayerGameInterface");
        }
        else if(Identifier.mpV_flag){
            SceneManager.LoadScene(sceneName: "VsGameInterface");
        }
    }

    public void ReturnToLogin()
    {
        PlayerList.SetActive(false);
        Room.SetActive(false);
    }

    void Update()
    {
        if (!checkflag)
        {
            Scoring.ResetToDefault();
            ScoringVS.ResetToDefault();
            LifeBarScript.ResetLife();
            if (inroom)
            {
                Debug.Log(Client.instance.myId);
                Debug.Log(RoomManager.roommanager.RoomID);
                UIManager.instance.MainMenu.SetActive(false);
                UIManager.instance.startMenu.SetActive(false);
                UIManager.instance.PlayerList.SetActive(false);
                UIManager.instance.Room.SetActive(true);
                //RoomManager.roommanager.ResetDefault();
                
            }
            checkflag = true;
        }
    }
}
