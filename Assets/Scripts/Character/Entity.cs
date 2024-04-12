using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Entity: MonoBehaviour
{
    public GameObject floatingTextPrefab;
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

    public bool isMoving = false;
    protected List<Cell> _pathToTake;
    protected CombatGrid grid;
    protected HealthBar healthBar;

    protected virtual void Start()
    {
        grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();

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
            if (healthBar != null)
                healthBar.SetHP(CurrentHP);
            IsDead();
            ShowFloatingDamage(damage);
        }
        else
        {
            // Do nothing / Show 0 damage
            _invincible = false;
        }
    }

    void ShowFloatingDamage(int damage)
    {
        GameObject damageText = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 2.3f, 0), Quaternion.identity);
        damageText.GetComponent<FloatingText>().Init(damage.ToString(), Color.white);
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

            if (movement.magnitude >= directeur.magnitude)
            {
                _pathToTake.RemoveAt(0);
                transform.position = newPosition;
                CurrentPos = nextCell.Coord;
                if (_pathToTake.Count == 0)
                {
                    isMoving = false;
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
        _pathToTake = pathToTake.GetRange(1, pathToTake.Count - 1); ;
        isMoving = true;
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