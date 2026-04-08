using UnityEngine;

public class WaterAmbienceTrigger : MonoBehaviour
{
    public AK.Wwise.Event waterEvent;
    public AK.Wwise.RTPC waterRTPC;

    private bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger: " + other.name);

        if (!other.CompareTag("Player")) return;

        Debug.Log("Posting water event");

        if (!isPlaying)
        {
            waterEvent.Post(gameObject);
            waterRTPC.SetGlobalValue(100f);
            isPlaying = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited trigger: " + other.name);

        if (!other.CompareTag("Player")) return;

        waterRTPC.SetGlobalValue(0f);
        isPlaying = false;
    }
}