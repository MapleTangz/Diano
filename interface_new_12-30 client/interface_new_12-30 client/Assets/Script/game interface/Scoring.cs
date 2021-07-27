using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static int scoreVal=0;
	public static int perfectcount=0;
	public static int goodcount=0;
	public static int badcount=0;
	public static int misscount=0;
    public static bool back_to_zero=false;
    public static int comboCount=0;
    public static int opponentscoreVal = 0;
    public static int missionComboCount=0; //reset to 0 when mission signal come in
    public bool score_flag;
    public bool combo_flag;
    public bool opponent_score;
    public bool ismulti;
    public static bool mission=false; //set to true in LifeBarScript (if receive mission signal)
    private int combo_needed=5;
    public static bool OnceTimeFlag;
    public MissionText ShowMission;
    public MissionText EndMission;
    private float time=0;
    //public bool exampleMission;
    //public bool exampleOnceTimeFlag;
    Text score;
    Text combo;
    public static Text opponentscore;
    
    // Start is called before the first frame update
    void Start()
    {
        OnceTimeFlag=true;
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
        missionComboCount = 0;
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
            if (ismulti)
            {
                time += Time.deltaTime;
                if (time >= 5)
                {
                    time = 0;
                    ClientSend.SendScore(scoreVal);
                }
            }
        }
        else if(combo_flag){
            if(back_to_zero){
                combo.text="Combo : 0";
                comboCount=0;
                if(mission){
                    missionComboCount=0;
                }
                
            }
            else{
                combo.text="Combo : "+comboCount;
                if(mission){
                    if(missionComboCount>=combo_needed){
                        Debug.Log("misson end");
                        mission=false;
                        OnceTimeFlag=true;
                        if(OnceTimeFlag){
                            EndMission.MissionStart(); //show mission end text
                        }                        
                        //send revived signal to server
                        ClientSend.SendRevivedSignal();
                        missionComboCount=0;
                    }
                }

            }
            
        }
        if(mission){
            if(OnceTimeFlag){
                ShowMission.MissionStart();
                OnceTimeFlag=false; 
            }
        }
        
    }
}
