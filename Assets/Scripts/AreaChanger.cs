using UnityEngine;
using UnityEngine.UI;

public class AreaChanger : MonoBehaviour
{
    Button applyButton;
    Slider slider;

    public static int sizeFromApplyButton = 10;

    private void OnEnable()
    {
        applyButton = GetComponent<Button>();
        slider = GetComponentInParent<Slider>();
        sizeFromApplyButton = (int)slider.value;
    }
}
