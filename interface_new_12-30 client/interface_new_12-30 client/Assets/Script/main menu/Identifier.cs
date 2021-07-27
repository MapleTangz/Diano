using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Identifier : MonoBehaviour
{
    public GameObject connectMenu;
    public Canvas sp;
    public static bool mpC_flag=false;
    public static bool mpV_flag=false;
    public static bool sp_flag=false;
    public static int checkID=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(connectMenu.activeInHierarchy){
            //mp_flag=true;
            sp_flag=false;
            /*Debug.Log(checkID);
            if(checkID==1){
                mpC_flag=true;
                mpV_flag=false;
                Debug.Log("mpC");
            }
            else if(checkID==2){
                mpC_flag=false;
                mpV_flag=true;
                Debug.Log("mpV");
            }*/
            /*if(EventSystem.current.currentSelectedGameObject != null){
                checkID= EventSystem.current.currentSelectedGameObject.GetComponent<buttonid>().id;
                Debug.Log(checkID);
                if(checkID==1){
                    mpC_flag=true;
                    mpV_flag=false;
                    Debug.Log("mpC");
                }
                else if(checkID==2){
                    mpC_flag=false;
                    mpV_flag=true;
                    Debug.Log("mpV");
                }
            }*/
        }
        else if(sp.isActiveAndEnabled){
            sp_flag=true;
            mpC_flag=false;
            mpV_flag=false;
            checkID=0;
            Debug.Log("sp");
        }
    }
}
