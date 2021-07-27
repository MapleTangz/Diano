using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongButtonMulti : MonoBehaviour
{
    private Animator animator;
    //public string scene;
    //audio
    public AudioClip menu;
    public AudioClip select;
    public bool selected=false;
    private AudioSource source;
    public AudioClip song;

    public buttonController bc;
    public int index;
    private bool clicked=false;

    public GameObject songmenu;
    public GameObject room;
    public Text song_selected;

    // Start is called before the first frame update
    
    /*void Awake()
    {
        obj=GameObject.FindGameObjectWithTag("Menu Audio");
        print(obj);
    }*/
    
    void Start()
    {
        source=GetComponent<AudioSource>();
        animator=GetComponent<Animator>();
        animator.SetBool("choosen",false);
        animator.SetBool("selected",false);
    }

    public void decide()
    {
        if(bc.selectedIndex==index){
            if(animator.GetBool("selected")==false&&clicked==false){
                source.PlayOneShot(select,1.0F);
                animator.SetBool("selected",true); 
                selected=true;
                clicked=true;
            }            
            
            else if(animator.GetBool("selected")==true&&clicked==true){
                source.PlayOneShot(menu,1.0F);
                animator.SetBool("choosen",true); 
				difficulty.id=index;
            }  
        }
        else{
            animator.SetBool("selected",false);
            animator.SetBool("choosen",false);
            clicked=false;
            selected=false;
        }
    }

    /*public void press()
    {
        if(animator.GetBool("selected")==false)
        {
            source.PlayOneShot(select,1.0F);
            animator.SetBool("selected",true); 
            selected=true;
        }
           
        else if(animator.GetBool("selected")==true)  
        {
            source.PlayOneShot(menu,1.0F);
            animator.SetBool("choosen",true); 
            
        } 
           
          
    }*/

    public void loadMulti()
    {
        //SceneManager.LoadScene(scene);
        songmenu.SetActive(false);
        room.SetActive(true);
        if(index==0){
            song_selected.text="Break My Fucking Sky";
            AudioSelect.s_clip = Resources.Load<AudioClip>("audio/Break My Fucking Sky - Eviscerate Soul");
            //send to client
            ClientSend.SendSongId(index);
        }
        else if(index==1){
            song_selected.text="Detective Conan";
            AudioSelect.s_clip = Resources.Load<AudioClip>("audio/Detective Conan");
            //send to client
            ClientSend.SendSongId(index);
        }
        
    }


}
