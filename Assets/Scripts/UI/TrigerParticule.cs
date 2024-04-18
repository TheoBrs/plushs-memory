using UnityEngine;

public class TrigerParticule : MonoBehaviour
{
    [SerializeField] ParticleSystem  blood ;
    public void OnTriggerEntter(Entity entity)
    {
        if(entity.CompareTag("Player"))
        {
            blood.Play();
        }
    }
}
