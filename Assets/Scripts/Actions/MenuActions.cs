using UnityEngine;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour, IDataPersistence
{
    [Header("UI Objects")]
    [SerializeField] private GameObject _mainActionsObject;
    [SerializeField] private GameObject _optionActionsObject;

    [Header("Volume Sliders")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

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

    public void LoadData(GameData data)
    {
        _musicSlider.value = data.MusicVolume;
        _sfxSlider.value = data.SfxVolume;
    }

    public void SaveData(GameData data)
    {
        data.MusicVolume = _musicSlider.value;
        data.SfxVolume = _sfxSlider.value;
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

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
