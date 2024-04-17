using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Transition : MonoBehaviour
{
    public Animator transition;
    public VideoPlayer videoPlayer;

    public float transitionTime = 1.0f;

    public void Next()
    {

    }

    void Update()
    {
        //load terrain
    }

    IEnumerator NextScene(int Index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        
    }

}
