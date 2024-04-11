using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetMaxHP(int maxHP)
    {
        healthSlider.maxValue = maxHP;
        healthSlider.value = maxHP;
    }

    public void SetHP(int maxHP)
    {
        healthSlider.value = maxHP;
    }
}
