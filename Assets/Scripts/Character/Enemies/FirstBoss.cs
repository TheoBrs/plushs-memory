using UnityEngine;

public class FirstBoss : Enemy
{
    [SerializeField] GameObject _enemyPrefab;

    void Start()
    {
        _currentHP = GetMaxHP().GetValue();
        _currentAP = GetMaxAP().GetValue();
        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1.Damage = 2;
        _ability1.Cost = 2;
        _ability2.Cost = 3;
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
