using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScripts : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnFadeInFinish()
    {
        GameObject.FindWithTag("TurnSystem").GetComponent<TurnSystem>().OnFadeInFinish();
    }
}
