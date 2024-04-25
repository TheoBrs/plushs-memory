using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorybookChapterDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] _chapterButtonList;
    [SerializeField] private Transform _chapterButtonGrid;
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Slider _loadingBar;

    [Header("Scrollbars")]
    [SerializeField] private Scrollbar _chapterButtonsScrollbar;
    [SerializeField] private Scrollbar _chapterDescriptionScrollbar;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _chapterTitle;
    [SerializeField] private TextMeshProUGUI _chapterDescription;
    [SerializeField] private GameObject _loadChapterButton;

    [Header("Dialogues")]
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private DialogueChannel _dialogueChanel;
    [SerializeField] private Dialogue _dialogue_6;
    [SerializeField] private Dialogue _dialogue_10;

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

    private void Update()
    {
        if (_currentChapterIndex <= GameManager.Instance.Progression)
        {
            _loadChapterButton.SetActive(true);
        }
        else
        {
            _loadChapterButton.SetActive(false);
        }
    }

    public void HandleButtonClick(GameObject clickedButton)
    {
        _chapterDescriptionScrollbar.value = 1;

        _currentChapterIndex = clickedButton.GetComponent<StorybookButtons>().GetChapterIndex();
        _chapterTitle.text = clickedButton.GetComponent<StorybookButtons>().GetChapter().Name;
        _chapterDescription.text = clickedButton.GetComponent<StorybookButtons>().GetChapter().Description;
    }

    public void LoadChapterScene()
    {
        if (GameManager.Instance.Progression == 2 && _currentChapterIndex == 1)
        {
            _dialogueBox.SetActive(true);
            _dialogueChanel.OnDialogueEnd += OnDialogueEnd;
            _dialogueChanel.RaiseRequestDialogue(_dialogue_6);
        }
        else if (GameManager.Instance.Progression == 3 && _currentChapterIndex == 3)
        {
            _dialogueBox.SetActive(true);
            _dialogueChanel.OnDialogueEnd += OnDialogueEnd;
            _dialogueChanel.RaiseRequestDialogue(_dialogue_10);
        }
        else
        {
            StartChapterLoading();
        }
    }

    private void StartChapterLoading()
    {
        _loadingPanel.SetActive(true);
        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync("Chapter" + _currentChapterIndex));
        StartCoroutine(ScenesManager.Instance.ProgressBarLoading(_loadingBar));
    }

    private void OnDialogueEnd(Dialogue dialogue)
    {
        _dialogueChanel.OnDialogueEnd -= OnDialogueEnd;

        StartChapterLoading();
    }
}
