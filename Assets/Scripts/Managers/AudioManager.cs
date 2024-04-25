using System;
using UnityEngine;

public class AudioManager : MonoBehaviour, IDataPersistence
{
    #region Singleton

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private float _musicVolume;
    private float _sfxVolume;
    private bool _isMusicMuted;
    private bool _isSfxMuted;

    private void Start()
    {
        musicSource.mute = _isMusicMuted;
        sfxSource.mute = _isSfxMuted;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.Name == name);

        if (s == null)
        {
        }
        else
        {
            musicSource.clip = s.Clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.Name == name);

        if (s == null)
        {
        }
        else
        {
            sfxSource.PlayOneShot(s.Clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;

        _isMusicMuted = !_isMusicMuted;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;

        _isSfxMuted = !_isSfxMuted;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;

        _musicVolume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;

        _sfxVolume = volume;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public bool IsMusicMuted()
    {
        return _isMusicMuted;
    }

    public bool IsSfxMuted()
    {
        return _isSfxMuted;
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }

    public float GetSFXVolume()
    {
        return _sfxVolume;
    }

    public void LoadData(GameData data)
    {
        _musicVolume = data.MusicVolume;
        _sfxVolume = data.SfxVolume;
        _isMusicMuted = data.IsMusicMuted;
        _isSfxMuted = data.IsSfxMuted;
    }

    public void SaveData(GameData data)
    {
        data.MusicVolume = _musicVolume;
        data.SfxVolume = _sfxVolume;
        data.IsMusicMuted = _isMusicMuted;
        data.IsSfxMuted = _isSfxMuted;
    }
}
