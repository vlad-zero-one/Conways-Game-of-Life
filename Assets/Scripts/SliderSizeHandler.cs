using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSizeHandler : MonoBehaviour
{
    public Slider slider;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = System.String.Format("{0}", slider.value);
    }
}
