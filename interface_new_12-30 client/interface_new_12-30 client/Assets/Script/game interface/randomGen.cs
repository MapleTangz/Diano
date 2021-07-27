using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomGen : MonoBehaviour
{
    public GameObject melody;
    public float genTime=1.0f;
    private float max_x=6.5f;
    private float min_x=-6.0f;
    private float max_y=3.6f;
    private float min_y=-3.0f;
    private float fixed_x=-5.1f;
    private float fixed_y=5.1f;
    public string label="vertical";

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(genMelody());
    }

    private void generate()
    {
        //generate melody
        GameObject go=Instantiate(melody) as GameObject;
        //set random position of generated melody
        if(label=="vertical"){
            go.transform.position=new Vector2(Random.Range(min_x,max_x),fixed_y);
            //go.transform.SetParent(GameObject.Find("Canvas").transform,false);
        }
        else if(label=="horizontal"){
            go.transform.position=new Vector2(fixed_x,Random.Range(min_y,max_y));
           //go.transform.SetParent(GameObject.Find("Canvas").transform,false);
        }
        else if(label=="leftBar"){
            go.transform.position=new Vector2(6.0f,Random.Range(min_y,max_y));
           // go.transform.SetParent(GameObject.Find("Canvas").transform,false);
        }
    }

    //set coroutine function
    IEnumerator genMelody()
    {
        while(true){
            yield return new WaitForSeconds(genTime);
            generate();
        }
    }
}
