public class WeakEnemy : Enemy
{
    void Start()
    {
        currentHP = maxHP.GetValue();
        currentAP = maxAP.GetValue();

        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        ability1.damage = 1;
        ability1.cost = 1;
        ability2.damage = 2;
        ability2.cost = 2;
    }

    protected override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    protected override void CastAbility2(Entity target)
    {
        currentAP -= ability2.cost;
        target.TakeDamage(ability2.damage + attack.GetValue());
    }
}
