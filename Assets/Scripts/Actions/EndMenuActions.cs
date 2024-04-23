using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuActions : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private CombatGrid _grid;

    private void Start()
    {
        _animator.SetTrigger("StartFadeOut");
        //AudioManager.Instance.PlayMusic("MusicTest");
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
