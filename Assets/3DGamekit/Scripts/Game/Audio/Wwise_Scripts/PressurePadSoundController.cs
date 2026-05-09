using System.Collections;
using UnityEngine;

public class PressurePadSoundController : MonoBehaviour
{
    public AK.Wwise.Event playSwellEvent;
    public AK.Wwise.Event stopSwellEvent;
    public AK.Wwise.Event pressurePadPressedEvent;

    public float stopSwellDelay = 3f;

    private bool hasPressed = false;

    private void Start()
    {
        if (playSwellEvent != null)
            playSwellEvent.Post(gameObject);
    }

    public void PressurePadPressed()
    {
        if (hasPressed) return;

        hasPressed = true;

        if (pressurePadPressedEvent != null)
            pressurePadPressedEvent.Post(gameObject);

        StartCoroutine(StopSwellAfterDelay());
    }

    private IEnumerator StopSwellAfterDelay()
    {
        yield return new WaitForSeconds(stopSwellDelay);

        if (stopSwellEvent != null)
            stopSwellEvent.Post(gameObject);
    }
}