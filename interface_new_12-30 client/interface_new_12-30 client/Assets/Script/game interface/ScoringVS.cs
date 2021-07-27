using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringVS : MonoBehaviour
{
    public static int scoreVal=0;
	public static int perfectcount=0;
	public static int goodcount=0;
	public static int badcount=0;
	public static int misscount=0;
    public static bool back_to_zero=false;
    public static int comboCount=0;
    public static int opponentscoreVal = 0;
    //public static bool generateTrap=false;
    public static int trapComboCount=0;
    public bool score_flag;
    public bool combo_flag;
    public bool opponent_score;
    public static int combo_needed=5;
    private int successGenerateTrapCount=1;
    public MissionText ShowComboNeeded;
    private float time = 0;
    Text score;
    Text combo;
    public static Text opponentscore;

    // Start is called before the first frame update
    void Start()
    {
        if(score_flag){
            score=GetComponent<Text>();
        }
        else if(combo_flag){
            combo=GetComponent<Text>();
        }
        else if (opponent_score)
        {
            opponentscore = GetComponent<Text>();
        }

    }

    public static void ResetToDefault()
    {
        scoreVal = 0;
        perfectcount = 0;
        goodcount = 0;
        badcount = 0;
        misscount = 0;
        comboCount = 0;
        opponentscoreVal = 0;
    }

    public static void ChangeOpponentScore(int score)
    {
        opponentscoreVal = score;
        opponentscore.text = "P2 : " + opponentscoreVal;
    }

    // Update is called once per frame
    void Update()
    {
        if(score_flag){
            score.text="Score : "+scoreVal;
            time += Time.deltaTime;
            if (time >= 5)
            {
                time = 0;
                ClientSend.SendScore(scoreVal);
            }
        }
        else if(combo_flag){
            if(back_to_zero){
                combo.text="Combo : 0";
                comboCount=0;
                trapComboCount=0;
            }
            else{
                combo.text="Combo : "+comboCount;
                if(trapComboCount>=combo_needed){
                    successGenerateTrapCount++;
                    Debug.Log("generate trap on opponent side");
                    trapComboCount=0;
                    //opponentTrap=true; 
                    //send signal to server to generate trap at opponent side
                    ClientSend.VsComboReach(true);
                    combo_needed*=successGenerateTrapCount;
                    ShowComboNeeded.MissionStart();
                }
                
            }
            
        }
        
    }
}
