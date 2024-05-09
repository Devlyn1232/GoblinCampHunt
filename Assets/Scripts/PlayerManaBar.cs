using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    public void UpdateManaBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
