using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Icons")]
    [SerializeField] private Sprite _musicOnIcon;
    [SerializeField] private Sprite _musicOffIcon;
    [SerializeField] private Sprite _sfxOnIcon;
    [SerializeField] private Sprite _sfxOffIcon;

    [Header("Images")]
    [SerializeField] private Image _musicIcon;
    [SerializeField] private Image _sfxIcon;

    private void Start()
    {
        _musicIcon.sprite = AudioManager.Instance.IsMusicMuted() ? _musicOffIcon : _musicOnIcon;
        _sfxIcon.sprite = AudioManager.Instance.IsSfxMuted() ? _sfxOffIcon : _sfxOnIcon;
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();

        _musicIcon.sprite = AudioManager.Instance.IsMusicMuted() ? _musicOffIcon : _musicOnIcon;
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();

        _sfxIcon.sprite = AudioManager.Instance.IsSfxMuted() ? _sfxOffIcon : _sfxOnIcon;
    }
}
