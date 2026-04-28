using UnityEngine;

public class CrystalSoundController : MonoBehaviour
{
    public AK.Wwise.Event playSwellEvent;
    public AK.Wwise.Event stopSwellEvent;
    public AK.Wwise.Event collectEvent;

    private bool collected = false;

    private void Start()
    {
        if (playSwellEvent != null)
            playSwellEvent.Post(gameObject);
    }

    public void CollectCrystal()
    {
        if (collected) return;

        collected = true;

        if (collectEvent != null)
            collectEvent.Post(gameObject);

        if (stopSwellEvent != null)
            stopSwellEvent.Post(gameObject);
    }
}

