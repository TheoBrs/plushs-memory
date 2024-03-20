using UnityEngine;

public class SafeAreaSetter : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private RectTransform _panelSafeArea;
    private Rect _currentSafeArea = new();
    private ScreenOrientation _currentOrientation = ScreenOrientation.AutoRotation;

    public void Start()
    {
        _panelSafeArea = GetComponent<RectTransform>();

        // store current value
        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;

        ApplySafeArea();
    }

    public void ApplySafeArea()
    {
        if (!_panelSafeArea) return;

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= _canvas.pixelRect.width;
        anchorMin.y /= _canvas.pixelRect.height;

        anchorMax.x /= _canvas.pixelRect.width;
        anchorMax.y /= _canvas.pixelRect.height;

        _panelSafeArea.anchorMin = anchorMin;
        _panelSafeArea.anchorMax = anchorMax;

        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;
    }

    public void Update()
    {
        if ((_currentOrientation != Screen.orientation) || (_currentSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}
