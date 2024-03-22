using System.Collections;
using System.Collections.Generic;
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
        public int HP;
        public int maxAP;
        public int AP;
        public int attackAttribute;
    }

    public struct Ability
    {
        public bool isOffensive;
        public int damage;
        public int cost;
        public int roundsBeforeReuse;

        public void SpawnEnemy(GameObject enemyToSpawn)
        {

        }
    }

    Coord coord;
    Stats stats;

    [SerializeField] public int maxHP;
    [SerializeField] public int maxAP;

    List<Ability> abilities;

    private void Start()
    {
        //Setting max HP and AP for prefabs
        stats.maxHP = maxHP;
        stats.maxAP = maxAP;

        //Initialize abilities for each prefab
    }

    public void CastAbility(Ability ability, Entity target)
    {
        stats.AP -= ability.cost;

        if (ability.isOffensive)
        {
            target.TakeDamage(ability.damage + stats.attackAttribute);
        }

        //Delay with roundsBeforeReuse
    }

    public void TakeDamage(int damage)
    {
        stats.HP -= damage;
        IsDead();
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

    public void AttackModifier(Stats stats, int amount)
    {
        stats.attackAttribute += amount;
    }

    public void APModifier(Stats stats, int amount)
    {
        stats.AP += amount;
    }
}