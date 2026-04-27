using UnityEngine;

public class LifeCrateAudioLogic : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event playSwellEvent;
    public AK.Wwise.Event stopSwellEvent;
    public AK.Wwise.Event openCrateEvent;

    private bool hasOpened = false;

    private void Start()
    {
        if (playSwellEvent != null)
            playSwellEvent.Post(gameObject);
    }

    public void OpenCrate()
    {
        if (hasOpened) return;

        hasOpened = true;

        if (openCrateEvent != null)
            openCrateEvent.Post(gameObject);

        if (stopSwellEvent != null)
            stopSwellEvent.Post(gameObject);
    }
}