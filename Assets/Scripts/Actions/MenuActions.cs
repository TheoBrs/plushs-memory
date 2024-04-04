using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
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

    public void SwapPanel()
    {
        if (_mainActionsObject.activeSelf && !_optionActionsObject.activeSelf)
        {
            _mainActionsObject.SetActive(false);
            _optionActionsObject.SetActive(true);
        }
        else if (!_mainActionsObject.activeSelf && _optionActionsObject.activeSelf)
        {
            _mainActionsObject.SetActive(true);
            _optionActionsObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Actions Menu Error");
        }
    }

    public void ClickButtonSound()
    {
        AudioManager.Instance.PlaySFX("SFXTest");
    }

    public void ExitGame()
    {
        ScenesManager.Instance.ExitGame();
    }

}
