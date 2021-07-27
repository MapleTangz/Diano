using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChgVolume : MonoBehaviour
{
    public AudioMixer am;

    public void adjust(float i)
    {
        am.SetFloat("volume",i);
    }
}
