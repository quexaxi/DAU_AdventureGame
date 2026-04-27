using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundController : MonoBehaviour
{

    public AK.Wwise.Event playMusicEvent;

    private void Start()
    {
        AkSoundEngine.SetState("MusicState", "Exploration");

        if (playMusicEvent != null)
            playMusicEvent.Post(gameObject);
    }
}