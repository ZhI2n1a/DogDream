using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoveFx;
    public AudioClip clickFx;

    public void HoverSound()
    {
        myFx.PlayOneShot(hoveFx);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}
