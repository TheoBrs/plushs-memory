using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        Instance = this;

        _loadingBarObject.SetActive(false);
    }

    [Header("Main Menu Objects")]
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private Image _loadingBar;

    [Header("Scene to load")]
    [SerializeField] private SceneField _dialogueScene;
    [SerializeField] private SceneField _menuScene;

    private List<AsyncOperation> _scenesToLoad = new();

    public void StartGame()
    {
        _loadingBarObject.SetActive(true);

        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_dialogueScene));

        StartCoroutine(ProgressBarLoading());
    }

    public void ReturnMenu()
    {
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(_menuScene));
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private IEnumerator ProgressBarLoading()
    {
        float loadProgress = 0;

        for (int i = 0; i < _scenesToLoad.Count; i++)
        {
            while (!_scenesToLoad[i].isDone)
            {
                loadProgress += _scenesToLoad[i].progress;
                _loadingBar.fillAmount = loadProgress / _scenesToLoad.Count;
                yield return null;
            }
        }
    }
}
