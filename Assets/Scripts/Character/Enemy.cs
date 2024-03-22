
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : Entity
{
    Ability weakAbility1;
    Ability weakAbility2;

    Ability midAbility1;
    Ability midAbility2;

    Ability strongAbility1;
    Ability strongAbility2;

    Ability bossAbility1;
    Ability bossAbility2;
    [SerializeField] GameObject enemyPrefab;

    Ability sBossAbility1;
    Ability sBossAbility2;

    public void Start()
    {
        AbilitiesInitialization();
    }

    void AbilitiesInitialization()
    {
        weakAbility1.damage = 1;
        weakAbility1.cost = 1;
        weakAbility2.damage = 2;
        weakAbility2.cost = 2;

        midAbility1.damage = 2;
        midAbility1.cost = 1;
        midAbility2.cost = 3;
        midAbility2.roundsBeforeReuse = 2;

        strongAbility1.damage = 3;
        strongAbility1.cost = 1;
        strongAbility2.damage = 2;
        strongAbility2.cost = 3;
        strongAbility2.roundsBeforeReuse = 3;

        bossAbility1.damage = 2;
        bossAbility1.cost = 2;
        bossAbility2.cost = 3;

        sBossAbility1.damage = 3;
        sBossAbility1.cost = 2;
        sBossAbility2.damage = 1;
        sBossAbility2.cost = 4;
    }

    private void CastWeakAbility1(Entity target)
    {
        stats.AP -= weakAbility1.cost;
        target.TakeDamage(weakAbility1.damage + stats.attackModifier);
    }
    private void CastWeakAbility2(Entity target)
    {
        stats.AP -= weakAbility2.cost;
        target.TakeDamage(weakAbility2.damage + stats.attackModifier);
    }

    private void CastMidAbility1(Entity target)
    {
        stats.AP -= midAbility1.cost;
        target.TakeDamage(midAbility1.damage + stats.attackModifier);
    }
    private void CastMidAbility2(Entity target)
    {
        stats.AP -= midAbility2.cost;
        if (midAbility2.roundsBeforeReuse == 0)
        {
            target.stats.apModifier -= 1;
            midAbility2.roundsBeforeReuse = 2;
        }
        else
        {
            // Lower the roundBeforeReuse int each round
        }
    }

    private void CastStrongAbility1(Entity target)
    {
        stats.AP -= strongAbility1.cost;
        target.TakeDamage(strongAbility1.damage + stats.attackModifier);
    }
    private void CastStrongAbility2(Entity target)
    {
        stats.AP -= strongAbility2.cost;
        if (strongAbility2.roundsBeforeReuse == 0)
        {
            target.TakeDamage(strongAbility2.damage + stats.attackModifier);
            strongAbility2.roundsBeforeReuse = 3;
        }
        else
        {
            // Lower the roundBeforeReuse int each round
        }
    }

    private void CastBossAbility1(Entity target)
    {
        stats.AP -= bossAbility1.cost;
        target.TakeDamage(bossAbility1.damage + stats.attackModifier);
    }
    private void CastBossAbility2()
    {
        // Spawn enemyPrefab on random tile
    }

    private void CastSecretBossAbility1(Entity target)
    {
        stats.AP -= sBossAbility1.cost;
        target.TakeDamage(sBossAbility1.damage + stats.attackModifier);
    }
    private void CastSecretBossAbility2(Entity target)
    {
        stats.AP -= sBossAbility2.cost;
        target.TakeDamage(sBossAbility2.damage + stats.attackModifier);
        target.stats.apModifier -= 1;
        target.stats.attackModifier -= 1;
    }

}