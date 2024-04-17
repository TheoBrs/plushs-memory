using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour, IDataPersistence
{
    [Header("Settings")]
    [SerializeField] private int _steps = 6;

    [Header("Sliders")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Fill Areas")]
    [SerializeField] private GameObject _musicFillArea;
    [SerializeField] private GameObject _sfxFillArea;

    [Header("Icons")]
    [SerializeField] private GameObject _fillIconPrefab;
    [SerializeField] private Sprite _musicOnIcon;
    [SerializeField] private Sprite _musicOffIcon;
    [SerializeField] private Sprite _sfxOnIcon;
    [SerializeField] private Sprite _sfxOffIcon;

    [Header("Images")]
    [SerializeField] private Image _musicIcon;
    [SerializeField] private Image _sfxIcon;

    private List<GameObject> _sfxIconsList = new();
    private List<GameObject> _musicIconsList = new();

    private void Start()
    {
        for (int i = 0; i < _steps; i++)
        {
            GameObject icon = Instantiate(_fillIconPrefab, _musicFillArea.transform);
            _musicIconsList.Add(icon);
        }

        for (int i = 0; i < _steps; i++)
        {
            GameObject icon = Instantiate(_fillIconPrefab, _sfxFillArea.transform);
            _sfxIconsList.Add(icon);
        }

        SFXVolume();
        MusicVolume();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.normalizedValue);

        EnableIcons(_musicIconsList);

        int startIndex = Mathf.Max(0, _musicIconsList.Count - (_steps - (int)_musicSlider.value));

        for (int i = _musicIconsList.Count - 1; i >= startIndex; i--)
        {
            _musicIconsList[i].SetActive(false);
        }
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.normalizedValue);

        EnableIcons(_sfxIconsList);

        int startIndex = Mathf.Max(0, _sfxIconsList.Count - (_steps - (int)_sfxSlider.value));

        for (int i = _sfxIconsList.Count - 1; i >= startIndex; i--)
        {
            _sfxIconsList[i].SetActive(false);
        }
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void DisableIcons(List<GameObject> icons)
    {
        foreach (GameObject icon in icons)
        {
            icon.SetActive(false);
        }
    }

    public void EnableIcons(List<GameObject> icons)
    {
        foreach (GameObject icon in icons)
        {
            icon.SetActive(true);
        }
    }

    public void LoadData(GameData data)
    {
        _musicSlider.normalizedValue = data.MusicVolume;
        _sfxSlider.normalizedValue = data.SfxVolume;
    }

    public void SaveData(GameData data)
    {
        data.MusicVolume = _musicSlider.normalizedValue;
        data.SfxVolume = _sfxSlider.normalizedValue;
    }
}
