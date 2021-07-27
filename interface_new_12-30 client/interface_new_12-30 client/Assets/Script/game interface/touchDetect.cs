using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class touchDetect : MonoBehaviour
{
    Vector3 tp;
    Vector3 tp2;
    TouchPhase touchphase=TouchPhase.Began;
    
    // Start is called before the first frame update
    void Start()
    {
	
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == touchphase)
            {
                Touch touch = Input.GetTouch(0);
                tp = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touch_position = new Vector2(tp.x, tp.y);
                RaycastHit2D hitInfo = Physics2D.Raycast(touch_position, Vector2.zero);
                if (hitInfo.collider != null)
                {
                    Transform t = hitInfo.transform;
                    GameObject touchedObject = hitInfo.transform.gameObject;
                    if (touchedObject.tag == "melodyDown" && t.position.y < -2.9f)
                    {
                        if (t.position.y > -3.43f)
                        {
                            Scoring.scoreVal += 50;         //Bad
                            Scoring.badcount++;
                            Scoring.back_to_zero = true;
                            Hit.score.text = "Bad";

                        }
                        else if (t.position.y > -3.84f)
                        {
                            Scoring.scoreVal += 100;        //Good
                            Scoring.goodcount++;
                            Scoring.comboCount++;
                            Scoring.back_to_zero = false;
                            Hit.score.text = "Good";
                        }
                        else if (t.position.y > -4.47f)
                        {
                            Scoring.scoreVal += 200;        //Perfect
                            Scoring.perfectcount++;
                            Scoring.comboCount++;
                            Scoring.back_to_zero = false;
                            Hit.score.text = "Perfect";
                        }
                        else if (t.position.y > -4.9f)
                        {
                            Scoring.scoreVal += 100;        //Good
                            Scoring.goodcount++;
                            Scoring.comboCount++;
                            Scoring.back_to_zero = false;
                            Hit.score.text = "Good";
                        }
                        else
                        {
                            Scoring.scoreVal += 50;         //Bad
                            Scoring.badcount++;
                            Scoring.back_to_zero = true;
                            Hit.score.text = "Bad";
                        }
                        Destroy(touchedObject);
                    }
                    else if (touchedObject.tag == "melodyRight" && t.position.x > 5f)
                    {
                        if (t.position.x < 6.58f)
                        {
                            Scoring.scoreVal += 50;         //Bad
                            Scoring.badcount++;
                            Scoring.back_to_zero = true;
                            Hit.score.text = "Bad";
                        }
                        else if (t.position.x < 7.2f)
                        {
                            Scoring.scoreVal += 100;        //Good
                            Scoring.goodcount++;
                            Scoring.comboCount++;
                            Scoring.back_to_zero = false;
                            Hit.score.text = "Good";
                        }
                        else if (t.position.x < 8.2f)
                        {
                            Scoring.scoreVal += 200;        //Perfect
                            Scoring.perfectcount++;
                            Scoring.comboCount++;
                            Scoring.back_to_zero = false;
                            Hit.score.text = "Perfect";
                        }
                        else if (t.position.x < 8.85f)
                        {
                            Scoring.scoreVal += 100;        //Good
                            Scoring.goodcount++;
                            Scoring.comboCount++;
                            Scoring.back_to_zero = false;
                            Hit.score.text = "Good";
                        }
                        else
                        {
                            Scoring.scoreVal += 50;         //Bad
                            Scoring.badcount++;
                            Scoring.back_to_zero = true;
                            Hit.score.text = "Bad";
                        }
                        Destroy(touchedObject);
                    }
                }
            }
            try
            {
                if (Input.GetTouch(1).phase == touchphase)
                {
                    Touch touch2 = Input.GetTouch(1);
                    tp2 = Camera.main.ScreenToWorldPoint(touch2.position);
                    Vector2 touch_position = new Vector2(tp2.x, tp2.y);
                    RaycastHit2D hitInfo = Physics2D.Raycast(touch_position, Vector2.zero);
                    if (hitInfo.collider != null)
                    {
                        Transform t = hitInfo.transform;
                        GameObject touchedObject = hitInfo.transform.gameObject;
                        if (touchedObject.tag == "melodyDown" && t.position.y < -2.9f)
                        {
                            if (t.position.y > -3.43f)
                            {
                                Scoring.scoreVal += 50;         //Bad
                                Scoring.badcount++;
                                Scoring.back_to_zero = true;
                                Hit.score.text = "Bad";

                            }
                            else if (t.position.y > -3.84f)
                            {
                                Scoring.scoreVal += 100;        //Good
                                Scoring.goodcount++;
                                Scoring.comboCount++;
                                Scoring.back_to_zero = false;
                                Hit.score.text = "Good";
                            }
                            else if (t.position.y > -4.47f)
                            {
                                Scoring.scoreVal += 200;        //Perfect
                                Scoring.perfectcount++;
                                Scoring.comboCount++;
                                Scoring.back_to_zero = false;
                                Hit.score.text = "Perfect";
                            }
                            else if (t.position.y > -4.9f)
                            {
                                Scoring.scoreVal += 100;        //Good
                                Scoring.goodcount++;
                                Scoring.comboCount++;
                                Scoring.back_to_zero = false;
                                Hit.score.text = "Good";
                            }
                            else
                            {
                                Scoring.scoreVal += 50;         //Bad
                                Scoring.badcount++;
                                Scoring.back_to_zero = true;
                                Hit.score.text = "Bad";
                            }
                            Destroy(touchedObject);
                        }
                        else if (touchedObject.tag == "melodyRight" && t.position.x > 5f)
                        {
                            if (t.position.x < 6.58f)
                            {
                                Scoring.scoreVal += 50;         //Bad
                                Scoring.badcount++;
                                Scoring.back_to_zero = true;
                                Hit.score.text = "Bad";
                            }
                            else if (t.position.x < 7.2f)
                            {
                                Scoring.scoreVal += 100;        //Good
                                Scoring.goodcount++;
                                Scoring.comboCount++;
                                Scoring.back_to_zero = false;
                                Hit.score.text = "Good";
                            }
                            else if (t.position.x < 8.2f)
                            {
                                Scoring.scoreVal += 200;        //Perfect
                                Scoring.perfectcount++;
                                Scoring.comboCount++;
                                Scoring.back_to_zero = false;
                                Hit.score.text = "Perfect";
                            }
                            else if (t.position.x < 8.85f)
                            {
                                Scoring.scoreVal += 100;        //Good
                                Scoring.goodcount++;
                                Scoring.comboCount++;
                                Scoring.back_to_zero = false;
                                Hit.score.text = "Good";
                            }
                            else
                            {
                                Scoring.scoreVal += 50;         //Bad
                                Scoring.badcount++;
                                Scoring.back_to_zero = true;
                                Hit.score.text = "Bad";
                            }
                            Destroy(touchedObject);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
    }
}
