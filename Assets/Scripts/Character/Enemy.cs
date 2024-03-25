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
    private bool _itsTurn = false;

    protected Entity _playerReference;

    protected override void Start()
    {
        base.Start();
        _currentState = State.WaitForTurn;
    }

    protected virtual void Update()
    {
        switch (_currentState)
        {
            case State.WaitForTurn:
                if (_itsTurn)
                {
                    ChangeState(State.Movement);
                }
                break;
            case State.Movement:

                Movement();

                ChangeState(State.Attacking);
                break;
            case State.Attacking:

                Attacking();

                ChangeState(State.EndTurn);
                break;
            case State.EndTurn:

                // Send message to GameManager to tell turn is done

                ChangeState(State.WaitForTurn);
                break;
        }
    }

    private void Movement()
    {
        // Calculate nearest path to player with _currentAP (1 AP = 1 case)

        int numberOfCase = 0;

        _currentAP -= numberOfCase;

        // Move to the position

        // Verify he arrived at the position
    }

    private void Attacking()
    {
        // Check if the player is reachable


        if(_currentAP > 0)
        {
            if(_currentAP >= _ability2.Cost)
            {
                CastAbility2(_playerReference);
            }
            else if (_currentAP >= _ability1.Cost)
            {
                CastAbility1(_playerReference);
            }

            // Verify Attack is done

        }
    }

    void ChangeState(State newState)
    {
        _currentState = newState;
    }

    public override void Death()
    {
        // Enemy Death / Check if player wins in GameManager
    }
}