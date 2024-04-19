using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] Animator animator;
    CombatGrid grid;

    private void Start()
    {
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
        // Tell battlescene to restart somehow
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
