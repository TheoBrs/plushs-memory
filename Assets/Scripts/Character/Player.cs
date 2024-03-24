public class Player : Entity
{
    Ability fAbility1;
    Ability fAbility2;
    Ability fAbility3;
    private int pattoBuff = 0;

    public void Start()
    {
        currentHP = maxHP.GetValue();
        currentAP = maxAP.GetValue();
        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        ability1.damage = 2;
        ability1.cost = 1;

        ability2.damage = 4;
        ability2.cost = 2;

        fAbility1.roundsBeforeReuse = 2;
        fAbility2.roundsBeforeReuse = 3;
        fAbility3.roundsBeforeReuse = 4;
    }

    protected override void CastAbility1(Entity target)
    {
        currentAP -= ability1.cost;
        if(pattoBuff > 0)
        {
            target.TakeDamage((ability1.damage + attack.GetValue()) *2);
            pattoBuff -= 1;
        }
        else
        {
            target.TakeDamage(ability1.damage + attack.GetValue());
        }
    }

    protected override void CastAbility2(Entity target)
    {
        currentAP -= ability2.cost;
        if (pattoBuff > 0)
        {
            target.TakeDamage((ability2.damage + attack.GetValue()) *2);
            pattoBuff -= 1;
        }
        else
        {
            target.TakeDamage(ability2.damage + attack.GetValue());
        }
    }

    protected void CastFriendAbility1()
    {
        if(fAbility1.roundsBeforeReuse == 0)
        {
            currentHP += 5;
            fAbility1.roundsBeforeReuse = 2;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    protected void CastFriendAbility2()
    {
        if (fAbility2.roundsBeforeReuse == 0)
        {
            invincible = true;
            fAbility2.roundsBeforeReuse = 3;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    protected void CastFriendAbility3()
    {
        if (fAbility3.roundsBeforeReuse == 0)
        {
            pattoBuff = 2;
            fAbility3.roundsBeforeReuse = 4;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    public override void Death()
    {
        // GameOver
    }
}