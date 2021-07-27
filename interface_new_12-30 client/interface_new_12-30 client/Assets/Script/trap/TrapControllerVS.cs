using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapControllerVS : MonoBehaviour
{

    public CountDownVS countDownGO;
    public static int which_trap = 0;
    public static bool trap; //need to change to true if receive signal from server
    public Text CountDownText;
    public Image middleBlockImg;
    public Image sideBlockImg1;
    public Image sideBlockImg2;
    public Image sideBlockImg3;
    public static bool OnceTimeFlag;
    public static bool CountDownEnd=false;

    // Start is called before the first frame update
    void Start()
    {
        trap=false;
        OnceTimeFlag=true;
        CountDownText.enabled = false;
        middleBlockImg.enabled = false;
        sideBlockImg1.enabled = false;
        sideBlockImg2.enabled = false;
        sideBlockImg3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //generate random number
        if (trap && which_trap == 0) which_trap = Random.Range(1, 4);
        else if (!trap) which_trap = 0;
        //which_trap = 2;
        if (which_trap == 1)
        {
            if (OnceTimeFlag)
            {
                CountDownText.enabled = true;
                countDownGO.TrapStart();
                Debug.Log("shake trap");
                OnceTimeFlag = false;
            }

        }
        /*else if (which_trap == 2)
        {
            if (OnceTimeFlag)
            {
                CountDownText.enabled = true;
                countDownGO.TrapStart();
                OnceTimeFlag = false;
                Debug.Log("blood trap");
            }
            if (CountDownEnd)
            {
                bloodTrap.DropStart();
            }

        }*/
        else if (which_trap == 2)
        {
            if (OnceTimeFlag)
            {
                CountDownText.enabled = true;
                countDownGO.TrapStart();
                Debug.Log("middle block");
                OnceTimeFlag = false;
            }

        }
        else if (which_trap == 3)
        {
            if (OnceTimeFlag)
            {
                CountDownText.enabled = true;
                countDownGO.TrapStart();
                Debug.Log("side block");
                OnceTimeFlag = false;
            }
        }
    }
}
