using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] new AnimationClip animation;
    [SerializeField] Color color;

    public void Init(string text, Color color)
    {
        Destroy(gameObject, animation.length);
        Debug.Log(animation.length);
        GetComponent<TextMesh>().text = text;
        GetComponent<TextMesh>().color = color;
    }
}
