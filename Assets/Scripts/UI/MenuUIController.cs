using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private GameObject _defaultPanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _darkBackground;

    private void Start()
    {
        _defaultPanel.SetActive(true);
        _optionPanel.SetActive(false);
        _darkBackground.SetActive(false);
    }

    public void SwapPanel()
    {
        _defaultPanel.SetActive(!_defaultPanel.activeSelf);
        _optionPanel.SetActive(!_optionPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
    }
}
