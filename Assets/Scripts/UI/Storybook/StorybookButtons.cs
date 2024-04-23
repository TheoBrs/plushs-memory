using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorybookButtons : MonoBehaviour
{
    [SerializeField] private StorybookChapter _chapter;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Image _previewImage;
    [SerializeField] private Sprite _blockedPreviewImage;

    public delegate void ButtonClickedEventHandler(GameObject clickedButton);
    public static event ButtonClickedEventHandler OnButtonClicked;

    private void Awake()
    {
        // Implement this

        _labelText.text = _chapter.Label;
        _previewImage.sprite = _blockedPreviewImage;
    }

    private void Update()
    {
        // TODO: Change the condition to check if the chapter is unlocked
        if (true)
        {
            _previewImage.sprite = _chapter.Preview;
        }
    }

    public void ButtonClick()
    {
        OnButtonClicked?.Invoke(gameObject);
    }

    public int GetChapterIndex()
    {
        return _chapter.Index;
    }

    public StorybookChapter GetChapter()
    {
        return _chapter;
    }
}
