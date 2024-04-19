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
        AnimationScripts.currentScene = AnimationScripts.Scenes.End;
        AnimationScripts.nextScene = AnimationScripts.Scenes.Battle;
        animator.SetTrigger("StartFadeIn");
    }

    public void MenuButtonClick()
    {
        AnimationScripts.currentScene = AnimationScripts.Scenes.End;
        AnimationScripts.nextScene = AnimationScripts.Scenes.Menu;
        animator.SetTrigger("StartFadeIn");
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
