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
    public AK.Wwise.Event chomperHit01Event;
    public AK.Wwise.Event chomperHit02Event;
    public AK.Wwise.Event chomperHit03Event;
    public AK.Wwise.Event chomperHit04Event;
    public AK.Wwise.Event chomperDeathEvent;
    public AK.Wwise.Event chomperBiteEvent;


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

    public void ChomperHit01()
    {
        chomperHit01Event.Post(gameObject);
    }

    public void ChomperHit02()
    {
        chomperHit02Event.Post(gameObject);
    }

    public void ChomperHit03()
    {
        chomperHit03Event.Post(gameObject);
    }

    public void ChomperHit04()
    {
        chomperHit04Event.Post(gameObject);
    }

    public void ChomperDeath()
    {
        chomperDeathEvent.Post(gameObject);
    }

    public void ChomperBite()
    {
        chomperBiteEvent.Post(gameObject);
    }

}
