using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _jsonFile;
    [SerializeField] private int _characterId;

    [System.Serializable]
    public class DialogueList
    {
        public Dialogue[] dialogue;
    }

    public DialogueList dialogueList = new();

    void Start()
    {
        if (_jsonFile == null)
        {
            Debug.LogError("Le fichier JSON n'est pas défini.");
            return;
        }

        dialogueList = JsonUtility.FromJson<DialogueList>(_jsonFile.text);
    }

    public void TriggerDialogue()
    {
        Dialogue dialogueToTrigger = null;
        foreach (var dialogue in dialogueList.dialogue)
        {
            if (dialogue.Id == _characterId)
            {
                dialogueToTrigger = dialogue;
                break;
            }
        }

        if (dialogueToTrigger != null)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueToTrigger);
        }
        else
        {
            Debug.LogWarning("Dialogue non trouvé pour le personnage avec l'ID : " + _characterId);
        }
    }
}
