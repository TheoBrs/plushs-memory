using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    public struct Coord
    {
        private float _x;
        private float _y;
    };

    [Header("HP and AP settings")]
    public Stat MaxHP;
    public Stat MaxAP;
    [HideInInspector] public Stat Attack;
    [HideInInspector] public Stat Defense;

    protected int _currentHP;
    protected int _currentAP;

    protected Ability _ability1;
    protected Ability _ability2;

    protected bool _invincible = false;


    protected virtual void Start()
    {
        _currentHP = MaxHP.GetValue();
        _currentAP = MaxAP.GetValue();

        AbilitiesInitialization();
    }

    protected abstract void AbilitiesInitialization();

    protected virtual void CastAbility1(Entity target)
    {
        _currentAP -= _ability1.Cost;
        target.TakeDamage(_ability1.Damage + Attack.GetValue());
    }
    protected abstract void CastAbility2(Entity target);

    public void TakeDamage(int damage)
    {
        if(!_invincible)
        {
            damage -= Defense.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            _currentHP -= damage;
            _currentHP = Mathf.Clamp(_currentHP, 0, int.MaxValue);
            IsDead();
        }
        else
        {
            // Do nothing / Show 0 damage
            _invincible = false;
        }
    }

    private void IsDead()
    {
        if (_currentHP <= 0)
        {
            Death();
        }
    }

    public abstract void Death();

    public void BattleEnd()
    {
        MaxHP.RemoveAllModifiers();
        MaxAP.RemoveAllModifiers();
        Attack.RemoveAllModifiers();
        Defense.RemoveAllModifiers();

        _currentHP = MaxHP.GetValue();
        _currentAP = MaxAP.GetValue();
    }
}