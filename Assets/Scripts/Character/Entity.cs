using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [Header("HP and AP settings")]
    public Stat MaxHP;
    public Stat MaxAP;
    public float speed;
    [HideInInspector] public Stat Attack;
    [HideInInspector] public Stat Defense;

    public Coord CurrentPos { get; set; }
    public int CurrentHP { get; set; }
    public int CurrentAP { get; set; }
    protected Ability _ability1;
    protected Ability _ability2;
    protected bool _invincible = false;


    protected bool _isMoving = false;
    protected List<Cell> _pathToTake;

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

    protected bool MoveOverTime()
    {
        if (_pathToTake.Count > 0)
        {
            Cell nextCell = _pathToTake.First();

            Vector3 newPosition = nextCell.GameObject.transform.position;

            // Increase value if model ossilate between two direction during movement
            // If the speed is bigger so must be the constant
            // This works for a speed of 2
            if ((transform.position - newPosition).magnitude < 0.05f)
            {
                _pathToTake.RemoveAt(0);
                transform.position = newPosition;
                CurrentPos = nextCell.Coord;
                if (_pathToTake.Count == 0)
                {
                    _isMoving = false;
                }
            }
            else
            {
                if (speed <= 0f)
                    speed = 1f;

                Vector3 directeur = (newPosition - transform.position).normalized;
                transform.position += speed * Time.deltaTime * directeur;
                if (directeur.x == 1)
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                if (directeur.x == -1)
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                if (directeur.z == 1)
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                if (directeur.z == -1)
                    transform.localRotation = Quaternion.Euler(0, 90, 0);
                Debug.Log(directeur);
            }
            return false;
        }
        return true;
    }

    public void Move(List<Cell> pathToTake)
    {
        _pathToTake = pathToTake;
        _isMoving = true;
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