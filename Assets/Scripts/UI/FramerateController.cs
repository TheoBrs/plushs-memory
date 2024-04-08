using UnityEngine;

public class FramerateController : MonoBehaviour
{
    public void SetFramerate(int framerate)
    {
        switch (framerate)
        {
            case 30:
                Application.targetFrameRate = 30;
                break;
            case 60:
                Application.targetFrameRate = 60;
                break;
            default:
                Debug.LogWarning("Framerate non pris en charge");
                break;
        }
    }
}