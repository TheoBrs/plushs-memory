
using UnityEngine;
using UnityEngine.Events;

 public enum EnumTurn 
{
    INIT,
    PLAYERTURN,
    ENEMIETURN,
    WIN,
    LOSE
}

 public class TurnSysteme : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemie;

    public UnityEvent EndTurn;  

    public EnumTurn current_state = EnumTurn.INIT;
    // Start is called before the first frame update
    void Start()
    {
         SetUpBattle();
    }

    void Update()
    {
        StateSwitch();
    }

    private void SetUpBattle()
    {
        Instantiate(Player);
        //Instantiate(Enemie); replace by liste
        current_state = EnumTurn.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        Debug.Log("TurnPlayer");
        OnEndTurnButton();
    }

    public void EnemieTurn()
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
        current_state = EnumTurn.PLAYERTURN;
    }

    private void StateSwitch()
    {
        
        switch(current_state)
        {
            case EnumTurn.PLAYERTURN:
            PlayerTurn();
            break;

            case EnumTurn.ENEMIETURN:
            EnemieTurn();
            break;

            case EnumTurn.WIN:
            Win();
            break;
            
            case EnumTurn.LOSE:
            Lose();
            break;

            default:
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
        if (current_state!= EnumTurn.PLAYERTURN)
        {
            return;
        }
/*      if( nbEnemie <=0 && Player.health > 0 )
        {
            current_state = EnumTurn.Win
        }
        else if( Player.health <= 0)
        {
            current_state = EnumTurn.lose;
        }*/
        else
        {
            current_state = EnumTurn.ENEMIETURN;
        }
    }
}
