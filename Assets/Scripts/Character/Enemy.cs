using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum State
{
    WaitForTurn,
    Movement,
    Attacking,
    EndTurn
}

public abstract class Enemy : Entity, IDataPersistence
{
    public bool cannotMove = false;
    public bool causeEndOfBattle = false;
    public bool justSpawned;
    public string _name = "Enemy";
    public bool ability2IsntAttack = false;

    private State _currentState;
    private int _miteKillCount;
    private int _coleoptereKillCount;

    protected override void Awake()
    {
        base.Awake();
        _currentState = State.WaitForTurn;

        _healthBar = ToolBox.GetChildWithTag(gameObject.transform, "HealthBar").GetComponent<HealthBar>();
        _healthBar.SetMaxHP(MaxHP.GetValue());
    }

    protected virtual void Update()
    {
        switch (_currentState)
        {
            case State.WaitForTurn:
                if (IsTurn)
                {
                    CurrentAP = MaxAP.GetValue();
                    if (justSpawned)
                    {
                        justSpawned = false;
                        ChangeState(State.EndTurn);
                        break;
                    }
                    ChangeState(State.Movement);
                }
                break;
            case State.Movement:
                bool hasFinishedMoving = Movement();
                if (hasFinishedMoving)
                    ChangeState(State.Attacking);
                break;
            case State.Attacking:
                Attacking();
                ChangeState(State.EndTurn);
                break;
            case State.EndTurn:
                IsTurn = false;
                TurnSystem turnSystem = GameObject.FindWithTag("TurnSystem").GetComponent<TurnSystem>();
                turnSystem.NextEnemyTurn();
                ChangeState(State.WaitForTurn);
                break;
        }
    }

    private bool Movement()
    {
        if (cannotMove || CurrentAP < 0)
            return true;

        if (isMoving)
        {
            isMoving = !MoveOverTime();
        }
        else
        {
            // Check if the player is reachable
            Player _player = FindObjectOfType<Player>();
            List<Cell> _pathToPlayer = AStar.FindPath(CurrentPos, _player.CurrentPos, true);

            _pathToPlayer.RemoveAt(_pathToPlayer.Count - 1);

            if ( _pathToPlayer.Count > 1)
            {
                if(_pathToPlayer.Count - 1 > CurrentAP)
                {
                    _pathToPlayer.RemoveRange(CurrentAP + 1, _pathToPlayer.Count - CurrentAP - 1);
                }

                // Move to the position
                CurrentAP -= _pathToPlayer.Count - 1;

                Coord nextPos = _pathToPlayer.Last().Coord;

                Move(_pathToPlayer);
                _grid.GetGridCell(CurrentPos.X, CurrentPos.Y).HasEnemy = false;
                _grid.GetGridCell(nextPos.X, nextPos.Y).HasEnemy = true;
                _grid.GetGridCell(nextPos.X, nextPos.Y).Entity = _grid.GetGridCell(CurrentPos.X, CurrentPos.Y).Entity;
                _grid.GetGridCell(CurrentPos.X, CurrentPos.Y).Entity = null;
                CurrentPos = nextPos;
                occupiedCells.Clear();
                occupiedCells.Add(_grid.GetGridCell(nextPos.X, nextPos.Y));
                _grid.RefreshGridMat();
            }
        }
        return !isMoving;

        // Verify he arrived at the position / Wait till animation is done
    }

    virtual public void Attacking()
    {
        Player _player = FindObjectOfType<Player>();

        if ((_player.transform.position - transform.position).magnitude == 1 * _grid.gridCellScale)
        {
            Vector3 directeur = (_player.transform.position - transform.position);
            if (directeur.x > 0)
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (directeur.x < 0)
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            if (directeur.z > 0)
                transform.localRotation = Quaternion.Euler(0, -90, 0);
            if (directeur.z < 0)
                transform.localRotation = Quaternion.Euler(0, 90, 0);

            if (CurrentAP > 0)
            {
                if (_ability2.RoundsBeforeReuse == 0)
                {
                    if (CurrentAP >= _ability2.Cost)
                    {
                        CastAbility2(_player);
                    }
                }
                
                if (_ability1.RoundsBeforeReuse == 0)
                {
                    if (CurrentAP >= _ability1.Cost)
                    {
                        CastAbility1(_player);
                    }
                }
            }
        }

        _ability1.RoundsBeforeReuse = Mathf.Clamp(_ability1.RoundsBeforeReuse - 1, 0, 10);
        _ability2.RoundsBeforeReuse = Mathf.Clamp(_ability2.RoundsBeforeReuse - 1, 0, 10);
    }

    public override void AttackEvent()
    {
        if (_lastAbilityAttack == 1)
        {
            _SFXplayer.Play(false);
            _currentTarget.TakeDamage(_ability1.Damage + Attack.GetValue());
        }
        if (_lastAbilityAttack == 2)
        {
            _SFXplayer.Play(true);
            _currentTarget.TakeDamage(_ability2.Damage + Attack.GetValue());
        }
    }
    void ChangeState(State newState)
    {
        _currentState = newState;
    }

    public void SaveData(GameData data)
    {
        data.miteKillCount = _miteKillCount;
        data.coleoptereKillCount = _coleoptereKillCount;
    }

    public void LoadData(GameData data)
    {
        _miteKillCount = data.miteKillCount;
        _coleoptereKillCount = data.coleoptereKillCount;
    }

    public override void Death()
    {
        if (this is Mite)
        {
            _miteKillCount++;
        }
        if (this is Coleo)
        {
            _coleoptereKillCount++;
        }


        TurnSystem turnSystyem = GameObject.FindGameObjectWithTag("TurnSystem").GetComponent<TurnSystem>();
        turnSystyem.OnEnemyDeath(this, causeEndOfBattle);
        // Enemy Death / Inform GameManager
        // You should remove yourself
        foreach (Cell cell in occupiedCells)
        {
            cell.SetGameObjectMaterial(_grid.GetDefaultGridMat());
            cell.HasEnemy = false;
            cell.Entity = null;
        }
        Destroy(gameObject);
    }
}