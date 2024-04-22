using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerSFX 
{
    public AudioSource source;
        
    List<AudioClip> Softclips ;
    List<AudioClip> Hardclips ;
         
    public void Play(bool _isHard)
    {
        if (_isHard)
        {
            int index = Random.Range(0, Hardclips.Count);
            source.PlayOneShot(Hardclips[index]);
        }
        else
        {
            int index = Random.Range(0, Softclips.Count);
            source.PlayOneShot(Softclips[index]);
        }
    }
}

