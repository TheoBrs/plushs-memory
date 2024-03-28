using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
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

    private int Competense = 0;

    private Entity _entity;

    public FightPhase CurrentState = FightPhase.INIT;

    void Start()
    {
        grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        SetUpBattle();
    }

    void Update()
    {
        StateSwitch();
    }

    private void SetUpBattle()
    {
        int x = -1;
        int y = -1;
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, 0.01f, y), quaternion.identity);
        enemy.AddComponent<Enemy>();
        enemy.GetComponent<Enemy>().name = "sus";

        grid.AddEnemy(new Coord(x + grid.GetMaxX() / 2, y + grid.GetMaxY() / 2), enemy.GetComponent<Enemy>()); 
        CurrentState = FightPhase.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        Debug.Log("TurnPlayer");
    }
 
    public void EnemyTurn()
    {
        //Debug.Log("TurnEnemey");
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

        Entity entity = _player.GetEnemy();
        if (CurrentState != FightPhase.PLAYERTURN || entity == null)
        {
            return;
        }
        else
        {
            _player.DebugEnemyStr();
            _player.CastAbility2(_entity);
            //detection porter
        }
    }

        public void OnRangeButton()
        {
        Entity entity = _player.GetEnemy();
        if (CurrentState != FightPhase.PLAYERTURN || entity == null)
        {
            Debug.Log("no enemy select");
            return;
        }
        else
        {
            _player.DebugEnemyStr();
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

    public void OnFiend2Button()
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

        public void OnFiend3Button()
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
