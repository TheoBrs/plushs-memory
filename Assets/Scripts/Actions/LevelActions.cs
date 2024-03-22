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

    public void ReturnMenu()
    {
        ScenesManager.Instance.ReturnMenu();
    }
}
