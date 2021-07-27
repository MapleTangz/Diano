using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrop : MonoBehaviour
{
    public static bool bloodTrapflag=false;
    public bool start = false;
    public void DropStart()
    {
        if (start)
        {
            if(bloodTrapflag){
                if(LifeBarScript.health<90.0f){
                    LifeBarScript.health+=10.0f;
                    bloodTrapflag=false;
                }
                else{
                    LifeBarScript.health=100.0f;
                    bloodTrapflag=false;
                }
            }
            else LifeBarScript.health-=0.1f;
        }
    }
}
