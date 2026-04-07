using UnityEngine;

public class WaterAmbienceTrigger : MonoBehaviour
{
    public AK.Wwise.Event waterEvent;
    public AK.Wwise.RTPC waterRTPC;

    private bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!isPlaying)
        {
            waterEvent.Post(gameObject);
            isPlaying = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        float distance = Vector3.Distance(other.transform.position, transform.position);

        // Distance arttýkça RTPC düþsün
        float value = Mathf.Clamp(100f - distance * 5f, 0f, 100f);

        waterRTPC.SetGlobalValue(value);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        waterRTPC.SetGlobalValue(0f);
        isPlaying = false;
    }
}