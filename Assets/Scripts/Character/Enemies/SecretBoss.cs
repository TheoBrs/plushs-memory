public class SecretBoss : Enemy
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
        CurrentAP -= _ability2.Cost;
        animator.SetTrigger("Damage");
        target.TakeDamage(_ability2.Damage + Attack.GetValue());
        target.MaxAP.AddModifier(-1);
        target.Attack.AddModifier(-1);
    }
}
