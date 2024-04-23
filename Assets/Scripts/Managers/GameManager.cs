using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 30;
    }

    #endregion

    public bool IsGamePaused { get; private set; }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsGamePaused = false;
    }

    public void TogglePauseGame()
    {
        if (IsGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
