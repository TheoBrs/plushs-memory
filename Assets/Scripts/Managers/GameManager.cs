using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
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

    public bool IsGamePaused { get; set; }
    public int Progression { get; set; }

    public void Update()
    {
        Debug.Log(Progression);
    }

    public void LoadData(GameData data)
    {
        Progression = data.Progression;
    }

    public void SaveData(GameData data)
    {
        data.Progression = Progression;
    }

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
