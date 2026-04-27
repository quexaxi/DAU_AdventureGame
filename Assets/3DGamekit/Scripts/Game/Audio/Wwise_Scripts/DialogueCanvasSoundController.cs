using UnityEngine;

public class DialogueCanvasSoundController : MonoBehaviour
{
    public AK.Wwise.Event uiTextPopUpEvent;
    public AK.Wwise.Event uiTextPopOutEvent;

    private GameObject listener;

    private void Awake()
    {
        // Ana listener'ý bul
        if (Camera.main != null)
            listener = Camera.main.gameObject;
        else
            listener = gameObject; // fallback
    }

    public void PlayTextPopUp()
    {
        if (uiTextPopUpEvent != null && listener != null)
            uiTextPopUpEvent.Post(listener);
    }

    public void PlayTextPopOut()
    {
        if (uiTextPopOutEvent != null && listener != null)
            uiTextPopOutEvent.Post(listener);
    }
}