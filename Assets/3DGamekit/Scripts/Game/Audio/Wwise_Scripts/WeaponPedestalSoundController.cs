using UnityEngine;

public class WeaponPedestalSoundController : MonoBehaviour
{
    public AK.Wwise.Event playSwellEvent;
    public AK.Wwise.Event stopSwellEvent;
    public AK.Wwise.Event pickupStaffEvent;

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

      
        if (pickupStaffEvent != null)
            pickupStaffEvent.Post(gameObject);

      
        if (stopSwellEvent != null)
            stopSwellEvent.Post(gameObject);
    }
}