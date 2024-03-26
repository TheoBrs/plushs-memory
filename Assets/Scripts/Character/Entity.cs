using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [Header("HP and AP settings")]
    public Stat MaxHP;
    public Stat MaxAP;
    [HideInInspector] public Stat Attack;
    [HideInInspector] public Stat Defense;

    protected Coord _currentPos;
    protected int _currentHP;
    protected int _currentAP;
    protected Ability _ability1;
    protected Ability _ability2;
    protected bool _invincible = false;

    CombatGrid _grid;

    private void Awake()
    {
        _grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
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

    public void Move(Coord coordTo, bool instant)
    {
        GridElement gridElement = _grid.GetGridElement(coordTo.x, coordTo.y);

        Vector3 newPosition = gridElement.GetGameObjectPosition();
        
        if ((transform.position - newPosition).magnitude < 0.02f || instant)
        {
            transform.position = newPosition;
            _currentPos = gridElement.GetCoord();
        }
        else
        {
            Vector3 directeur = 10f * Time.deltaTime * (newPosition - transform.position).normalized;
            transform.position += directeur;
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