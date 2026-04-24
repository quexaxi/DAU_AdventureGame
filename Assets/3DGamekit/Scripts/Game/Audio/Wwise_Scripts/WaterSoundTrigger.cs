using UnityEngine;

public class WaterSoundTrigger : MonoBehaviour
{
    public AK.Wwise.Event waterEvent;
    public AK.Wwise.RTPC waterRTPC;

    private bool isPlaying = false;

    private void Start()
    {
        waterRTPC.SetGlobalValue(0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!isPlaying)
        {
            waterEvent.Post(other.gameObject);
            isPlaying = true;
        }

        waterRTPC.SetGlobalValue(100f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        waterRTPC.SetGlobalValue(100f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        waterRTPC.SetGlobalValue(0f);
        isPlaying = false;
    }
}