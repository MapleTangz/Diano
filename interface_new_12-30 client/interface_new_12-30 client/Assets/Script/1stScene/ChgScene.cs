using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChgScene : MonoBehaviour
{
    public string scene;
	private float t;
    // Start is called before the first frame update
    void Start()
    {
		t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.touchCount>0){
        t += Time.deltaTime;
        if (t > 5f)
        {
            Debug.Log(Client.instance.myId);
            if (Identifier.sp_flag)
            {
                UIManager.inroom = false;
            }
            else if (Identifier.mpC_flag || Identifier.mpV_flag)
            {
                UIManager.inroom = true;
            }
            SceneManager.LoadScene(scene);
        }
        //}
    }
}
