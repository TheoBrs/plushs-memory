using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuActions : MonoBehaviour
{
    public static string lastBattleChapter;
    [SerializeField] private Animator _animator;
    private CombatGrid _grid;

    private void Start()
    {
        _animator.SetTrigger("StartFadeOut");
    }

    public void ContinueRestartButtonClick()
    {
        AnimationScripts.currentScene = AnimationScripts.Scenes.End;
        AnimationScripts.nextScene = AnimationScripts.Scenes.Battle;
        _animator.SetTrigger("StartFadeIn");
    }

    public void MenuButtonClick()
    {
        AnimationScripts.currentScene = AnimationScripts.Scenes.End;
        AnimationScripts.nextScene = AnimationScripts.Scenes.Menu;
        _animator.SetTrigger("StartFadeIn");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(lastBattleChapter);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("NeutralZone");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
