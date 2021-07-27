using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
	Text score;
    public bool iscoop;
    // Start is called before the first frame update
    void Start()
    {
		score=GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Identifier.mpV_flag){ //vs mode
        score.text="Score : "+ScoringVS.scoreVal;
      }
      else if(iscoop){ //single player and coop mode
            int scoring = Scoring.scoreVal + Scoring.opponentscoreVal;
            score.text="Total Score : "+scoring;
      }
      else
      {
            score.text = "Score : " + Scoring.scoreVal;
      }

		
    }
}
