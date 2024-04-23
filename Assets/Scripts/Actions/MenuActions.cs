using UnityEngine;

public class MenuActions : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("MusicTest");
    }

    public void ClickButtonSound()
    {
        AudioManager.Instance.PlaySFX("SFXTest");
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
