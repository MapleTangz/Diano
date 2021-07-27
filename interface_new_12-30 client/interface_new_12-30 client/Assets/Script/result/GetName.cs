using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetName : MonoBehaviour
{
    public Text P1;
    public Text P2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        P1.text = "P1 : "+RoomManager.roommanager.getp1Name();
        P2.text = "P2 : " + RoomManager.roommanager.getp2Name();
    }
}
