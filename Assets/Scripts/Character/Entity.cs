using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    public struct Coord
    {
        private float _x;
        private float _y;
    };

    public Coord Coordinates { get; set; }

    [Header("HP and AP settings")]
    public Stat maxHP;
    public Stat maxAP;
    [HideInInspector] public Stat attack;
    [HideInInspector] public Stat defense;

    protected int currentHP;
    protected int currentAP;

    protected Ability ability1;
    protected Ability ability2;

    protected bool invincible = false;

    protected abstract void AbilitiesInitialization();

    protected virtual void CastAbility1(Entity target)
    {
        currentAP -= ability1.cost;
        target.TakeDamage(ability1.damage + attack.GetValue());
    }
    protected abstract void CastAbility2(Entity target);

    public void TakeDamage(int damage)
    {
        if(invincible == false)
        {
            damage -= damage - defense.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            currentHP -= damage;
            IsDead();
        }
        else
        {
            // Do nothing / Show 0 damage
            invincible = false;
        }
    }

    private void IsDead()
    {
        if (currentHP <= 0)
        {
            Death();
        }
    }

    public abstract void Death();

    public void BattleEnd()
    {
        maxHP.RemoveAllModifiers();
        maxAP.RemoveAllModifiers();
        attack.RemoveAllModifiers();
        defense.RemoveAllModifiers();

        currentHP = maxHP.GetValue();
        currentAP = maxAP.GetValue();
    }
}