public class StrongEnemy : Enemy
{
    void Start()
    {
        currentHP = maxHP.GetValue();
        currentAP = maxAP.GetValue();

        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        ability1.damage = 3;
        ability1.cost = 1;
        ability2.damage = 2;
        ability2.cost = 3;
        ability2.roundsBeforeReuse = 3;
    }

    protected override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
    }
    protected override void CastAbility2(Entity target)
    {
        currentAP -= ability2.cost;
        if (ability2.roundsBeforeReuse == 0)
        {
            target.TakeDamage(ability2.damage + attack.GetValue());
            ability2.roundsBeforeReuse = 3;
        }
        else
        {
            // Lower the roundBeforeReuse int each round
        }
    }
}
