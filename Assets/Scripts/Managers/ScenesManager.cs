using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    #region Singleton
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

    #endregion

    private readonly List<AsyncOperation> _scenesToLoad = new();
    public List<AsyncOperation> ScenesToLoad => _scenesToLoad;

    public IEnumerator ProgressBarLoading(Slider loadingBar)
    {
        float loadProgress = 0;

        for (int i = 0; i < ScenesToLoad.Count; i++)
        {
            while (!ScenesToLoad[i].isDone)
            {
                loadProgress += ScenesToLoad[i].progress;
                loadingBar.value = loadProgress / ScenesToLoad.Count;

                yield return null;
            }
        }
    }
}
