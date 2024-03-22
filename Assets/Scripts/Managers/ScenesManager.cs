using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

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

    public enum Scene
    {
        Menu,
        Dialogue,
    }

    [SerializeField] private SceneField _menuScene;

    private readonly List<AsyncOperation> _scenesToLoad = new();
    public List<AsyncOperation> ScenesToLoad => _scenesToLoad;

    public void ReturnMenu()
    {
        ScenesToLoad.Add(SceneManager.LoadSceneAsync(_menuScene));
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
