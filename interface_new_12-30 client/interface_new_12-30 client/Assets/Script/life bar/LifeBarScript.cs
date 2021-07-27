using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarScript : MonoBehaviour
{

    public static float health=100.0f;
    public static bool dead=false;
    const float maxHealth=100.0f;
    public GameObject deadCanvas;
    public Image fillImg;
    private Slider slider;
    Color color;
    //public bool revived=false;
    public static bool revived_static=false;
    public static bool EnableInput=true;
    private bool deadSendOnce=true;

    // Start is called before the first frame update
    void Start()
    {
        slider=GetComponent<Slider>();
    }

    public static void ResetLife()
    {
        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value<=slider.minValue){
            fillImg.enabled=false;
        }
        else if(slider.value>slider.minValue&&!fillImg.enabled){
            fillImg.enabled=true;
        }
        if(slider.value>=0.3&&slider.value<0.6){
            color=new Color (255f/255f, 122f/255f, 148f/255f);
            fillImg.color=color;
        }
        else if(slider.value>=0.6){
            color=new Color (255f/255f, 0f/255f, 50f/255f);
            fillImg.color=color;
        }
        else if(slider.value<0.3){
            color=new Color (255f/255f, 197f/255f, 208f/255f);
            fillImg.color=color;
        }

        slider.value=health/maxHealth;
        if(slider.value==0.0f&&deadSendOnce){
            Debug.Log("player dead");
            deadSendOnce=false;
            dead=true;
            // do a canvas to show player is dead and need to wait for revive
            deadCanvas.SetActive(true);
            EnableInput=false;
            //send a mission signal to server
            ClientSend.SendDeadSignal();
        }
        if(revived_static==true){
        //if(revived==true){
            Debug.Log("revived");
            dead=false;
            deadSendOnce=true;
            revived_static=false;
            health=100.0f;
            deadCanvas.SetActive(false);
            EnableInput=true;
        }
    }
}
