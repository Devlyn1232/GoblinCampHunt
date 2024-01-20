using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownVisuial : MonoBehaviour
{
    public Slider slider;

    public void Cooldown(float time)
    {
        slider.maxValue = time;
        slider.value = time;
    }
    public void SetCooldown(float time)
    {
        slider.value = time;
    }
}
