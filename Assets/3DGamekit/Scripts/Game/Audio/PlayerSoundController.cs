using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    // Wwise event for player footsteps
    [SerializeField]
    private AK.Wwise.Event playerFootstepEvent;
    // Game object to play footsteps on
    [SerializeField]
    private GameObject footstepSource;

    // Jump up sound
    [SerializeField]
    private AK.Wwise.Event jumpUpVoiceEvent;

    [SerializeField]
    private GameObject headAudioSource;

    // Footstep switch related
    [SerializeField] private SurfaceProbeWwise surfaceProbe;

    public void ww_player_footstep_play()
    {
        if (surfaceProbe != null && footstepSource != null)
            surfaceProbe.ApplySurfaceTo(footstepSource);

        playerFootstepEvent.Post(footstepSource);
    }

    public void ww_player_jump_up_play()
    {
        jumpUpVoiceEvent.Post(headAudioSource);
    }
}
