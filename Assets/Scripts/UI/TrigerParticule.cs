using UnityEngine;

public class TrigerParticule : MonoBehaviour
{
    [SerializeField]ParticleSystem  blood;
   
    void Start()
    {
        blood = GetComponent<ParticleSystem>();
    }
    public void OnTriggerEntter(Entity entity)
    {
        if(entity.tag == "ennemy")
        {
            blood.Play();
        }
    }
}
