using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NeutralZoneUIController : MonoBehaviour
{
    [Header("Gloabl UI")]
    [SerializeField] private GameObject _defaultUI;
    [SerializeField] private GameObject _darkBackground;

    [Header("Panels")]
    [SerializeField] private GameObject _encyclopediaPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _storybookPanel;
    [SerializeField] private GameObject _tutorialPanel;

    [Header("Scene Management")]
    [SerializeField] private SceneField _menuScene;
    [SerializeField] private Slider _loadingBar;

    [Header("Dialogues")]
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private DialogueChannel _dialogueChanel;
    [SerializeField] private Dialogue _dialogue_5;
    [SerializeField] private Dialogue _dialogue_9;
    [SerializeField] private Dialogue _dialogue_14;

    private void Start()
    {
        _defaultUI.SetActive(true);
        _darkBackground.SetActive(false);
        _encyclopediaPanel.SetActive(false);
        _inventoryPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _storybookPanel.SetActive(false);
        _tutorialPanel.SetActive(false);

        if (GameManager.Instance.Progression == 1)
        {
            StartCoroutine(ShowDialogue(_dialogue_5));
        }
        else if (GameManager.Instance.Progression == 3)
        {
            StartCoroutine(ShowDialogue(_dialogue_9));
        }
        else if (GameManager.Instance.Progression == 4)
        {
            StartCoroutine(ShowDialogue(_dialogue_14));
        }
    }

    public IEnumerator ShowDialogue(Dialogue dialogue)
    {
        yield return new WaitForSeconds(1f);

        _dialogueBox.SetActive(true);
        _dialogueChanel.RaiseRequestDialogue(dialogue);
    }

    public void TriggerEncyclopedia()
    {
        _encyclopediaPanel.SetActive(!_encyclopediaPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void TriggerInventory()
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void TriggerSettings()
    {
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void TriggerStorybook()
    {
        _storybookPanel.SetActive(!_storybookPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void TriggerTutorial()
    {
        _tutorialPanel.SetActive(!_tutorialPanel.activeSelf);
        _darkBackground.SetActive(!_darkBackground.activeSelf);
        _defaultUI.SetActive(!_defaultUI.activeSelf);
    }

    public void ReturnToMenu()
    {
        ScenesManager.Instance.ScenesToLoad.Add(SceneManager.LoadSceneAsync(_menuScene));

        StartCoroutine(ScenesManager.Instance.ProgressBarLoading(_loadingBar));
    }
}
