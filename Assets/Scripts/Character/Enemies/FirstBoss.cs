using UnityEngine;

public class FirstBoss : Enemy
{
    [SerializeField] GameObject _enemyPrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1.Damage = 2;
        _ability1.Cost = 2;
        _ability2.Cost = 3;
    }

    public override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    public override void CastAbility2(Entity target)
    {
        // Spawn prefab on random tile
    }
}
