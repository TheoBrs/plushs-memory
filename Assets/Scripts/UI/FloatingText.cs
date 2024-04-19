using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] AnimationClip animationClip;
    [SerializeField] Color color;
    [SerializeField] GameObject textObj;

    public void Init(string text, Color color)
    {
        Destroy(gameObject, animationClip.length);
        textObj.GetComponent<TextMeshProUGUI>().text = text;
        textObj.GetComponent<TextMeshProUGUI>().color = color;
    }
}
