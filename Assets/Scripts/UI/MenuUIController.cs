using TMPro;
using UnityEngine;

public class MenuUIController : MonoBehaviour, IDataPersistence
{
    [Header("UI Objects")]
    [SerializeField] private GameObject _defaultPanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _darkBackground;

    [Header("Button")]
    [SerializeField] private TextMeshProUGUI _startButtonText;

    private bool _isNewGame;

    private void Start()
    {
        // panel
        _defaultPanel.SetActive(true);
        _optionPanel.SetActive(false);
        _darkBackground.SetActive(false);

        if (_isNewGame)
        {
            _startButtonText.text = "Nouvelle partie";
        }
        else
        {
            _startButtonText.text = "Continuer";
        }
    }

    public void SwapPanel()
    {
        // panel
        _defaultPanel.SetActive(!_defaultPanel.activeSelf);
        _optionPanel.SetActive(!_optionPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
    }

    public void StartedNewGame()
    {
        if (_isNewGame) _isNewGame = false;
    }

    public void LoadData(GameData data)
    {
        _isNewGame = data.IsNewGame;
    }
    public void SaveData(GameData data)
    {
        data.IsNewGame = _isNewGame;
    }
}
