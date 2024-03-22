using UnityEngine;

public class LevelActions : MonoBehaviour
{
    [SerializeField] private GameObject _UIGameObject;

    void Start()
    {
        if (!_UIGameObject.activeSelf)
        {
            _UIGameObject.SetActive(true);
        }

        AudioManager.Instance.StopMusic();
    }

    public void ReturnMenu()
    {
        ScenesManager.Instance.ReturnMenu();
    }

    public void ClickButtonSound()
    {
        AudioManager.Instance.PlaySFX("SFXTest");
    }
}
