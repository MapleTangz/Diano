using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoadingScript : MonoBehaviour
{
    public Text load;
    private int c;
    
    // Start is called before the first frame update
    void Start()
    {
        c=0;
        InvokeRepeating("loading",0.3f,0.3f);
    }

    void Update()
    {
        
    }

    void loading()
    {
        switch (c)
        {
            case 0:
                load.text="Waiting To Revive.";
                c++;
                break;
            case 1:
                load.text="Waiting To Revive..";
                c++;
                break;
            case 2:
                load.text="Waiting To Revive...";
                c++;
                break;
            default:
                load.text="Waiting To Revive....";
                c=0;     
                break;     
        }
    }

    
}
