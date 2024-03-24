public class MidEnemy : Enemy
{
    void Start()
    {
        currentHP = maxHP.GetValue();
        currentAP = maxAP.GetValue();

        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        ability1.damage = 2;
        ability1.cost = 1;
        ability2.cost = 3;
        ability2.roundsBeforeReuse = 2;
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
            target.maxAP.AddModifier(-1);
            ability2.roundsBeforeReuse = 2;
        }
        else
        {
            // Lower the roundBeforeReuse int each round
        }
    }
}
