using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private string[] sentences;

    public int Id => id;
    public string Name => name; 
    public string[] Sentences => sentences;
}