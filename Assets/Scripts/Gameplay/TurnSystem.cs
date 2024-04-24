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
        END,
        STOP
    }

    [SerializeField] Text _playerHPText;
    [SerializeField] Text _turnText;
    public Animator animator;
    [HideInInspector] public FightPhase currentState = FightPhase.INIT;


    private Player _player;
    private CombatGrid _grid;
    private Entity _entity;
    private List<Enemy> _enemies = new List<Enemy>();
    private int _chapterIndex;
    private bool _playerTurnInitalized = false;
    private bool _enemyTurnInitalized = false;
    private bool _battleFullyEnded = false;
    private int _enemyIndex = 0;
    private AlliesManager _alliesManager;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
        SetUpBattle();
        _alliesManager = AlliesManager.Instance;

        if (_alliesManager)
        {
            _player.currentAlly = _alliesManager._actualAlly;
            _player.SetupAllyPassives();
        }
    }

    void Update()
    {
        StateSwitch();
    }

    public void AddMoomoo(Player moomoo)
    {
        _player = moomoo;
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    private void SetUpBattle()
    {
        _player.IsTurn = true;
        _player.CheckAP(false);
        currentState = FightPhase.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        if (!_playerTurnInitalized)
        {
            _turnText.text = "Tour du joueur";
            _player.CurrentAP = _player.MaxAP.GetValue();
            _player.IsTurn = true;
            _player.CheckAP(false);
            _player.StartOfTurn();
            _playerTurnInitalized = true;
            _enemyTurnInitalized = false;
        }
        //Debug.Log("TurnPlayer");
    }
 
    public void EnemyTurn()
    {
        if (!_enemyTurnInitalized)
        {
            // Stuff to do only once at the start of the enemies turn
            _turnText.text = "Tour des ennemis";
            _playerTurnInitalized = false;
            _enemyTurnInitalized = true;
            _enemyIndex = 0;
            NextEnemyTurn();
        }
    }

    public void NextEnemyTurn()
    {
        if (_enemyIndex == _enemies.Count)
        {
            if (currentState == FightPhase.ENEMYTURN)
                currentState = FightPhase.PLAYERTURN;
            return;
        }

        _enemies[_enemyIndex].IsTurn = true;
        _enemyIndex++;
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

            case FightPhase.STOP:
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
        if (!IsWin.IsWinBool || _grid.battleSceneActions.nextBattlePlacement.nextWave == null)
        {
            // If you lose or there is no next wave
            _battleFullyEnded = true;
            AnimationScripts.nextScene = AnimationScripts.Scenes.End;
            currentState = FightPhase.STOP;
            if (IsWin.IsWinBool)
            {
                switch (_grid.dialogueIndex)
                {
                    case 4:
                    case 8:
                    case 13:
                        _grid.RunDialogue();
                        break;

                    default:
                        break;
                }
            }
            else
                animator.SetTrigger("StartFadeIn");
        }
        else // If we're going to next wave
        {
            animator.SetTrigger("StartFadeIn");
            _grid.battleSceneActions.nextBattlePlacement = _grid.battleSceneActions.nextBattlePlacement.nextWave;
            // Start Mask
            AnimationScripts.nextScene = AnimationScripts.Scenes.Battle;
            currentState = FightPhase.INIT;
        }
    }

    public void OnFadeInFinish()
    {
        if (_battleFullyEnded)
        {
            if (IsWin.IsWinBool)
            {
                if (StatisticsManager.Instance)
                {
                    if (SceneManager.GetActiveScene().name == "Chapter1")
                        GameManager.Instance.Progression = 2;
                    if (SceneManager.GetActiveScene().name == "Chapter2")
                        GameManager.Instance.Progression = 3;
                    if (SceneManager.GetActiveScene().name == "Chapter3")
                        GameManager.Instance.Progression = 4;
                }
            }
            else
                EndMenuActions.lastBattleChapter = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene("End");
        }
        else
        {
            switch (_grid.dialogueIndex)
            {
                case 3:
                    _grid.RunDialogue();
                    break;

                default:
                    break;
            }
            Destroy(_player.gameObject);
            foreach (var tempEnemy in _enemies)
            {
                foreach (Cell cell in tempEnemy.occupiedCells)
                {
                    cell.SetGameObjectMaterial(_grid.GetDefaultGridMat());
                    cell.HasEnemy = false;
                    cell.Entity = null;
                }
                Destroy(tempEnemy.gameObject);
            }
            _enemies.Clear();
            _grid.DestroyGrid();
            _grid.SetupGrid();
            SetUpBattle();
            animator.SetTrigger("StartFadeOut");
        }
    }
    public void OnMoveButton()
    {
        _player.Move();
    }

    public void OnEndTurnButton()
    {
        if (currentState == FightPhase.PLAYERTURN)
        {
            _player.EndOfTurn();
            currentState = FightPhase.ENEMYTURN;
        }
    }

    public void OnPlayerDeath()
    {
        currentState = FightPhase.LOSE;
    }
    public void OnEnemyDeath(Enemy enemy, bool EndBattle)
    {
        _player.entity = null;
        _player.CheckEntity();
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
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
        _entity = _player.GetEnemy();
        if (currentState != FightPhase.PLAYERTURN)
        {
            Debug.Log("Not the player turn");
            return;
        }
        else if (_entity == null)
        {
            Debug.Log("No enemy select");
            return;
        }
        else
        {
            _player.isAttacking = true;
            _player.CastAbility1(_entity);
        }
    }

    public void OnAbility2Button()
    {
        _entity = _player.GetEnemy();
        if (currentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else if (_entity == null)
        {
            Debug.Log("No enemy select");
            return;
        }
        else
        {
            _player.isAttacking = true;
            _player.CastAbility2(_entity);
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
            _player.FriendAbilityButton();
            Debug.Log("Friend Ability");
        }
    }
#endregion
}
