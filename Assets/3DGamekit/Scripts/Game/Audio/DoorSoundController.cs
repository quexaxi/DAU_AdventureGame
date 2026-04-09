using UnityEngine;

public class DoorSoundController : MonoBehaviour
{
    public AK.Wwise.Event openEvent;
    public AK.Wwise.Event closeEvent;

    public void PlayDoorOpen()
    {
        if (openEvent != null)
            openEvent.Post(gameObject);
    }

    public void PlayDoorClose()
    {
        if (closeEvent != null)
            closeEvent.Post(gameObject);
    }
}