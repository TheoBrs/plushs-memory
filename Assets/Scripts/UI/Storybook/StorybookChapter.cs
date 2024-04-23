using UnityEngine;

[CreateAssetMenu(fileName = "New Chapter", menuName = "Scriptable Objects/Storybook/Chapter")]
public class StorybookChapter : ScriptableObject
{
    [SerializeField] private int _index;
    [SerializeField] private string _label;
    [SerializeField] private string _name;
    [SerializeField, TextArea(10, 10)] private string _description;
    [SerializeField] private Sprite _preview;

    public int Index => _index;
    public string Label => _label;
    public string Name => _name;
    public string Description => _description;
    public Sprite Preview => _preview;
}