using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainActionsObject;
    [SerializeField] private GameObject _optionActionsObject;

    private void Awake()
    {
        if (!_mainActionsObject.activeSelf)
        {
            _mainActionsObject.SetActive(true);
        }
        if (_optionActionsObject.activeSelf)
        {
            _optionActionsObject.SetActive(false);
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("MusicTest");
    }

    public void RestartGame()
    {
        ScenesManager.Instance.ExitGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
