using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class buttonPress : MonoBehaviour
{
    private Animator animator;
    public AudioClip select;
    private AudioSource source;
    public GameObject canvasToDeactivate;
    public GameObject canvasToActivate;
    // Start is called before the first frame update
    void Start()
    {
        source=GetComponent<AudioSource>();
        animator=GetComponent<Animator>();
    }

    public void press()
    {
        source.PlayOneShot(select,1.0F);
        animator.SetBool("pressed",true);
    }   

    public void pressWithoutAnim()
    {
        source.PlayOneShot(select,1.0F);
    }

    public void activateCanvas()
    {
        canvasToDeactivate.SetActive(false);
        canvasToActivate.SetActive(true);
    }

    public void CoopID()
    {
        Identifier.checkID=1;
        Debug.Log("coopid");
    }

    public void VsID()
    {
        Identifier.checkID=2;
        Debug.Log("vsid");
    }

}
