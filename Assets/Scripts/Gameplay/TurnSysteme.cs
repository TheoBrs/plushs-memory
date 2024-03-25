using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


 public enum EnumTurn 
{
    init,
    PlayerTurn,
    EnemieTurn,
    Win,
    lose
}

 public class TurnSysteme : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemie;



    public EnumTurn current_state;
    // Start is called before the first frame update
    void Start()
    {
         current_state = EnumTurn.init;
         SetUpBattel();
    }

    IEnumerator SetUpBattel()
    {
        Instantiate(Player);
        Instantiate(Enemie);

        yield return new WaitForSeconds(2f);
        current_state = EnumTurn.EnemieTurn;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        Debug.Log("TurnPlayer");
    }

    public void EnemieTurn()
    {
        Debug.Log("TurnEnemey");
        current_state = EnumTurn.PlayerTurn;
/*      if( nbEnemie <=0 && Player.health > 0 )
        {
            current_state = EnumTurn.Win
        }

        else if( Player.health <= 0)
        {
            current_state = EnumTurn.lose;
        }*/
    }


    public void OnEndTurnButton()
    {
        if (current_state!= EnumTurn.PlayerTurn)
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
            current_state = EnumTurn.EnemieTurn;
        }
    }
}
