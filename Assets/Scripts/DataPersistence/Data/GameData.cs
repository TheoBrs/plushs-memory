[System.Serializable]
public class GameData
{
    // 1 -- Define the data you want to save here
    // 2 -- Define the default values for the data
    // 3 -- Use the LoadData and SaveData methods to load and save the data

    // -- LoadData and SaveData

    // public void LoadData(GameData data) {}
    // public void SaveData(GameData data) {}

    // Audio
    public float MusicVolume;
    public float SfxVolume;
    public bool IsMusicMuted;
    public bool IsSfxMuted;

    // Player
    public int Progression;

    // Others
    public int miteKillCount;
    public int coleoptereKillCount;
    public int sourisKillCount;
    public int reineKillCount;

    // The value defined in the constructor will be the default value when the game is started with no saved data
    public GameData()
    {
        // Define the default values here

        // Audio
        MusicVolume = 0.8f;
        SfxVolume = 0.8f;
        IsMusicMuted = false;
        IsSfxMuted = false;

        // Player
        Progression = 0;

        // Others
        miteKillCount = 0;
        coleoptereKillCount = 0;
        sourisKillCount = 0;
        reineKillCount = 0;

    }
}
