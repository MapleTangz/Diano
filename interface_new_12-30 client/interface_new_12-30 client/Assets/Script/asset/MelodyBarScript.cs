using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyBarScript : MonoBehaviour
{
    private Vector3 touch_position;
    private Vector2 screenBound;
    private Rigidbody2D rb;
    public float move_speed=2f;

    // Start is called before the first frame update
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        screenBound=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
    }

    // Update is called once per frame
    private void Update()
    {
        rb.velocity=new Vector2(0f,-move_speed);
        //touch and destroy
        /* if(Input.touchCount>0){
            Touch touch=Input.GetTouch(0);
            Vector3 tp=Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touch_position=new Vector2(tp.x,tp.y);
            if(GetComponent<Collider2D>()==Physics2D.OverlapPoint(touch_position)&&touch_position.y<-2.9f){
                Destroy(gameObject);     
            }
        }*/
        //if player does not touch the melody, destroy melody when it is out of screen
        if(transform.position.y<-screenBound.y*2){
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
}
