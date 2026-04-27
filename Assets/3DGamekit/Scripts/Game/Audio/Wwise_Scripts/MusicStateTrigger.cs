using UnityEngine;

public class MusicStateTrigger : MonoBehaviour
{
    public string enterState = "Temple";
    public string exitState = "Exploration";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        AkSoundEngine.SetState("MusicState", enterState);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        AkSoundEngine.SetState("MusicState", exitState);
    }
}