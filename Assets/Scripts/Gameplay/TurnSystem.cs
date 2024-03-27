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
    private Player player;

    public FightPhase CurrentState = FightPhase.INIT;

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

        switch (CurrentState)
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
        if (CurrentState != FightPhase.PLAYERTURN)
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

#region Attaque

    public void OnCACButton()
    {
        if (CurrentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else
        {
            //player.CastAbility1();
            //detection porter
            Debug.Log("CAC");
        }
    }

        public void OnRangeButton()
    {
        if (CurrentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else
        {
            //player.CastAbility2();
            //detection porter
            Debug.Log("Range");
        }
    }

        public void OnFriend1Button()
    {
        if (CurrentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else
        {
            player.CastFriendAbility1();
            //detection porter
            Debug.Log("Friend Ability 1");
        }
    }

    public void OnFiend2Button()
    {
        if (CurrentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else
        {
            player.CastFriendAbility2();
            //detection porter
            Debug.Log("Friend Ability 2");
        }
    }

        public void OnFiend3Button()
    {
        if (CurrentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else
        {
            player.CastFriendAbility3();
            //detection porter
            Debug.Log("Friend Ability 3");
        }
    }
#endregion
}
