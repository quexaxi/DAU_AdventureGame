using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    // Wwise event for player footsteps
    [SerializeField]
    private AK.Wwise.Event wwPlayerFootstepEvent;

    // Game object to play footsteps on
    [SerializeField]
    private GameObject footstepSource;

    public void ww_player_footstep_play()
    {
        wwPlayerFootstepEvent.Post(footstepSource);
    }
}
