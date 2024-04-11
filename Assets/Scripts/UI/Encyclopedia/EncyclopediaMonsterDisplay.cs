using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaMonsterDisplay : MonoBehaviour
{
    [SerializeField] private EncyclopediaMonster _monster;
    [SerializeField] private Scrollbar _descriptionScrollbar;

    // UI
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _monstersKilledText;
    [SerializeField] private Image _imageAvatar;
    [SerializeField] private Sprite _notDiscoveredSprite;

    void Start()
    {
        _nameText.text = _monster.Name;
        _descriptionText.text = _monster.Description;
        _imageAvatar.sprite = _monster.Avatar;

        // TO DO : Read the number of monsters killed
        _monstersKilledText.text = 0.ToString();
    }

    public void SelectMonster()
    {
        _nameText.text = _monster.Name;
        _descriptionText.text = _monster.Description;

        // TO DO : Read the number of monsters killed
        _monstersKilledText.text = 0.ToString();

        // Set the scrollbar to the top
        _descriptionScrollbar.value = 1;
    }
}
