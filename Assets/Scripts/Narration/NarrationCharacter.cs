using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Character")]
public class NarrationCharacter : ScriptableObject
{
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _characterAvatar;

    public string CharacterName => _characterName;
    public Sprite CharacterAvatar => _characterAvatar;
}