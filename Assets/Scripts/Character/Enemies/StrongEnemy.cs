public class StrongEnemy : Enemy
{
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
        _ability1.Damage = 3;
        _ability1.Cost = 1;
        _ability2.Damage = 2;
        _ability2.Cost = 3;
        _ability2.RoundsBeforeReuse = 3;
    }

    public override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    public override void CastAbility2(Entity target)
    {
        CurrentAP -= _ability2.Cost;
        if (_ability2.RoundsBeforeReuse == 0)
        {
            target.TakeDamage(_ability2.Damage + Attack.GetValue());
            _ability2.RoundsBeforeReuse = 3;
        }
        else
        {
            // Lower the roundBeforeReuse int each round
        }
    }
}
