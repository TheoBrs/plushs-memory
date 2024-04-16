using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    Text healthBarText;
    int maxHP;

    private void Awake()
    {
        healthBarText = ToolBox.GetChildWithTag(gameObject.transform, "HealthBarText")?.GetComponent<Text>();
    }

    public void SetMaxHP(int maxHP)
    {
        healthSlider.maxValue = maxHP;
        healthSlider.value = maxHP;
        this.maxHP = maxHP;
        if (healthBarText)
            healthBarText.text = maxHP.ToString() + " / " + maxHP.ToString();
    }

    public void SetHP(int HP)
    {
        healthSlider.value = HP;
        if (healthBarText)
            healthBarText.text = HP.ToString() + " / " + maxHP.ToString();
    }
}
