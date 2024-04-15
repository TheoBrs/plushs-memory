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

public abstract class Enemy : Entity
{
    private State _currentState;
    public bool ItsTurn = false;
    [SerializeField] public string _name = "Enemy";

    protected override void Start()
    {
        base.Start();
        _currentState = State.WaitForTurn;

        healthBar = ToolBox.GetChildWithTag(gameObject.transform, "HealthBar").GetComponent<HealthBar>();
        healthBar.SetMaxHP(CurrentHP);
    }

    protected virtual void Update()
    {
        switch (_currentState)
        {
            case State.WaitForTurn:
                if (ItsTurn)
                {
                    CurrentAP = MaxAP.GetValue();
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
                ItsTurn = false;
                TurnSystem turnSystem = GameObject.FindWithTag("TurnSystem").GetComponent<TurnSystem>();
                turnSystem.NextEnemyTurn();
                ChangeState(State.WaitForTurn);
                break;
        }
    }

    private bool Movement()
    {
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
                grid.GetGridCell(CurrentPos.X, CurrentPos.Y).HasEnemy = false;
                grid.GetGridCell(nextPos.X, nextPos.Y).HasEnemy = true;
                grid.GetGridCell(nextPos.X, nextPos.Y).Entity = grid.GetGridCell(CurrentPos.X, CurrentPos.Y).Entity;
                grid.GetGridCell(CurrentPos.X, CurrentPos.Y).Entity = null;
                CurrentPos = nextPos;
                grid.RefreshGridMat();
            }
        }
        return !isMoving;

        // Verify he arrived at the position / Wait till animation is done
    }

    private void Attacking()
    {
        Player _player = FindObjectOfType<Player>();
        if ((_player.transform.position - transform.position).magnitude == 1)
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
                if (CurrentAP >= _ability2.Cost)
                {
                    CastAbility2(_player);
                }
                else if (CurrentAP >= _ability1.Cost)
                {
                    CastAbility1(_player);
                }
            }
        }
    }

    void ChangeState(State newState)
    {
        _currentState = newState;
    }

    public override void Death()
    {
        Debug.Log(name + " Dead");
        TurnSystem turnSystyem = GameObject.FindGameObjectWithTag("TurnSystem").GetComponent<TurnSystem>();
        turnSystyem.OnEnemyDeath(this);
        // Enemy Death / Inform GameManager
        // You should remove yourself
        Cell cell = grid.GetGridCell(CurrentPos);
        cell.SetGameObjectMaterial(grid.GetDefaultGridMat());
        cell.HasEnemy = false;
        cell.Entity = null;
        Destroy(gameObject);
    }
}