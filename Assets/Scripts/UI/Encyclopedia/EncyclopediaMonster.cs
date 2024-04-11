using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Encyclopedia/Monster")]
public class EncyclopediaMonster : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField, TextArea(10, 10)] private string _description;
    [SerializeField] private Sprite _avatar;

    public string Name => _name;
    public string Description => _description;
    public Sprite Avatar => _avatar;
}