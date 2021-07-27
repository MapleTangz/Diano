using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class songButton : MonoBehaviour
{
    private Animator animator;
    public string scene;
    //audio
    public AudioClip menu;
    public AudioClip select;
    public bool selected=false;
    private AudioSource source;
    public AudioClip song;

    public buttonController bc;
    public int index;
    private bool clicked=false;


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

    public void load()
    {
        SceneManager.LoadScene(scene);
    }


}
