using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private TextMeshProUGUI _healthBarText;
    private int _maxHP;

    private void Awake()
    {
        var go = ToolBox.GetChildWithTag(gameObject.transform, "HealthBarText");
        if (go)
        {
            _healthBarText = go.GetComponent<TextMeshProUGUI>();
            SetMaxHP(_maxHP);
        }
    }

    public void SetMaxHP(int maxHP)
    {
        healthSlider.maxValue = maxHP;
        healthSlider.value = maxHP;
        this._maxHP = maxHP;
        if (_healthBarText)
            _healthBarText.text = maxHP.ToString() + " / " + maxHP.ToString();
    }

    public void SetHP(int HP)
    {
        healthSlider.value = HP;
        if (_healthBarText)
            _healthBarText.text = HP.ToString() + " / " + _maxHP.ToString();
    }
}
