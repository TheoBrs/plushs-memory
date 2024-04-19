using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorybookChapterDisplay : MonoBehaviour
{
    [SerializeField] private StorybookChapter _chapter;
    [SerializeField] private Scrollbar _descriptionScrollbar;
    [SerializeField] private bool _checkFirst;

    // UI
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _previewImage;
    [SerializeField] private Sprite _blockedPreviewImage;

    private void Awake()
    {
        _descriptionScrollbar.value = 1;

        _labelText.text = _chapter.Label;
        _nameText.text = _chapter.Name;
        _descriptionText.text = _chapter.Description;
        _previewImage.sprite = _chapter.Preview;
    }

    private void Start()
    {
        if (_checkFirst)
        {
            SelectChapter();
        }
    }

    public void SelectChapter()
    {
        _descriptionScrollbar.value = 1;

        _nameText.text = _chapter.Name;
        _descriptionText.text = _chapter.Description;
    }
}
