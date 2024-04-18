using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    BattleManager Instance;
    private void Start()
    {
        Instance = BattleManager.Instance;
        AudioManager.Instance.PlayMusic("MusicTest");
    }

    public void RestartGame()
    {
        Instance.nextBattlePlacement = Instance.originalPlacement;
    }

    public void ContinueGame()
    {
        //something
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
