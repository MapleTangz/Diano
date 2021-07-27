using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetOpponentScore : MonoBehaviour
{
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Identifier.mpV_flag)
        { //vs mode
            score.text = "Score : " + ScoringVS.opponentscoreVal;
        }
        else
        { //single player and coop mode
            score.text = "Score : " + Scoring.opponentscoreVal;
        }
    }
}
