﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownVS : MonoBehaviour
{
    public float timeLeft = 3.0f;
    public float timeLeft2 = 6.0f;
    public Text startText;
    public Text startText2;
    public Animator anim;
    public Image img; //middle block
    public Image img1; //sideblock(left)
    public Image img2; //sideblock(down)
    public Image img3; //sideblock(right)

    public void TrapStart()
    {
        img.enabled = false;
        img1.enabled = false;
        img2.enabled = false;
        img3.enabled = false;
        timeLeft = 3.0f;
        timeLeft2 = 6.0f;
        StartCoroutine(Timer1());
    }

    IEnumerator Timer1()
    {
        yield return new WaitForSeconds(1f);
        do
        {
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            yield return null;
            if (timeLeft < 0)
            {
                do
                {
                    if (TrapControllerVS.which_trap == 1) anim.SetTrigger("shake");
                    else if (TrapControllerVS.which_trap == 2) img.enabled = true;
                    else if (TrapControllerVS.which_trap == 3) { img1.enabled = true; img2.enabled = true; img3.enabled = true; }
                    startText.text = "Trap Ending in";
                    timeLeft2 -= Time.deltaTime;
                    startText2.text = (timeLeft2).ToString("0");
                    yield return null;
                    startText.text="";
                    startText2.text = "";
                } while (timeLeft2 > 0);
            }
        } while (timeLeft > 0);
        img.enabled = false;
        img1.enabled = false;
        img2.enabled = false;
        img3.enabled = false;
        TrapControllerVS.trap=false;
        TrapControllerVS.OnceTimeFlag=true;
    }
}
