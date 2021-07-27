using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{

    public float max=35;
    public float min=25;
    private float curTime; //current time
    private float timeActivate;

    // Start is called before the first frame update
    void Start()
    {
        randomTime();
        curTime=0;
    }

    void FixedUpdate()
    {
        //count time
        curTime+=Time.deltaTime;
        //Debug.Log(curTime);
        //time to activate trap
        if(curTime>=timeActivate&&LifeBarScript.dead==false){
            TrapController.trap=true;
            TrapController.OnceTimeFlag=true;
            curTime=0;
            randomTime();
        }
    }

    void randomTime()
    {
        //random select time to activate trap
        timeActivate=Random.Range(min,max);
        Debug.LogFormat("trap activate after: {0}",timeActivate);
    }
}
