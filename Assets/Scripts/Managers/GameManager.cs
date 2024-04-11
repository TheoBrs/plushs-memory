using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance;

    #region Singleton

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
    }

    #endregion

    public void LoadData(GameData data)
    {
        Application.targetFrameRate = data.Framerate;
    }

    public void SaveData(GameData data)
    {
        data.Framerate = Application.targetFrameRate;
    }
}
