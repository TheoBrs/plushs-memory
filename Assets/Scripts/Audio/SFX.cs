
using System.Diagnostics;
using UnityEngine;

public class SFX 
{
    [System.Serializable]
    public class Sound
    {

        [SerializeField] private AudioClip _effect1;
        [SerializeField] private AudioClip _effect2;
        [SerializeField] private AudioClip _effect3;
        [SerializeField] private AudioClip _effect4;
        

    }

    public void Play(AudioClip SFX)
    {
    /*
        if (triger is Enemy|| Player Super attack) State = HitHard 
        switch (State)
        {
            case hitSlow:
                random SFXSlow;

            case hitHard:
                random SFXHard;
        }*/
    }

}
