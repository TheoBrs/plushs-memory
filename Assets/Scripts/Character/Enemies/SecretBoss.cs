public class SecretBoss : Enemy
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
        _ability1.Cost = 2;
        _ability2.Damage = 1;
        _ability2.Cost = 4;
    }

    public override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    public override void CastAbility2(Entity target)
    {
        _currentAP -= _ability2.Cost;
        target.TakeDamage(_ability2.Damage + Attack.GetValue());
        target.MaxAP.AddModifier(-1);
        target.Attack.AddModifier(-1);
    }
}
