using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    BattleManager Instance;
    [SerializeField] Animator animator;
    [SerializeField] LoadingBarAction loadingBarAction;

    private void Start()
    {
        Instance = BattleManager.Instance;
        animator.SetTrigger("StartFadeOut");
        //AudioManager.Instance.PlayMusic("MusicTest");
    }

    public void ContinueRestartButtonClick()
    {
        if (IsWin.IsWinBool)
            ContinueGame();
        else
            RestartGame();
    }
    public void RestartGame()
    {
        Instance.nextBattlePlacement = Instance.originalPlacement;
        loadingBarAction.StartBattle();
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
