using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial", menuName = "Scriptable Objects/Tutorial")]
public class Tutorial : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    public string Name => _name;
    public string Description => _description;
}
