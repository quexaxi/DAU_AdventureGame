using UnityEngine;

public class WeaponPedestalSoundController : MonoBehaviour
{
    public AK.Wwise.Event playSwellEvent;
    public AK.Wwise.Event stopSwellEvent;

    private bool hasStopped = false;

    private void Start()
    {
        if (playSwellEvent != null)
            playSwellEvent.Post(gameObject);
    }

    public void StopSwell()
    {
        if (hasStopped) return;

        hasStopped = true;

        if (stopSwellEvent != null)
            stopSwellEvent.Post(gameObject);
    }
}