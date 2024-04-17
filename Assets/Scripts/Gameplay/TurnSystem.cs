using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public enum FightPhase
    {
        INIT,
        PLAYERTURN,
        ENEMYTURN,
        WIN,
        LOSE,
        END
    }

    private Player player;

    private Entity entity;
    private List<Enemy> enemies = new List<Enemy>();

    [SerializeField] Text playerHPText;
    [SerializeField] Text turnText;

    bool playerTurnInitalized = false;
    bool enemyTurnInitalized = false;
    int enemyIndex = 0;
    public FightPhase currentState = FightPhase.INIT;

    private AlliesManager _alliesManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        SetUpBattle();
        _alliesManager = AlliesManager.Instance;

        if (_alliesManager)
        {
            player._currentAlly = _alliesManager._actualAlly;
            player.SetupAllyPassives();
        }
    }

    void Update()
    {
        StateSwitch();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    private void SetUpBattle()
    {
        currentState = FightPhase.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        if (!playerTurnInitalized)
        {
            turnText.text = "Tour du joueur";
            player.CurrentAP = player.MaxAP.GetValue();
            player.CheckAP();
            playerTurnInitalized = true;
            enemyTurnInitalized = false;
        }
        //Debug.Log("TurnPlayer");
    }
 
    public void EnemyTurn()
    {
        if (!enemyTurnInitalized)
        {
            // Stuff to do only once at the start of the enemies turn
            turnText.text = "Tour des ennemis";
            playerTurnInitalized = false;
            enemyTurnInitalized = true;
            enemyIndex = 0;
            NextEnemyTurn();
        }
    }

    public void NextEnemyTurn()
    {
        if (enemyIndex == enemies.Count)
        {
            if (currentState == FightPhase.ENEMYTURN)
                currentState = FightPhase.PLAYERTURN;
            return;
        }

        enemies[enemyIndex].ItsTurn = true;
        enemyIndex++;
        UpdatePlayerHPText();
    }

    private void StateSwitch()
    {
        switch (currentState)
        {
            case FightPhase.PLAYERTURN:
                UpdatePlayerHPText();
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

            case FightPhase.END:
                End();
                break;

            default:
                currentState = FightPhase.INIT;
                break;
        }
    }

    private void Win()
    {
        Debug.Log("Win");
        IsWin.IsWinBool = true;
        currentState = FightPhase.END;
    }

    private void Lose()
    {
        Debug.Log("Lose");
        IsWin.IsWinBool = false;
        currentState = FightPhase.END;
    }

    private void End()
    {
        Debug.Log("End");
        SceneManager.LoadScene("End");
        // Call animation to exit battleScene or something
    }

    public void OnEndTurnButton()
    {
        if (currentState == FightPhase.PLAYERTURN)
        {
            player.EndOfTurn();
            currentState = FightPhase.ENEMYTURN;
        }
    }

    public void UpdatePlayerHPText()
    {
        //playerHPText.text = "HP : " + player.GetComponent<Entity>().CurrentHP.ToString() + " / " + player.GetComponent<Entity>().MaxHP.GetValue().ToString();
    }

    public void OnPlayerDeath()
    {
        currentState = FightPhase.LOSE;
    }
    public void OnEnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0 )
        {
            currentState = FightPhase.WIN;
        }
    }
    #region Attaque


    public void OnAbility1Button()
    {
        entity = player.GetEnemy();
        if (currentState != FightPhase.PLAYERTURN)
        {
            Debug.Log("Not the player turn");
            return;
        }
        else if (entity == null)
        {
            Debug.Log("No enemy select");
            return;
        }
        else
        {
            player.CastAbility1(entity);
            //detection porter
        }
    }

    public void OnAbility2Button()
    {
        entity = player.GetEnemy();
        if (currentState != FightPhase.PLAYERTURN || entity == null)
        {
            return;
        }
        else
        {
            player.CastAbility2(entity);
            //detection porter
        }
    }

    public void OnFriendButton()
    {
        if (currentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else
        {
            player.FriendAbilityButton();
            Debug.Log("Friend Ability");
        }
    }
#endregion
}
