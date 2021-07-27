using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textBlink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("blink1",0f,1f);
        InvokeRepeating("blink2",0.5f,1f); 
    }

    private void blink1()
    {
        GetComponent<SpriteRenderer>().enabled=true;
    }
    private void blink2()
    {
        GetComponent<SpriteRenderer>().enabled=false;
    }
}
