using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private GameObject _defaultPanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _darkBackground;

    [Header("Button")]
    [SerializeField] private TextMeshProUGUI _startButtonText;

    [Header("Scene Management")]
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private SceneField _neutralZone;

    private void Start()
    {
        // panel
        _defaultPanel.SetActive(true);
        _optionPanel.SetActive(false);
        _darkBackground.SetActive(false);

        if (GameManager.Instance.Progression == 0)
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

    public void LoadNeutralZone()
    {
        if (GameManager.Instance.Progression == 0)
        {
            GameManager.Instance.Progression = 1;
        }

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_neutralZone));

        StartCoroutine(ScenesManager.Instance.ProgressBarLoading(_loadingBar));
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
