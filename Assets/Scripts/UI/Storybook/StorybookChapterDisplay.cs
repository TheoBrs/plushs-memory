using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorybookChapterDisplay : MonoBehaviour
{
    [SerializeField] private StorybookChapter _chapter;
    [SerializeField] private Scrollbar _descriptionScrollbar;
    [SerializeField] private bool _checkFirst;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _previewImage;
    [SerializeField] private Sprite _blockedPreviewImage;
    [SerializeField] private Button _startChapterButton;

    [Header("Progress Bar")]
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private Image _loadingBar;

    private void Awake()
    {
        _loadingBarObject.SetActive(false);

        _descriptionScrollbar.value = 1;

        _labelText.text = _chapter.Label;
        _nameText.text = _chapter.Name;
        _descriptionText.text = _chapter.Description;
        _previewImage.sprite = _blockedPreviewImage;
    }

    private void Start()
    {
        _startChapterButton.onClick.AddListener(LoadChapterScene);

        if (_checkFirst)
        {
            SelectChapter();
        }
    }

    private void Update()
    {
        // TODO: Change the condition to check if the chapter is unlocked
        if (true)
        {
            _previewImage.sprite = _chapter.Preview;
        }
    }

    public void SelectChapter()
    {
        _descriptionScrollbar.value = 1;

        _nameText.text = _chapter.Name;
        _descriptionText.text = _chapter.Description;
    }

    public void LoadChapterScene()
    {
        _loadingBarObject.SetActive(true);

        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync("Chapter" + _chapter.Index));

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

        // _loadingBarObject.SetActive(false);
    }
}
