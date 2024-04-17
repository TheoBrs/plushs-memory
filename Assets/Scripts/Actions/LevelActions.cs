using UnityEngine;

public class LevelActions : MonoBehaviour
{
    [SerializeField] private GameObject _globalUI;

    void Start()
    {
        AudioManager.Instance.StopMusic();
    }

    public void ReturnMenu()
    {
        ScenesManager.Instance.ReturnMenu();
    }

    public void PlaySFX(string name)
    {
        AudioManager.Instance.PlaySFX(name);
    }

    public void PlayMusic(string name)
    {
        AudioManager.Instance.PlayMusic(name);
    }

    public void TriggerHideUI()
    {
        _globalUI.SetActive(!_globalUI.activeSelf);
    }
}
