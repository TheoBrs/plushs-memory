using UnityEngine;

public class AnimationScripts : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnFadeInFinish()
    {

        // This is if we finish a wave but not the battle full
        var turnSystem = GameObject.FindWithTag("TurnSystem");
        if (turnSystem != null)
            turnSystem.GetComponent<TurnSystem>().OnFadeInFinish();
    }
}
