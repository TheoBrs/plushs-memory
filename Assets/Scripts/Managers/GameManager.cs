using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int miteKillCount;
    public int coleoptereKillCount;

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

    private void Start()
    {
        Application.targetFrameRate = 30;
    }
}
