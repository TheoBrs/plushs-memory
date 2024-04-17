using System.Collections.Generic;
using UnityEngine;

public class BattleActions : MonoBehaviour
{
    BattleManager Instance;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] GameObject moomooPrefab;

    private void Start()
    {
        Instance = BattleManager.Instance;
    }
    public void SetupBattle1()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        Instance.nextBattlePlacement.ClearBattlePlacement();
        Instance.nextBattlePlacement.AddEnemy(new Coord(0, 0), enemyPrefabs[0], rotation, new Coord(1, 1));
        Instance.nextBattlePlacement.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        Instance.nextBattlePlacement.nextWave = new BattlePlacement();
        Instance.nextBattlePlacement.nextWave.AddEnemy(new Coord(2, 0), enemyPrefabs[0], rotation, new Coord(1, 1));
        Instance.nextBattlePlacement.nextWave.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        Instance.originalPlacement = Instance.nextBattlePlacement;
    }
    public void SetupBossBattle1()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        Instance.nextBattlePlacement.ClearBattlePlacement();
        Instance.nextBattlePlacement.AddEnemy(new Coord(1, 0), enemyPrefabs[1], rotation, new Coord(2, 2));
        Instance.nextBattlePlacement.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        Instance.originalPlacement = Instance.nextBattlePlacement;
    }
}
