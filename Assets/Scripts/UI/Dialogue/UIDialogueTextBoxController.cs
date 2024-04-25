using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueTextBoxController : MonoBehaviour, DialogueNodeVisitor
{
    [SerializeField] private Image _speakerAvatar;
    [SerializeField] private TextMeshProUGUI _speakerText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private RectTransform _choicesBoxTransform;
    [SerializeField] private UIDialogueChoiceController _choiceControllerPrefab;
    [SerializeField] private DialogueChannel _dialogueChannel;

    private bool _listenToInput = false;
    private DialogueNode _nextNode = null;

    private void Awake()
    {
        _dialogueChannel.OnDialogueNodeStart += OnDialogueNodeStart;
        _dialogueChannel.OnDialogueNodeEnd += OnDialogueNodeEnd;

        gameObject.SetActive(false);
        _choicesBoxTransform.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _dialogueChannel.OnDialogueNodeEnd -= OnDialogueNodeEnd;
        _dialogueChannel.OnDialogueNodeStart -= OnDialogueNodeStart;
    }

    public void ContinueDialogue()
    {
        if (_listenToInput)
        {
            _dialogueChannel.RaiseRequestDialogueNode(_nextNode);

            // TODO : Add a sound effect when the player clicks on the continue button
            AudioManager.Instance?.PlaySFX("SFXTest");
        }
    }

    private void OnDialogueNodeStart(DialogueNode node)
    {
        gameObject.SetActive(true);

        _dialogueText.text = node.DialogueLine.Text;

        _speakerText.text = node.DialogueLine.Speaker.CharacterName;
        _speakerAvatar.sprite = node.DialogueLine.Speaker.CharacterAvatar;

        node.Accept(this);
    }

    private void OnDialogueNodeEnd(DialogueNode node)
    {
        _nextNode = null;
        _listenToInput = false;
        _dialogueText.text = "";
        _speakerText.text = "";

        foreach (Transform child in _choicesBoxTransform)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);
        _choicesBoxTransform.gameObject.SetActive(false);
    }

    public void Visit(BasicDialogueNode node)
    {
        _listenToInput = true;
        _nextNode = node.NextNode;
    }

    public void Visit(ChoiceDialogueNode node)
    {
        _choicesBoxTransform.gameObject.SetActive(true);

        foreach (DialogueChoice choice in node.Choices)
        {
            UIDialogueChoiceController newChoice = Instantiate(_choiceControllerPrefab, _choicesBoxTransform);
            newChoice.Choice = choice;
        }
    }
}