using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonController : MonoBehaviour
{
    public int selectedIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void press()
    {
        selectedIndex=EventSystem.current.currentSelectedGameObject.GetComponent<songButton>().index;
        
    }
}
