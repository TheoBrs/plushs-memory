using TMPro;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void DisplayTutorial(Tutorial tutorial)
    {
        _nameText.text = tutorial.Name;
        _descriptionText.text = tutorial.Description;
    }
}
