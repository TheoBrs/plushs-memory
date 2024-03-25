public class Player : Entity
{
    private Ability _fAbility1;
    private Ability _fAbility2;
    private Ability _fAbility3;
    private int _pattoBuff = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1.Damage = 2;
        _ability1.Cost = 1;

        _ability2.Damage = 4;
        _ability2.Cost = 2;

        _fAbility1.RoundsBeforeReuse = 2;
        _fAbility2.RoundsBeforeReuse = 3;
        _fAbility3.RoundsBeforeReuse = 4;
    }

    protected override void CastAbility1(Entity target)
    {
        _currentAP -= _ability1.Cost;
        if(_pattoBuff > 0)
        {
            target.TakeDamage((_ability1.Damage + Attack.GetValue()) *2);
            _pattoBuff -= 1;
        }
        else
        {
            target.TakeDamage(_ability1.Damage + Attack.GetValue());
        }
    }

    protected override void CastAbility2(Entity target)
    {
        _currentAP -= _ability2.Cost;
        if (_pattoBuff > 0)
        {
            target.TakeDamage((_ability2.Damage + Attack.GetValue()) *2);
            _pattoBuff -= 1;
        }
        else
        {
            target.TakeDamage(_ability2.Damage + Attack.GetValue());
        }
    }

    protected void CastFriendAbility1()
    {
        if(_fAbility1.RoundsBeforeReuse == 0)
        {
            _currentHP += 5;
            _fAbility1.RoundsBeforeReuse = 2;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    protected void CastFriendAbility2()
    {
        if (_fAbility2.RoundsBeforeReuse == 0)
        {
            _invincible = true;
            _fAbility2.RoundsBeforeReuse = 3;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    protected void CastFriendAbility3()
    {
        if (_fAbility3.RoundsBeforeReuse == 0)
        {
            _pattoBuff = 2;
            _fAbility3.RoundsBeforeReuse = 4;
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