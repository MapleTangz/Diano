using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class difficulty : MonoBehaviour
{
	public static int id;
	public static int songdiff;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(id == 0){
			AudioSelect.s_clip = Resources.Load<AudioClip>("audio/Break My Fucking Sky - Eviscerate Soul");
		}
		else if(id==1){
			AudioSelect.s_clip = Resources.Load<AudioClip>("audio/Detective Conan");
		}
		if(EventSystem.current.currentSelectedGameObject != null){
			songdiff= EventSystem.current.currentSelectedGameObject.GetComponent<buttonid>().id;
		}
    }
}
