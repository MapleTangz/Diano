using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class singlemulti : MonoBehaviour
{
    public Image img;
    public Button sp,mp,st;
    public Transform pos1;
    public Transform pos2;
    public Vector3 posTem;
    private Animator anim;
    public GameObject ConnectMenu;

    void Start()
    {
        pos2.gameObject.SetActive(false);
        anim=GetComponent<Animator>();


    }

    public void OnClickMenu()
    {
        posTem=pos1.transform.position;
        img.gameObject.transform.DOMove(pos2.transform.position,0.3f);
        pos2.gameObject.SetActive(true);
        pos1.gameObject.SetActive(false);
        sp.gameObject.SetActive(false);
        mp.gameObject.SetActive(false);
        st.gameObject.SetActive(false);
    }

    public void OnClickMenuMulti()
    {
        posTem = pos1.transform.position;
        img.gameObject.transform.DOMove(pos2.transform.position, 0.3f);
        pos2.gameObject.SetActive(false);
        pos1.gameObject.SetActive(false);
        sp.gameObject.SetActive(false);
        mp.gameObject.SetActive(false);
        st.gameObject.SetActive(false);
        ConnectMenu.SetActive(true);
    }

    void Update()
    {
        /*if(SceneQuit.back==true){
            anim.speed=0f;
            anim.Play("Base Layer.default",0,0.0f);
            SceneQuit.back=false;
        }*/
    }
}
