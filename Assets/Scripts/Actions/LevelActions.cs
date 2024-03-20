using UnityEngine;

public class LevelActions : MonoBehaviour
{
    [SerializeField] private GameObject _UIGameObject;

    void Start()
    {
        if (!_UIGameObject.activeSelf)
        {
            _UIGameObject.SetActive(true);
        }
    }
}
