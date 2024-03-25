using Board;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [Header("HP and AP settings")]
    public Stat MaxHP;
    public Stat MaxAP;
    [HideInInspector] public Stat Attack;
    [HideInInspector] public Stat Defense;

    public Coord _currentPos; // Public for Testing

    protected int _currentHP;
    protected int _currentAP;
    protected Ability _ability1;
    protected Ability _ability2;

    protected bool _invincible = false;

    GameObject _grid;

    private void Awake()
    {
        _grid = GameObject.Find("WorldPlane");
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

    public void Move(Coord to)
    {
        GridElement gridElement = _grid.GetComponent<Board.Grid>().elements[to.x, to.y];

        Vector3 newPosition = gridElement.gridElement.transform.position;
        
        if ((transform.position - newPosition).magnitude < 0.02f)
        {
            transform.position = newPosition;
            _currentPos = gridElement.coord;
        }
        else
        {
            Vector3 directeur = (newPosition - transform.position).normalized * Time.deltaTime * 10f;
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