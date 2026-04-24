using UnityEngine;

public class EeldogSoundController : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event waterSplashEvent;

    // Animation Event: EeldogJump
    public void EeldogJump()
    {
        PlayWaterSplash();
    }

    private void PlayWaterSplash()
    {
        if (waterSplashEvent != null)
        {
            waterSplashEvent.Post(gameObject);
        }
    }
}