using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX 
{
    public AudioSource source;

    public List<string> Softclips = new List<string>{ "hitSoft1", "hitSoft2", "hitSoft3" };
    public List<string> Hardclips = new List<string> { "hitHard1", "hitHard2", "hitHard3" };

    public void Play(bool _isHard)
    {
        if (AudioManager.Instance)
        {
            if (_isHard)
            {
                int index = Random.Range(0, Hardclips.Count);
                AudioManager.Instance.PlaySFX(Hardclips[index]);
            }
            else
            {
                int index = Random.Range(0, Softclips.Count);
                AudioManager.Instance.PlaySFX(Softclips[index]);
            }
        }
    }
}

