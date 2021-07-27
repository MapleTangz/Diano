using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCont : MonoBehaviour
{
    private static AudioCont instance=null;

    public static AudioCont Instance
    {
        get{
            return instance;
        }
    }

    void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(this.gameObject);
        }
        else{
            instance=this;
        }
       // DontDestroyOnLoad(this.gameObject);
    }
}
