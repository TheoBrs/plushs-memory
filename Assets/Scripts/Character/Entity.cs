using Board;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [Header("HP and AP settings")]
    public Stat MaxHP;
    public Stat MaxAP;
    [HideInInspector] public Stat Attack;
    [HideInInspector] public Stat Defense;

    protected int _currentHP;
    protected int _currentAP;
    public Coord _currentPos;
    protected Ability _ability1;
    protected Ability _ability2;

    protected bool _invincible = false;

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

    public void Move(Coord to)
    {
        _currentPos = to;
        /*
        if ((transform.position - newPosition).magnitude < 0.01f)
            transform.position = newPosition;
        else
        {
            Vector3 directeur = (newPosition - transform.position).normalized * Time.deltaTime * movementSpeed;

            transform.position += directeur;
        }*/
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