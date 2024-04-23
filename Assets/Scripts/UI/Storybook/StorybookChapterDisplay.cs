using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorybookChapterDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] _chapterButtonList;
    [SerializeField] private Transform _chapterButtonGrid;

    [Header("Scrollbars")]
    [SerializeField] private Scrollbar _chapterButtonsScrollbar;
    [SerializeField] private Scrollbar _chapterDescriptionScrollbar;

    [Header("Loading Bar")]
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private Image _loadingBar;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _chapterTitle;
    [SerializeField] private TextMeshProUGUI _chapterDescription;

    private int _currentChapterIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < _chapterButtonList.Count(); i++)
        {
            Instantiate(_chapterButtonList[i], _chapterButtonGrid);
        }
    }

    private void Start()
    {
        _chapterButtonsScrollbar.value = 1;

        foreach (GameObject button in _chapterButtonList)
        {
            StorybookButtons.OnButtonClicked += HandleButtonClick;
        }

        HandleButtonClick(_chapterButtonList[0]);
    }

    public void HandleButtonClick(GameObject clickedButton)
    {
        _currentChapterIndex = clickedButton.GetComponent<StorybookButtons>().GetChapterIndex();
        _chapterTitle.text = clickedButton.GetComponent<StorybookButtons>().GetChapter().Name;
        _chapterDescription.text = clickedButton.GetComponent<StorybookButtons>().GetChapter().Description;
    }

    public void LoadChapterScene()
    {
        _loadingBarObject.SetActive(true);

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync("Chapter" + _currentChapterIndex));

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
