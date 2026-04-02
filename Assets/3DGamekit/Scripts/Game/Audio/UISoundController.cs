using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundController : MonoBehaviour, IPointerEnterHandler
{
    public AK.Wwise.Event UI_Back;
    public AK.Wwise.Event UI_Cancel;
    public AK.Wwise.Event UI_Click;
    public AK.Wwise.Event UI_ESC;
    public AK.Wwise.Event UI_Hover;
    public AK.Wwise.Event UI_Select;
    public AK.Wwise.Event UI_Start;
    public AK.Wwise.Event UI_Text_Pop;
    public AK.Wwise.Event UI_Text_PopOut;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UI_Hover != null)
            UI_Hover.Post(gameObject);
    }

    public void PlayClick()
    {
        if (UI_Click != null)
            UI_Click.Post(gameObject);
    }

    public void PlayBack()
    {
        if (UI_Back != null)
            UI_Back.Post(gameObject);
    }

    public void PlayCancel()
    {
        if (UI_Cancel != null)
            UI_Cancel.Post(gameObject);
    }

    public void PlayESC()
    {
        if (UI_ESC != null)
            UI_ESC.Post(gameObject);
    }

    public void PlaySelect()
    {
        if (UI_Select != null)
            UI_Select.Post(gameObject);
    }

    public void PlayStart()
    {
        if (UI_Start != null)
            UI_Start.Post(gameObject);
    }

    public void PlayTextPop()
    {
        if (UI_Text_Pop != null)
            UI_Text_Pop.Post(gameObject);
    }

    public void PlayTextPopOut()
    {
        if (UI_Text_PopOut != null)
            UI_Text_PopOut.Post(gameObject);
    }
}