using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_MPSP_Scene : MonoBehaviour
{
    public string sceneSP;
    public string sceneMPCoop;
    public string sceneMPVs;
	private float t;
    // Start is called before the first frame update
    void Start()
    {
       t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.touchCount>0){
            if(Identifier.sp_flag) {
                SceneManager.LoadScene(sceneSP);
                Debug.Log(Identifier.sp_flag);
                Debug.Log("sp");
            }
            
            else if(Identifier.mp_flag) {
                SceneManager.LoadScene(sceneMP);
                Debug.Log(sceneMP);
                Debug.Log(Identifier.mp_flag);
                Debug.Log("mp");
            }
            
        }*/
        
    }

    public void loadSPMPScene()
    {
        if(Identifier.sp_flag) {
            SceneManager.LoadScene(sceneSP);
            Debug.Log(Identifier.sp_flag);
            Debug.Log("sp");
        }
        
        else if(Identifier.mpC_flag) {
            SceneManager.LoadScene(sceneMPCoop);
            Debug.Log(sceneMPCoop);
            Debug.Log(Identifier.mpC_flag);
            Debug.Log("mpC");
        }

        else if(Identifier.mpV_flag) {
            SceneManager.LoadScene(sceneMPVs);
            Debug.Log(sceneMPVs);
            Debug.Log(Identifier.mpV_flag);
            Debug.Log("mpV");
        }

    }
}
