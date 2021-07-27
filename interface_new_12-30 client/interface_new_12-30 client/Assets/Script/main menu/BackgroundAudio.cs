using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioSource source2;
    GameObject[] objs;
    
    void Update()
    {
        source=GetComponent<AudioSource>();
        if(Identifier.sp_flag){
            objs=GameObject.FindGameObjectsWithTag("Menu Audio");     
            Debug.Log("ok");   
        }
        else if(Identifier.mpC_flag||Identifier.mpV_flag){
            objs=GameObject.FindGameObjectsWithTag("Multi Menu Audio");
        }
        
    }

    void Start()
    {
        
        source.Play();
    }

    public void play_song()
    {
        foreach(GameObject obj in objs){
            if(Identifier.sp_flag&&obj.GetComponent<songButton>().selected==true){
                source.Stop();
                source.clip=obj.GetComponent<songButton>().song;
                source.Play();
                
            }
            else if((Identifier.mpC_flag||Identifier.mpV_flag)&&obj.GetComponent<SongButtonMulti>().selected==true){
                source.Stop();
                source.clip=obj.GetComponent<SongButtonMulti>().song;
                source.Play();
            }
        }
    }  
    //prob
    public void menu_audio()
    {
        source.Stop();
        source=source2;
        source.Play();
    }
}
