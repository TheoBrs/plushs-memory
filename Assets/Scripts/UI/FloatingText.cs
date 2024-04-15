using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] new AnimationClip animation;
    [SerializeField] Color color;

    public void Init(string text, Color color)
    {
        Destroy(gameObject, animation.length);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
    }
}
