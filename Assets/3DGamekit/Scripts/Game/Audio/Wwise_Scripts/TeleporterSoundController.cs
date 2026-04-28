using UnityEngine;

public class TeleporterSoundController : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event playLoopEvent;
    public AK.Wwise.Event stopLoopEvent;

    [Header("Wwise RTPC")]
    public AK.Wwise.RTPC proximityRTPC;

    [Header("Settings")]
    public float maxDistance = 12f;
    public Transform player;

    private bool isPlaying = false;
    private bool hasTeleported = false;

    private void Start()
    {
        proximityRTPC.SetGlobalValue(0f);
    }

    private void Update()
    {
        if (hasTeleported || player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        float value = Mathf.Clamp01(1f - distance / maxDistance) * 100f;

        proximityRTPC.SetGlobalValue(value);

        if (value > 0f && !isPlaying)
        {
            playLoopEvent.Post(gameObject);
            isPlaying = true;
        }
    }

    public void StopTeleporterSound()
    {
        if (hasTeleported) return;

        hasTeleported = true;
        proximityRTPC.SetGlobalValue(0f);

        if (stopLoopEvent != null)
            stopLoopEvent.Post(gameObject);
    }
}