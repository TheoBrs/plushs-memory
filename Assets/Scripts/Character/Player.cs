
public class Player : Entity
{
    Ability ability1;
    Ability ability2;
    Ability fAbility1;
    Ability fAbility2;
    Ability fAbility3;
    public int pattoBuff = 0;

    public void Start()
    {
        AbilitiesInitialization();
    }

    void AbilitiesInitialization()
    {
        ability1.damage = 2;
        ability1.cost = 1;

        ability2.damage = 4;
        ability2.cost = 2;

        fAbility1.roundsBeforeReuse = 2;
        fAbility2.roundsBeforeReuse = 3;
        fAbility3.roundsBeforeReuse = 4;
    }

    private void CastAbility1(Entity target)
    {
        stats.AP -= ability1.cost;
        if(pattoBuff > 0)
        {
            target.TakeDamage((ability1.damage + stats.attackModifier)*2);
            pattoBuff -= 1;
        }
        else
        {
            target.TakeDamage(ability1.damage + stats.attackModifier);
        }
    }

    private void CastAbility2(Entity target)
    {
        stats.AP -= ability2.cost;
        if (pattoBuff > 0)
        {
            target.TakeDamage((ability2.damage + stats.attackModifier)*2);
            pattoBuff -= 1;
        }
        else
        {
            target.TakeDamage(ability2.damage + stats.attackModifier);
        }
    }

    private void CastFriendAbility1()
    {
        if(fAbility1.roundsBeforeReuse == 0)
        {
            stats.hpModifier += 3;
            stats.HP += 8;
            fAbility1.roundsBeforeReuse = 2;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    private void CastFriendAbility2()
    {
        if (fAbility2.roundsBeforeReuse == 0)
        {
            stats.maxHP += 3;
            invincible = true;
            fAbility2.roundsBeforeReuse = 3;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    private void CastFriendAbility3()
    {
        if (fAbility3.roundsBeforeReuse == 0)
        {
            stats.attackModifier += 1;
            pattoBuff = 2;
            fAbility3.roundsBeforeReuse = 4;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    public void BattleEnd()
    {
        stats.hpModifier = 0;
        stats.attackModifier = 0;
        stats.apModifier = 0;
        stats.defenseModifier = 0;

        stats.HP = stats.maxHP;
        stats.AP = stats.maxAP;
    }

}