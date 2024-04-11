using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    public enum FightPhase
    {
        INIT,
        PLAYERTURN,
        ENEMYTURN,
        WIN,
        LOSE
    }

    CombatGrid grid;
    private Player _player;

    private Entity _entity;
    private List<Enemy> _enemies = new List<Enemy>();

    [SerializeField] Text _playerHPText;

    bool playerTurnInitalized = false;
    bool enemyTurnInitalized = false;
    int enemyIndex = 0;
    public FightPhase CurrentState = FightPhase.INIT;

    private AlliesManager _alliesManager;

    void Start()
    {
        grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        SetUpBattle();
        Enemy[] _enemiesArray = FindObjectsOfType<Enemy>();
        _enemies.AddRange(_enemiesArray);

        _alliesManager = AlliesManager.Instance;
    }

    void Update()
    {
        StateSwitch();
    }

    private void SetUpBattle()
    {
        var maxX = grid.GetMaxX();
        var maxY = grid.GetMaxY();

        int x = 1;
        int y = -2;
        Vector3 rotation = new Vector3(0, 180, 0);
        grid.AddEnemy(new Coord(x, y), enemyPrefabs[0], rotation);

        x = 1;
        y = -1;
        grid.AddEnemy(new Coord(x, y), enemyPrefabs[1], rotation);

        x = 1;
        y = 0;
        grid.AddEnemy(new Coord(x, y), enemyPrefabs[2], rotation);

        x = 1;
        y = 1;
        grid.AddEnemy(new Coord(x, y), enemyPrefabs[3], rotation);

        x = 1;
        y = 2;
        grid.AddEnemy(new Coord(x, y), enemyPrefabs[4], rotation);

        CurrentState = FightPhase.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        _player._actualAlly = _alliesManager._actualAlly;

        if (!playerTurnInitalized)
        {
            _player.CurrentAP = _player.MaxAP.GetValue();
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
            playerTurnInitalized = false;
            enemyTurnInitalized = true;
            enemyIndex = 0;
            NextEnemyTurn();
        }
    }

    public void NextEnemyTurn()
    {
        if (enemyIndex == _enemies.Count)
        {
            CurrentState = FightPhase.PLAYERTURN;
            return;
        }

        _enemies[enemyIndex].ItsTurn = true;
        enemyIndex++;
        UpdatePlayerHPText();
    }

    private void StateSwitch()
    {
        switch (CurrentState)
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
        if (CurrentState == FightPhase.PLAYERTURN)
        {
            _player.EndOfTurn();
            CurrentState = FightPhase.ENEMYTURN;
        }
        /*if( nbEnemie <=0 && Player.health > 0 )
        {
            current_state = EnumTurn.Win
        }
        else if( Player.health <= 0)
        {
            current_state = EnumTurn.lose;
        }*/
    }

    public void UpdatePlayerHPText()
    {
        _playerHPText.text = _player.GetComponent<Entity>().CurrentHP.ToString() + " / " + _player.GetComponent<Entity>().MaxHP.GetValue().ToString();
    }

    #region Attaque


    public void OnAbility1Button()
    {
        _entity = _player.GetEnemy();
        if (CurrentState != FightPhase.PLAYERTURN)
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
            _player.CastAbility1(_entity);
            //detection porter
        }
    }

    public void OnAbility2Button()
    {
        _entity = _player.GetEnemy();
        if (CurrentState != FightPhase.PLAYERTURN || _entity == null)
        {
            return;
        }
        else
        {
            _player.CastAbility2(_entity);
            //detection porter
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
            _player.CastFriendAbility1();
            Debug.Log("Friend Ability 1");
        }
    }

    public void OnFriend2Button()
    {
        if (CurrentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else
        {
            _player.CastFriendAbility2();
            Debug.Log("Friend Ability 2");
        }
    }

        public void OnFriend3Button()
    {
        if (CurrentState != FightPhase.PLAYERTURN)
        {
            return;
        }
        else
        {
            _player.CastFriendAbility3();
            Debug.Log("Friend Ability 3");
        }
    }
#endregion
}
