using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("MusicTest");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
