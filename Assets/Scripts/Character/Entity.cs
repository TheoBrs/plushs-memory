using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [Header("HP and AP settings")]
    public Stat MaxHP;
    public Stat MaxAP;
    [HideInInspector] public Stat Attack;
    [HideInInspector] public Stat Defense;

    public Coord CurrentPos { get; set; }
    public int CurrentHP { get; set; }
    public int CurrentAP { get; set; }
    protected Ability _ability1;
    protected Ability _ability2;
    protected bool _invincible = false;

    CombatGrid _grid;

    private void Awake()
    {
        _grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
    }

    protected virtual void Start()
    {
        CurrentHP = MaxHP.GetValue();
        CurrentAP = MaxAP.GetValue();

        _ability1 = new Ability();
        _ability2 = new Ability();

        AbilitiesInitialization();
    }

    protected abstract void AbilitiesInitialization();

    public virtual void CastAbility1(Entity target)
    {
        CurrentAP -= _ability1.Cost;
        target.TakeDamage(_ability1.Damage + Attack.GetValue());
    }
    public abstract void CastAbility2(Entity target);

    public void TakeDamage(int damage)
    {
        if(!_invincible)
        {
            damage -= Defense.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            CurrentHP -= damage;
            CurrentHP = Mathf.Clamp(CurrentHP, 0, int.MaxValue);
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
        if (CurrentHP <= 0)
        {
            Death();
        }
    }

    public void Move(Coord coordTo, bool instant)
    {
        Cell gridElement = _grid.GetGridElement(coordTo.X, coordTo.Y);

        Vector3 newPosition = gridElement.GameObject.transform.position;
        
        if ((transform.position - newPosition).magnitude < 0.02f || instant)
        {
            transform.position = newPosition;
            CurrentPos = gridElement.Coord;
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

        CurrentHP = MaxHP.GetValue();
        CurrentAP = MaxAP.GetValue();
    }
}