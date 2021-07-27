using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dropdown : MonoBehaviour
{
    public Dropdown drpd;
    public static int modeFromHost=0;
    void Start()
    {
        drpd.onValueChanged.AddListener(singleORmulti);
    }
    
    void singleORmulti(int index)
    {
        if(index==1){
            Identifier.mpC_flag=true;
            Identifier.mpV_flag=false;
            Debug.Log("mpC");
            //send to client
            ClientSend.SendModeID(index);
        }
        else if(index==2){
            Identifier.mpC_flag=false;
            Identifier.mpV_flag=true;
            Debug.Log("mpV");
            //send to client
            ClientSend.SendModeID(index);
        }
    }

    void Update()
    {
        if(modeFromHost>0){
            drpd.value=modeFromHost;
        }
    }
}
