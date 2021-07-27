using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    public Text WinLoseMsg;
    public static int winloseflag;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(winloseflag == 0)
        {
            WinLoseMsg.text = "Win";
            WinLoseMsg.color = Color.yellow;
        }
        else if(winloseflag == 1)
        {
            WinLoseMsg.text = "Lose";
            WinLoseMsg.color = Color.blue;
        }
        else
        {
            WinLoseMsg.text = "Draw";
            WinLoseMsg.color = Color.green;
        }
    }
}
