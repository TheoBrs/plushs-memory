public class MidEnemy : Enemy
{
    void Start()
    {
        _currentHP = MaxHP.GetValue();
        _currentAP = MaxAP.GetValue();

        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1.Damage = 2;
        _ability1.Cost = 1;
        _ability2.Cost = 3;
        _ability2.RoundsBeforeReuse = 2;
    }

    public override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    public override void CastAbility2(Entity target)
    {
        _currentAP -= _ability2.Cost;
        if (_ability2.RoundsBeforeReuse == 0)
        {
            target.MaxAP.AddModifier(-1);
            _ability2.RoundsBeforeReuse = 2;
        }
        else
        {
            // Lower the roundBeforeReuse int each round
        }
    }
}
