public class SecretBoss : Enemy
{
    void Start()
    {
        _currentHP = GetMaxHP().GetValue();
        _currentAP = GetMaxAP().GetValue();
        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1.Damage = 3;
        _ability1.Cost = 2;
        _ability2.Damage = 1;
        _ability2.Cost = 4;
    }

    protected override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    protected override void CastAbility2(Entity target)
    {
        _currentAP -= _ability2.Cost;
        target.TakeDamage(_ability2.Damage + Attack.GetValue());
        target.GetMaxAP().AddModifier(-1);
        target.Attack.AddModifier(-1);
    }
}
