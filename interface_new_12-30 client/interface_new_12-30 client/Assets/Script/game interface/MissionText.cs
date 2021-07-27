using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionText : MonoBehaviour
{
    public Text missionText; //combo text in vs mode
    public Text missionText2;

    void Start()
    {
        missionText.enabled = false;
        missionText2.enabled = false;
    }

    public void MissionStart()
    {
        StartCoroutine(startMissionText()); //show start mission text for 2 seconds
    }

    IEnumerator startMissionText () {
        
        missionText.enabled = true;
        if(Identifier.mpV_flag){
            missionText2.text="Next Combo Needed is "+ScoringVS.combo_needed.ToString();
        }
        missionText2.enabled = true;
        yield return new WaitForSeconds(2.0f);
        missionText.enabled = false;
        missionText2.enabled = false;
    }
}
