using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneQuit : MonoBehaviour
{
    public singlemulti sp;
    public singlemulti mp;
    public BackgroundAudio ba;   
    public GameObject canvasToDeactivate;
    public GameObject canvasToActivate;
    public GameObject ConnectMenu;
    public GameObject lobby;
    public GameObject room;
    //public static bool back=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void quit()
    {
        if(sp.pos2.gameObject.activeSelf==true){
            print("sp");
            sp.img.gameObject.transform.DOMove(sp.posTem,0.3f);
            sp.pos2.gameObject.SetActive(false);
            sp.pos1.gameObject.SetActive(true);
            sp.sp.gameObject.SetActive(true);
            sp.mp.gameObject.SetActive(true);
            sp.st.gameObject.SetActive(true);
            GameObject.Find("singlebutton").GetComponent<Animator>().SetBool("pressed",false);
            //back=true;
            //ba.source.Stop(); //prob
        }

        else if(lobby.activeSelf==true){
            ConnectMenu.gameObject.SetActive(true);
            lobby.gameObject.SetActive(false);
        }

        else if(room.activeSelf==true){
            lobby.gameObject.SetActive(true);
            room.gameObject.SetActive(false);
        }

        else if(sp.pos2.gameObject.activeSelf==false&&ConnectMenu.activeSelf==false){
            print("quit");
            Application.Quit();
        }

        else{
            print("mp");
            mp.img.gameObject.transform.DOMove(mp.posTem,0.3f);
            mp.pos2.gameObject.SetActive(false);
            mp.pos1.gameObject.SetActive(true);
            mp.sp.gameObject.SetActive(true);
            mp.mp.gameObject.SetActive(true);
            mp.st.gameObject.SetActive(true);
            ConnectMenu.SetActive(false);
            canvasToDeactivate.SetActive(false);
            canvasToActivate.SetActive(true);
            //back=true;
        } 
        
    }
}
