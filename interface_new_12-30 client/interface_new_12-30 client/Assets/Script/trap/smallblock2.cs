using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class smallblock2 : MonoBehaviour
{
    public float timeLeft = 3.0f;
    public float timeLeft2 = 6.0f;
    public Text startText;
    public Text startText2;
    public Text trapnum;
    public Image img; //down
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Timer1());
        //img.enabled = false;
    }

    //public void Startmidd

    IEnumerator Timer1()
    {
        yield return new WaitForSeconds(1f);
        do
        {
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            yield return null;
            if (timeLeft < 0)
            {
                do
                {
                    if(trapnum.text == "2") img.enabled = true;
                    startText.text = "Trap Ending in";
                    timeLeft2 -= Time.deltaTime;
                    startText2.text = (timeLeft2).ToString("0");
                    yield return null;
                    startText2.text = "";
                } while (timeLeft2 > 0);
            }
            //img.enabled = false;
        } while (timeLeft > 0);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
