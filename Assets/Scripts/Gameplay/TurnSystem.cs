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
    CombatGrid grid;
    private Entity entity;
    private List<Enemy> enemies = new List<Enemy>();

    [SerializeField] Text playerHPText;
    [SerializeField] Text turnText;
    [SerializeField] Animator animator;

    bool playerTurnInitalized = false;
    bool enemyTurnInitalized = false;
    bool battleFullyEnded = false;
    int enemyIndex = 0;
    public FightPhase currentState = FightPhase.INIT;

    private AlliesManager _alliesManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
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

    public void AddMoomoo(Player moomoo)
    {
        player = moomoo;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    private void SetUpBattle()
    {
        player.CheckAP();
        currentState = FightPhase.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        if (!playerTurnInitalized)
        {
            turnText.text = "Tour du joueur";
            player.CurrentAP = player.MaxAP.GetValue();
            player.CheckAP();
            player.StartOfTurn();
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
    }

    private void StateSwitch()
    {
        switch (currentState)
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
        IsWin.IsWinBool = true;
        currentState = FightPhase.END;
    }

    private void Lose()
    {
        IsWin.IsWinBool = false;
        currentState = FightPhase.END;
    }

    private void End()
    {
        AnimationScripts.currentScene = AnimationScripts.Scenes.Battle;
        if (!IsWin.IsWinBool || grid.battleSceneActions.nextBattlePlacement.nextWave == null)
        {
            battleFullyEnded = true;
            AnimationScripts.nextScene = AnimationScripts.Scenes.End;
            animator.SetTrigger("StartFadeIn");
        }
        else
        {
            currentState = FightPhase.INIT;
            // if end scene isn't loaded then a next wave must be placed
            grid.battleSceneActions.nextBattlePlacement = grid.battleSceneActions.nextBattlePlacement.nextWave;
            // Start Mask
            AnimationScripts.nextScene = AnimationScripts.Scenes.Battle;
            animator.SetTrigger("StartFadeIn");
        }
    }

    public void OnFadeInFinish()
    {
        if (!battleFullyEnded)
        {
            Destroy(player.gameObject);
            foreach (var tempEnemy in enemies)
            {
                foreach (Cell cell in tempEnemy.occupiedCells)
                {
                    cell.SetGameObjectMaterial(grid.GetDefaultGridMat());
                    cell.HasEnemy = false;
                    cell.Entity = null;
                }
                Destroy(tempEnemy.gameObject);
            }
            enemies.Clear();
            grid.DestroyGrid();
            grid.SetupGrid();
            SetUpBattle();
            animator.SetTrigger("StartFadeOut");
        }
        else
        {
            SceneManager.LoadScene("End");
        }
    }

    public void OnMoveButton()
    {
        player.Move();
    }

    public void OnEndTurnButton()
    {
        if (currentState == FightPhase.PLAYERTURN)
        {
            player.EndOfTurn();
            currentState = FightPhase.ENEMYTURN;
        }
    }

    public void OnPlayerDeath()
    {
        currentState = FightPhase.LOSE;
    }
    public void OnEnemyDeath(Enemy enemy, bool EndBattle)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            currentState = FightPhase.WIN;
        }
        if (EndBattle)
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
        }
    }

    public void OnAbility2Button()
    {
        entity = player.GetEnemy();
        if (currentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else if (entity == null)
        {
            Debug.Log("No enemy select");
            return;
        }
        else
        {
            player.CastAbility2(entity);
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
