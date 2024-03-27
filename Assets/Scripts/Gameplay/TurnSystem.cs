using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public enum FightPhase
    {
        INIT,
        PLAYERTURN,
        ENEMYTURN,
        WIN,
        LOSE
    }

    private GameObject Player;
    private GameObject Enemy;

    private UnityEvent EndTurn;
    public FightPhase CurrentState = FightPhase.INIT;
   
    void Start()
    {
         SetUpBattle();
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(OnEndTurnButton);
    }

    void Update()
    {
        StateSwitch();

    }

    private void SetUpBattle()
    {
        //Instantiate(Player);
       // Instantiate(Enemie); //replace by liste
        CurrentState = FightPhase.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        Debug.Log("TurnPlayer");
    }

    public void EnemyTurn()
    {
        Debug.Log("TurnEnemey");
/*      if( nbEnemie <=0 && Player.health > 0 )
        {
            current_state = EnumTurn.Win
        }

        else if( Player.health <= 0)
        {
            current_state = EnumTurn.lose;
        }*/
        CurrentState = FightPhase.PLAYERTURN;
    }

    private void StateSwitch()
    {
        
        switch(CurrentState)
        {
            case FightPhase.PLAYERTURN:
            PlayerTurn();
            break;

            case FightPhase.ENEMYTURN:
            EnemyTurn();
            break;

            case FightPhase.WIN:
            Win();
            break;
            
            case FightPhase.LOSE:
            Lose();
            break;

            default:
            CurrentState = FightPhase.INIT;
            break;
        }
    }


    private void Win()
    {
        Debug.Log("Win");
    }

    private void Lose()
    {
        Debug.Log("lose");
    }

    public void OnEndTurnButton()
    {
        if (CurrentState!= FightPhase.PLAYERTURN)
        {
            return;
        }
        /*if( nbEnemie <=0 && Player.health > 0 )
        {
            current_state = EnumTurn.Win
        }
        else if( Player.health <= 0)
        {
            current_state = EnumTurn.lose;
        }*/
        else
        {
            CurrentState = FightPhase.ENEMYTURN;
        }
    }
}
