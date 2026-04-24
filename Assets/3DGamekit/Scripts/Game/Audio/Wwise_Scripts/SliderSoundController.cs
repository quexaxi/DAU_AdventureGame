using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderSoundController : MonoBehaviour
{
    protected Slider m_Slider;
    public AK.Wwise.RTPC slider_RTPC;

    void Awake()
    {
        m_Slider = GetComponent<Slider>();

        //float value;

        m_Slider.onValueChanged.AddListener(SliderValueChange);
    }

    //private void Update()
    //{
    //    print(m_Slider.value);
    //}


    void SliderValueChange(float value)
    {
        //print(value);
        slider_RTPC.SetGlobalValue(m_Slider.value);
    }
}
