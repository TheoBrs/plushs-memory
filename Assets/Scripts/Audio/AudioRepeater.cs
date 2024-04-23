using UnityEngine;

public class AudioRepeater : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.StopMusic();
    }

    private void Start()
    {
        PlayMusic("MusicTest");
    }

    public void PlayMusic(string name)
    {
        AudioManager.Instance.PlayMusic(name);
    }

    public void PlaySFX(string name)
    {
        AudioManager.Instance.PlaySFX(name);
    }
}
