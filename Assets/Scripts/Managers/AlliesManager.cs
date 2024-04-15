using UnityEngine;

public class AlliesManager : MonoBehaviour
{
    #region Singleton
    public static AlliesManager Instance;

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

    public int _actualAlly;

}
