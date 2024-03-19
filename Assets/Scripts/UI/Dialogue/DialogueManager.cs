using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _dialogueBox;

    private Queue<string> _sentences;
    private Coroutine _typeSentenceCoroutine;
    private bool _isTyping = false;
    private string _currentDialogue;

    public void Start()
    {
        if(_dialogueBox.activeSelf)
        {
            _dialogueBox.SetActive(false);
        }

        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _dialogueBox.SetActive(true);

        _nameText.text = dialogue.Name;

        _sentences.Clear();

        foreach(string sentence in dialogue.Sentences)
        {
            _sentences.Enqueue(sentence); 
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0 && !_isTyping)
        {
            EndDialogue();
            return;
        }
        
        if (_isTyping)
        {
            StopCoroutine(_typeSentenceCoroutine);
            _dialogueText.text = _currentDialogue;
            _isTyping = false;
        }
        else
        {
            _currentDialogue = _sentences.Dequeue();
            _typeSentenceCoroutine = StartCoroutine(TypeSentence(_currentDialogue));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        _isTyping = true;

        _dialogueText.text = "";

        foreach(char letter in  sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        _isTyping = false;
    }

    public void EndDialogue()
    {
        _dialogueBox.SetActive(false);
    }
}
