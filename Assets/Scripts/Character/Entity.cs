using UnityEngine;

public class Entity : MonoBehaviour
{
     public struct Coord
    {
        public float x;
        public float y;
    };

    public struct Stats
    {
        public int maxHP;
        public int maxAP;

        public int HP;
        public int AP;

        public int hpModifier;
        public int attackModifier;
        public int apModifier;
        public int defenseModifier;
    }

    public Coord coord;
    public Stats stats;

    public bool invincible = false;

    [Header("Health and Action points Settings")]
    public int maxHP;
    public int maxAP;

    private void Start()
    {
        //Setting max HP and AP for prefabs
        stats.maxHP = maxHP;
        stats.maxAP = maxAP;
    }

    public void TakeDamage(int damage)
    {
        if(invincible == false)
        {
            if(damage > stats.defenseModifier)
            {
                stats.HP -= damage - stats.defenseModifier;
                IsDead();
            }
            else
            {
                // Do nothing / Show 0 damage
            }
        }
        else
        {
            // Do nothing / Show 0 damage
            invincible = false;
        }
    }

    public bool IsDead()
    {
        if (stats.HP <= 0)
        {
            //Manage death in GameManager
            return true;
        }
        return false;
    }
}