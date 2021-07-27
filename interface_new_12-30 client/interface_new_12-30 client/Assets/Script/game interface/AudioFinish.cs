using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioFinish : MonoBehaviour
{
	AudioSource audio;
	private float t;
	private string scene="Result";
    public Text waitp2;
    // Start is called before the first frame update
    void Start()
    {
		t = 0;
		audio=GetComponent<AudioSource>();
        if (waitp2 != null)waitp2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(!audio.isPlaying)
		{
			t += Time.deltaTime;
			if(t > 5){
				if(Identifier.sp_flag){
					SceneManager.LoadScene(scene);
				}
				else if(Identifier.mpV_flag){ //vs mode
                    ClientSend.ConcludeScore(ScoringVS.scoreVal);
                    waitp2.gameObject.SetActive(true);
                }
				else if(Identifier.mpC_flag){ //coop mode
                    ClientSend.ConcludeScore(Scoring.scoreVal);
                    waitp2.gameObject.SetActive(true);
                }
			}
		}
    }
}
