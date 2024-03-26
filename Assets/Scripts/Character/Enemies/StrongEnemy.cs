public class StrongEnemy : Enemy
{
    void Start()
    {
        _currentHP = MaxHP.GetValue();
        _currentAP = MaxAP.GetValue();
        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1.Damage = 3;
        _ability1.Cost = 1;
        _ability2.Damage = 2;
        _ability2.Cost = 3;
        _ability2.RoundsBeforeReuse = 3;
    }

    protected override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    protected override void CastAbility2(Entity target)
    {
        _currentAP -= _ability2.Cost;
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
