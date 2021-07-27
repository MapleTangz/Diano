using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCount : MonoBehaviour
{
	Text score;
    // Start is called before the first frame update
    void Start()
    {
		score=GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Identifier.mpV_flag){ //vs mode
        score.text="Perfect : "+ScoringVS.perfectcount+"\r\nGood : "+ScoringVS.goodcount+"\r\nBad : "+ScoringVS.badcount+"\r\nMiss : "+ScoringVS.misscount;
      }
      else{ //single player and coop mode
        score.text="Perfect : "+Scoring.perfectcount+"\r\nGood : "+Scoring.goodcount+"\r\nBad : "+Scoring.badcount+"\r\nMiss : "+Scoring.misscount;   
      }
		   
    }
}
