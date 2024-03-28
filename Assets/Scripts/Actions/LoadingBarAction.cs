using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBarAction : MonoBehaviour
{
    [Header("Progress Bar")]
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private Image _loadingBar;

    [Header("Scenes To Load")]
    [SerializeField] private SceneField _dialogueScene;
    [SerializeField] private SceneField _battleScene;

    private void Awake()
    {
        _loadingBarObject.SetActive(false);
    }

    public void StartGame()
    {
        _loadingBarObject.SetActive(true);

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_dialogueScene));

        StartCoroutine(ProgressBarLoading());
    }

    public void StartBattle()
    {
        _loadingBarObject.SetActive(true);

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_battleScene));

        StartCoroutine(ProgressBarLoading());
    }

    private IEnumerator ProgressBarLoading()
    {
        float loadProgress = 0;

        for (int i = 0; i < ScenesManager.Instance.ScenesToLoad.Count; i++)
        {
            while (!ScenesManager.Instance.ScenesToLoad[i].isDone)
            {
                loadProgress += ScenesManager.Instance.ScenesToLoad[i].progress;
                _loadingBar.fillAmount = loadProgress / ScenesManager.Instance.ScenesToLoad.Count;
                yield return null;
            }
        }
    }
}
