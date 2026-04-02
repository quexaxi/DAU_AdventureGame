using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class ChomperSoundController : MonoBehaviour
{
    public AK.Wwise.Event tongueSlurpEvent;
    public AK.Wwise.Event growlEvent;
    public AK.Wwise.Event footstepEvent;
    public AK.Wwise.Event spottedEvent;
    public AK.Wwise.Event breathEvent;
    public AK.Wwise.Event smellEvent;
    public AK.Wwise.Event gruntEvent;
    public AK.Wwise.Event itchingEvent;
   
    public void AnimSlurp()
    {
        tongueSlurpEvent.Post(gameObject);
    }

    public void AnimGrowl()
    {
        growlEvent.Post(gameObject);
    }

    public void ChomperRun()
    {
        footstepEvent.Post(gameObject);
    }

    public void AnimSpotted()
    {
        spottedEvent.Post(gameObject);
    }

    public void AnimBreath()
    {
        breathEvent.Post(gameObject);
    }

    public void AnimSmell()
    {
        smellEvent.Post(gameObject);
    }

    public void AnimGrunt()
    {
        gruntEvent.Post(gameObject);
    }

    public void AnimItching()
    {
        itchingEvent.Post(gameObject);
    }
}
