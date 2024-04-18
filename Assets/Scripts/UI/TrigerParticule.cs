using Unity.VisualScripting;
using UnityEngine;

public class TrigerParticule : MonoBehaviour
{
    [SerializeField] ParticleSystem  blood ;
    [SerializeField] ParticleSystem  Greenblood ;
    public void OnTriggerEntter(Entity entity)
    {
        if(entity.CompareTag("Player"))
        {
            blood.Play();
        }
        else if (entity.CompareTag("Enemy"))
        {
            Greenblood.Play();
        }
    }
}
