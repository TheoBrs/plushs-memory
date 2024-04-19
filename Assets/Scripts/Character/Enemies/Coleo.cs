public class Coleo : Enemy
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
        _ability1.Damage = 2;
        _ability1.Cost = 1;
        _ability2.Cost = 3;
        _ability2.RoundsBeforeReuse = 2;
    }

    public override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
        animation.SetTrigger("Damage");
    }
    public override void CastAbility2(Entity target)
    {
        CurrentAP -= _ability2.Cost;
        target.MaxAP.AddModifier(-1);
        _ability2.RoundsBeforeReuse = 2;
    }
}
