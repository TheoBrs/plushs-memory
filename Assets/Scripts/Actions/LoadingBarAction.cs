using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBarAction : MonoBehaviour
{
    [Header("Progress Bar")]
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private Image _loadingBar;

    [Header("Scenes To Load")]
    [SerializeField] private SceneField _neutralZoneScene;
    [SerializeField] private SceneField _battleScene;
    [SerializeField] private SceneField _dialogueScene;
    [SerializeField] private SceneField _testInventoryScene;
    [SerializeField] List<GameObject> enemyPrefabs;

    private void Awake()
    {
        _loadingBarObject.SetActive(false);
    }

    public void StartGame()
    {
        _loadingBarObject.SetActive(true);

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_neutralZoneScene));

        StartCoroutine(ProgressBarLoading());
    }

    public void StartBattle()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        BattleManager.Instance.nextBattlePlacement.ClearBattlePlacement();
        BattleManager.Instance.nextBattlePlacement.AddEnemy(new Coord(0, 0), enemyPrefabs[0], rotation, new Coord(1, 1));

        _loadingBarObject.SetActive(true);
        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_battleScene));
        StartCoroutine(ProgressBarLoading());
    }

    public void StartBossBattle()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        BattleManager.Instance.nextBattlePlacement.ClearBattlePlacement();
        BattleManager.Instance.nextBattlePlacement.AddEnemy(new Coord(1, 0), enemyPrefabs[1], rotation, new Coord(2, 2));

        _loadingBarObject.SetActive(true);
        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_battleScene));
        StartCoroutine(ProgressBarLoading());
    }

    public void StartDialogue()
    {
        _loadingBarObject.SetActive(true);

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_dialogueScene));

        StartCoroutine(ProgressBarLoading());
    }

    public void StartInventory()
    {
        _loadingBarObject.SetActive(true);

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_testInventoryScene));

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
