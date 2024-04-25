using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    public GameObject floatingTextPrefab;
    [Header("HP and AP settings")]
    public Stat MaxHP;
    public Stat MaxAP;
    public float speed;


    protected Ability _ability1;
    protected Ability _ability2;
    protected bool _invincible = false;
    protected Entity _currentTarget;
    protected int _lastAbilityAttack;
    protected List<Cell> _pathToTake;
    protected CombatGrid _grid;
    protected HealthBar _healthBar;
    protected TrigerParticule _particuleSysteme;
    protected Animator _animator;
    protected PlayerSFX _SFXplayer = new PlayerSFX();

    [HideInInspector] public Stat Attack;
    [HideInInspector] public Stat Defense;
    [HideInInspector] public bool IsTurn = false;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public List<Cell> occupiedCells = new List<Cell>();
    [HideInInspector] public Coord CurrentPos { get; set; }
    [HideInInspector] public int CurrentHP { get; set; }
    [HideInInspector] public int CurrentAP { get; set; }
    [HideInInspector] public Coord Size { get; set; }


    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _particuleSysteme = GetComponent<TrigerParticule>();
        _grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();

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
        _currentTarget = target;
        _animator.SetTrigger("Attack");
    }

    public virtual void CastAbility2(Entity target)
    {
        CurrentAP -= _ability2.Cost;
        _currentTarget = target;
        _animator.SetTrigger("Attack");
    }

    public abstract void AttackEvent();

    public void TakeDamage(int damage)
    {
        if(!_invincible)
        {
            damage -= Defense.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            CurrentHP -= damage;
            CurrentHP = Mathf.Clamp(CurrentHP, 0, int.MaxValue);
            if (_healthBar != null)
                _healthBar.SetHP(CurrentHP);
            IsDead();
            ShowFloatingDamage(damage);
        }
        else
        {
            // Do nothing / Show 0 damage
            ShowFloatingDamage(0);
            _invincible = false;
        }
    }

    void ShowFloatingDamage(int damage)
    {
        FloatingText damageText = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 2f, 0), Quaternion.identity).GetComponent<FloatingText>();
        damageText.Init(damage.ToString(), Color.white);
        _particuleSysteme.OnTriggerEntter(this);
    }

    private void IsDead()
    {
        if (CurrentHP <= 0)
        {
            Death();
        }
    }

    protected bool MoveOverTime()
    {
        if (_pathToTake.Count > 0)
        {
            if (speed <= 0f)
                speed = 1f;

            Cell nextCell = _pathToTake.First();
            Vector3 newPosition = nextCell.GameObject.transform.position;
            Vector3 directeur = (newPosition - transform.position);
            Vector3 movement = speed * Time.deltaTime * directeur.normalized;

            if (movement.magnitude >= directeur.magnitude || directeur.magnitude < 0.01f)
            {
                _pathToTake.RemoveAt(0);
                transform.position = newPosition;
                CurrentPos = nextCell.Coord;
                if (_pathToTake.Count == 0)
                {
                    isMoving = false;
                    _animator.SetBool("Move", isMoving);
                }
            }
            else
            {
                transform.position += movement;
                if (movement.x > 0)
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                if (movement.x < 0)
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                if (movement.z > 0)
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                if (movement.z < 0)
                    transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
            return false;
        }
        return true;
    }

    public void Move(List<Cell> pathToTake)
    {
        _pathToTake = pathToTake.GetRange(1, pathToTake.Count - 1);
        isMoving = true;
        _animator.SetBool("Move", isMoving);
        pathToTake.Clear();
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