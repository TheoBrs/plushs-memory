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

    private bool _isGamePaused;

    public void PauseGame()
    {
        Time.timeScale = 0;
        _isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isGamePaused = false;
    }

    public void TogglePauseGame()
    {
        if (_isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public bool IsGamePaused => _isGamePaused;
}
