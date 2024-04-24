using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private Color _color;
    [SerializeField] private GameObject _textObj;

    public void Init(string text, Color color)
    {
        Destroy(gameObject, _animationClip.length);
        _textObj.GetComponent<TextMeshProUGUI>().text = text;
        _textObj.GetComponent<TextMeshProUGUI>().color = color;
    }
}
