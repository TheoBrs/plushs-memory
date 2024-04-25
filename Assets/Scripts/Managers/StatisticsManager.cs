using UnityEngine;

public class StatisticsManager : MonoBehaviour, IDataPersistence
{
    #region Singleton
    public static StatisticsManager Instance;

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

    [HideInInspector] public int miteKillCount;
    [HideInInspector] public int coleoptereKillCount;

    public void SaveData(GameData data)
    {
        data.miteKillCount = miteKillCount;
        data.coleoptereKillCount = coleoptereKillCount;

    }

    public void LoadData(GameData data)
    {
        miteKillCount = data.miteKillCount;
        coleoptereKillCount = data.coleoptereKillCount;
    }
}
