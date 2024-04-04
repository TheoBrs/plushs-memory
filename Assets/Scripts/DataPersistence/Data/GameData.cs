[System.Serializable]
public class GameData
{
    // Define the data you want to save here

    public float MusicVolume;
    public float SfxVolume;

    // The value defined in the constructor will be the default value when the game is started with no saved data
    public GameData()
    {
        // Define the default values here
        SfxVolume = 0.8f;
        MusicVolume = 0.8f;
    }
}
