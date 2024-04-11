[System.Serializable]
public class GameData
{
    // 1 -- Define the data you want to save here
    // 2 -- Define the default values for the data
    // 3 -- Use the LoadData and SaveData methods to load and save the data

    // -- LoadData and SaveData

    // public void LoadData(GameData data) {}
    // public void SaveData(GameData data) {}

    public float MusicVolume;
    public float SfxVolume;
    public int Framerate;

    // The value defined in the constructor will be the default value when the game is started with no saved data
    public GameData()
    {
        // Define the default values here
        SfxVolume = 0.8f;
        MusicVolume = 0.8f;
        Framerate = 30;
    }
}
