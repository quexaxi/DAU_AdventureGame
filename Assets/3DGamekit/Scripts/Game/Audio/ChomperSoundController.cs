using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class ChomperSoundController : MonoBehaviour 
{
    
    public AK.Wwise.Event tongueSlurpEvent;
    public AK.Wwise.Event growlEvent;

    public void AnimTongueSlurp()
    {
        tongueSlurpEvent.Post(gameObject);
    }

    public void AnimGrowl()
    {
        growlEvent.Post(gameObject);
    }
}
