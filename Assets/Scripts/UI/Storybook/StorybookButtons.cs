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

    private void Start()
    {
        _labelText.text = _chapter.Label;

        if (_chapter.Index <= GameManager.Instance.Progression)
        {
            _previewImage.sprite = _chapter.Preview;
        }
        else
        {
            _previewImage.sprite = _blockedPreviewImage;
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
