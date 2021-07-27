using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMovementScript : MonoBehaviour
{
    private Vector3 touch_position;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float move_speed=20f;
    private Vector2 screenBound;
    public GameObject melodyh;

    // Start is called before the first frame update
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        screenBound=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
        Physics2D.IgnoreCollision(melodyh.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.touchCount>0){
            Touch touch=Input.GetTouch(0);
            touch_position=Camera.main.ScreenToWorldPoint(touch.position);
            if(touch_position.x<-7.5f){
                touch_position.z=0;
                //y stay between -3.7 & 3.6
                transform.position=new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.7f,3.6f),transform.position.z);
                direction=(touch_position-transform.position);
                rb.velocity=new Vector2(0,direction.y)*move_speed;            
                if(touch.phase==TouchPhase.Ended){
                    rb.velocity=Vector2.zero;
                    print(rb.position.y);
                    
                }
            }
        }
        if (rb.position.y > 3.6f)
        {
            print("up out");
            transform.position = new Vector3(transform.position.x, 3.6f, transform.position.z);
        }
        else if (rb.position.y < -3.7f)
        {
            print("down out");
            transform.position = new Vector3(transform.position.x, -3.7f, transform.position.z);
        }

    }
}
