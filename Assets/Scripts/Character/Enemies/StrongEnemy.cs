public class StrongEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1.Damage = 3;
        _ability1.Cost = 1;
        _ability2.Damage = 2;
        _ability2.Cost = 3;
        _ability2.RoundsBeforeReuse = 3;
    }

    public override void CastAbility1(Entity target)
    {
        lastAbilityAttack = 1;
        base.CastAbility1(target);
    }
    public override void CastAbility2(Entity target)
    {
        lastAbilityAttack = 2;
        base.CastAbility2(target);
        _ability2.RoundsBeforeReuse = 3;
    }
}
