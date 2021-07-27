using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelect : MonoBehaviour
{
	public static AudioClip s_clip;
	AudioSource audioData;
	// Start is called before the first frame update
	void Awake()
    {
		audioData = GetComponent<AudioSource>();
		audioData.clip = s_clip;
		audioData.Play();
    }

    // Update is called once per frame
    void Update()
    {
		
    }
}
