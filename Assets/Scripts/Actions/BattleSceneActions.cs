using System.Collections.Generic;
using UnityEngine;

public class BattleSceneActions : MonoBehaviour
{
    public BattlePlacement nextBattlePlacement = new BattlePlacement();
    public BattlePlacement originalPlacement = new BattlePlacement();
    [SerializeField] Animator animator;
    [SerializeField] GameObject mitePrefab;
    [SerializeField] GameObject coleoPrefab;
    [SerializeField] GameObject sourisPrefab;
    [SerializeField] GameObject moomooPrefab;

    List<(Coord, GameObject, Vector3, Coord, bool)> enemyList = new List<(Coord coord, GameObject prefab, Vector3 rotation, Coord size, bool causeEndOfBattle)>();
    List<(Coord, GameObject)> obstacleList = new List<(Coord coord, GameObject prefab)>();

    private void Start()
    {
        animator.SetTrigger("StartFadeOut");
    }

    void SetupAnimation()
    {
        AnimationScripts.currentScene = AnimationScripts.Scenes.Menu;
        AnimationScripts.nextScene = AnimationScripts.Scenes.Battle;
        animator.SetTrigger("StartFadeIn");
    }

    public void SetupChapter1()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        BattlePlacement wave = nextBattlePlacement;
        wave.ClearBattlePlacement();

        enemyList.Clear();
        enemyList.Add((new Coord(2, 1), mitePrefab, rotation, new Coord(1, 1), true));
        enemyList.Add((new Coord(2, -1), mitePrefab, rotation, new Coord(1, 1), true));
        wave.AddEnemy(enemyList);
        wave.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        wave.nextWave = new BattlePlacement();
        wave = wave.nextWave;
        enemyList.Clear();
        enemyList.Add((new Coord(2, 1), mitePrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(2, -1), mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(enemyList);
        wave.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        wave.nextWave = new BattlePlacement();
        wave = wave.nextWave;
        enemyList.Clear();
        enemyList.Add((new Coord(2, 2), mitePrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(2, 0), mitePrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(2, -2), mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(enemyList);
        wave.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        originalPlacement = nextBattlePlacement;
    }
    public void SetupChapter2()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        BattlePlacement wave = nextBattlePlacement;
        wave.ClearBattlePlacement();

        enemyList.Clear();
        enemyList.Add((new Coord(2, 2), mitePrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(2, 0), coleoPrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(2, -2), mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(enemyList);
        wave.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        wave.nextWave = new BattlePlacement();
        wave = wave.nextWave;
        enemyList.Clear();
        enemyList.Add((new Coord(2, 2), mitePrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(2, 1), coleoPrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(2, -1), coleoPrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(2, -2), mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(enemyList);
        wave.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        originalPlacement = nextBattlePlacement;
    }
    public void SetupChapter3()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        BattlePlacement wave = .nextBattlePlacement;
        wave.ClearBattlePlacement();

        enemyList.Clear();
        enemyList.Add((new Coord(1, 2), mitePrefab, rotation, new Coord(1, 1), false));
        enemyList.Add((new Coord(1, 0), sourisPrefab, rotation, new Coord(2, 2), true));
        enemyList.Add((new Coord(1, -1), mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(enemyList);
        wave.AddMoomoo((new Coord(-2, 0), moomooPrefab));

        originalPlacement = nextBattlePlacement;
    }
}
