using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyBarBar : MonoBehaviour
{
    private Rigidbody2D rb;
    public float move_speed=2f;
    private Vector2 screenBound;

    // Start is called before the first frame update
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        screenBound=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
    }

    // Update is called once per frame
    private void Update()
    {
        rb.velocity=new Vector2(-move_speed,0f);
        //if player does not touch the melody, destroy melody when it is out of screen
        if(transform.position.x<-screenBound.y*2){
			Hit.score.text="Miss";            
            if(Identifier.mpV_flag){
                ScoringVS.misscount++;
                ScoringVS.back_to_zero=true;
            }
            else if(Identifier.mpC_flag){
                Scoring.misscount++;
                Scoring.back_to_zero=true;
                if(!LifeBarScript.dead){
                    LifeBarScript.health-=10.0f;
                }
            }
            else{
                Scoring.misscount++;
                Scoring.back_to_zero=true;
            }
                
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collided)
    {
        if(collided.gameObject.tag=="Bar"){
            Hit.score.text="Perfect";
            if(Identifier.mpC_flag){
                Scoring.scoreVal += 200;
                Scoring.comboCount++;
                Scoring.back_to_zero=false;
                BloodDrop.bloodTrapflag=true;
                if(Scoring.mission){
                    Scoring.missionComboCount++;
                }
            }
            else if(Identifier.mpV_flag){
                ScoringVS.scoreVal += 200;
                ScoringVS.comboCount++;
                ScoringVS.trapComboCount++;
                ScoringVS.back_to_zero=false;
            }
            else{
                Scoring.scoreVal += 200;
                Scoring.comboCount++;
                Scoring.back_to_zero=false;
            }
            
			Destroy(gameObject);
        }
        
        if(collided.gameObject.tag!="Bar"){            
            Physics2D.IgnoreLayerCollision(8,9,true); 
            print("i");
        }
            
    }
}
