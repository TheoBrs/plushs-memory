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

    public int miteKillCount;
    public int coleoptereKillCount;
    public bool chapter1Cleared;
    public bool chapter2Cleared;
    public bool chapter3Cleared;

    public void SaveData(GameData data)
    {
        data.miteKillCount = miteKillCount;
        data.coleoptereKillCount = coleoptereKillCount;
        data.chapter1Cleared = chapter1Cleared;
        data.chapter2Cleared = chapter2Cleared;
        data.chapter3Cleared = chapter3Cleared;

    }

    public void LoadData(GameData data)
    {
        miteKillCount = data.miteKillCount;
        coleoptereKillCount = data.coleoptereKillCount;
        chapter1Cleared = data.chapter1Cleared;
        chapter2Cleared = data.chapter2Cleared;
        chapter3Cleared = data.chapter3Cleared;
    }
}
