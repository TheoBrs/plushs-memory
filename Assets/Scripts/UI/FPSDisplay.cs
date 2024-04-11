using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float _pollingTime = 1f;
    private float _time;
    private int _frameCount;

    [SerializeField] TextMeshProUGUI _fpsText;

    private void Update()
    {
        _time += Time.deltaTime;

        _frameCount++;

        if (_time > _pollingTime)
        {
            int frameRate = Mathf.RoundToInt(_frameCount / _time);
            _fpsText.text = frameRate.ToString() + " FPS";

            _time -= _pollingTime;
            _frameCount = 0;
        }
    }
}
