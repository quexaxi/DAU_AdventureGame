using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundController : MonoBehaviour
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

    public void PlayHover()
    {
        UI_Hover.Post(gameObject);
    }

    public void PlayClick()
    {
        UI_Click.Post(gameObject);
    }

    public void PlayBack()
    {
        UI_Back.Post(gameObject);
    }

    public void PlayCancel()
    {
        UI_Cancel.Post(gameObject);
    }

    public void PlayESC()
    {
        UI_ESC.Post(gameObject);
    }

    public void PlaySelect()
    {
        UI_Select.Post(gameObject);
    }

    public void PlayStart()
    {
        UI_Start.Post(gameObject);
    }

    public void PlayTextPop()
    {
        UI_Text_Pop.Post(gameObject);
    }

    public void PlayTextPopOut()
    {
        UI_Text_PopOut.Post(gameObject);
    }
}