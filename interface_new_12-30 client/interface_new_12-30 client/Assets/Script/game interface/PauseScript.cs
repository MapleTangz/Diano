using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool paused=false;
    public GameObject pauseInterface;


    // Update is called once per frame
    void Update()
    {
        if(paused){
            print("pause");
            pause_game();
        }
        else{
            pauseInterface.SetActive(false);
        }
    }
    
    void pause_game()
    {
        pauseInterface.SetActive(true);
        Time.timeScale=0f;
        print("interface");
    }

    public void resume_game()
    {
        pauseInterface.SetActive(false);
        Time.timeScale=1f;
        paused=false;
    }

    public void quit()
    {
        Application.Quit();
    }

    public void load_menu()
    {
        SceneManager.LoadScene("menu");
        Time.timeScale=1f;
        paused=false;
    }

    public void press_pause()
    {
        paused=true;
        print("press");
    }
}
