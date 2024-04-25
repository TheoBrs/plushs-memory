using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuUIController : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private GameObject _defaultPanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _confirmationCanvas;
    [SerializeField] private GameObject _darkBackground;
    [SerializeField] private GameObject _videoCanvas;
    [SerializeField] private VideoPlayer _videoPlayer;

    [Header("Button")]
    [SerializeField] private TextMeshProUGUI _startButtonText;

    [Header("Scene Management")]
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private SceneField _neutralZone;
    [SerializeField] private SceneField _chapter_1;

    private void Start()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("Neutre");

        // panel
        _defaultPanel.SetActive(true);
        _optionPanel.SetActive(false);
        _darkBackground.SetActive(false);
        _videoCanvas.SetActive(false);
        _confirmationCanvas.SetActive(false);

        _videoPlayer.loopPointReached += OnVideoFinished;

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
            AudioManager.Instance.StopMusic();

            _videoCanvas.SetActive(true);
            _videoPlayer.Play();
        }
        else
        {
            ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_neutralZone));

            StartCoroutine(ScenesManager.Instance.ProgressBarLoading(_loadingBar));
        }
    }

    private void OnVideoFinished(VideoPlayer videoPlayer)
    {
        _videoCanvas.SetActive(false);

        GameManager.Instance.Progression = 1;

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_chapter_1));

        StartCoroutine(ScenesManager.Instance.ProgressBarLoading(_loadingBar));
    }

    public void TriggerConfirmationPanel()
    {
        _defaultPanel.SetActive(!_defaultPanel.activeSelf);
        _confirmationCanvas.SetActive(!_confirmationCanvas.activeSelf);
    }

    public void ResetProgression()
    {
        GameManager.Instance.Progression = 0;

        _startButtonText.text = "Nouvelle partie";
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
