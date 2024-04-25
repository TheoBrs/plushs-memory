using UnityEngine;

public class AnimationScripts : MonoBehaviour
{
    [SerializeField] private EndMenuActions _endMenu;

    public enum Scenes
    {
        Menu,
        Battle,
        End,
        Dialogue
    }

    public static Scenes currentScene;
    public static Scenes nextScene;
    public void OnFadeInFinishEvent()
    {
        if (currentScene == Scenes.Battle)
        {
        }
        if (currentScene == Scenes.End && nextScene == Scenes.Battle)
        {
            if (IsWin.IsWinBool)
                _endMenu.ContinueGame();
            else
                _endMenu.RestartGame();
        }
        if (currentScene == Scenes.End && nextScene == Scenes.Menu)
        {
            _endMenu.MainMenu();
        }
        if (currentScene == Scenes.Menu && nextScene == Scenes.Battle)
        {
        }

    }
}
