using UnityEngine;

public class FirstBoss : Enemy
{
    [SerializeField] GameObject enemyPrefab;

    void Start()
    {
        currentHP = maxHP.GetValue();
        currentAP = maxAP.GetValue();

        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        ability1.damage = 2;
        ability1.cost = 2;
        ability2.cost = 3;
    }

    protected override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    protected override void CastAbility2(Entity target)
    {
        // Spawn prefab on random tile
    }
}
