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
    // Start is called before the first frame update
    public void OnFadeInFinishEvent()
    {
        if (currentScene == Scenes.Battle)
        {
            // This is if we finish a wave but not the battle full
            var turnSystem = GameObject.FindWithTag("TurnSystem");
            if (turnSystem != null)
                turnSystem.GetComponent<TurnSystem>().OnFadeInFinish();
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
            // load battle thingy
        }

    }
}
