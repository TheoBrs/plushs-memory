using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] new AnimationClip animation;
    [SerializeField] Color color;
    [SerializeField] GameObject textObj;

    public void Init(string text, Color color)
    {
        Destroy(gameObject, animation.length);
        textObj.GetComponent<TextMeshProUGUI>().text = text;
        textObj.GetComponent<TextMeshProUGUI>().color = color;
    }
}
