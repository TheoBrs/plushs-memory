using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaMonsterDisplay : MonoBehaviour
{
    [SerializeField] private EncyclopediaMonster _monster;
    [SerializeField] private Scrollbar _descriptionScrollbar;
    [SerializeField] private bool _checkFirst;

    // UI
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _monstersKilledText;
    [SerializeField] private Image _imageAvatar;
    [SerializeField] private Sprite _notDiscoveredSprite;

    private void Awake()
    {
        _descriptionScrollbar.value = 1;
        _imageAvatar.sprite = _monster.Avatar;
    }

    private void Start()
    {
        if (_checkFirst)
        {
            SelectMonster();
        }
    }

    public void SelectMonster()
    {
        _descriptionScrollbar.value = 1;

        _nameText.text = _monster.Name;
        _descriptionText.text = _monster.Description;

        SwitchMonsterKillCount();
    }

    private void SwitchMonsterKillCount()
    {
        switch (_monster.name)
        {
            case "Mite":
                _monstersKilledText.text = StatisticsManager.Instance.miteKillCount.ToString();
                break;
            case "Coleoptere":
                _monstersKilledText.text = StatisticsManager.Instance.coleoptereKillCount.ToString();
                break;
            case "Souris":
                _monstersKilledText.text = 0.ToString();
                break;
            case "ReineMite":
                _monstersKilledText.text = 0.ToString();
                break;
            default:
                break;
        }
    }
}
