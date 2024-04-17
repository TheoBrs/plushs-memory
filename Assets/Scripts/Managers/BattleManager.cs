using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public BattlePlacement nextBattlePlacement = new BattlePlacement();
    [SerializeField] List<GameObject> enemyPrefabs;

    #region Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void SetupBattle1()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        Instance.nextBattlePlacement.ClearBattlePlacement();
        Instance.nextBattlePlacement.AddEnemy(new Coord(0, 0), enemyPrefabs[0], rotation, new Coord(1, 1));
    }
    public void SetupBossBattle1()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        Instance.nextBattlePlacement.ClearBattlePlacement();
        Instance.nextBattlePlacement.AddEnemy(new Coord(1, 0), enemyPrefabs[1], rotation, new Coord(2, 2));
    }
}
