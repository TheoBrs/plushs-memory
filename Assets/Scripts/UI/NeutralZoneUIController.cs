using UnityEngine;

public class NeutralZoneUIController : MonoBehaviour
{
    [Header("Gloabl UI")]
    [SerializeField] private GameObject _defaultUI;
    [SerializeField] private GameObject _darkBackground;

    [Header("Panels")]
    [SerializeField] private GameObject _encyclopediaPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _storybookPanel;
    [SerializeField] private GameObject _tutorialPanel;

    private void Start()
    {
        _defaultUI.SetActive(true);
        _darkBackground.SetActive(false);
        _encyclopediaPanel.SetActive(false);
        _inventoryPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _storybookPanel.SetActive(false);
        _tutorialPanel.SetActive(false);
    }

    public void TriggerEncyclopedia()
    {
        _encyclopediaPanel.SetActive(!_encyclopediaPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void TriggerInventory()
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void TriggerSettings()
    {
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void TriggerStorybook()
    {
        _storybookPanel.SetActive(!_storybookPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void TriggerTutorial()
    {
        _tutorialPanel.SetActive(!_tutorialPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }
}
