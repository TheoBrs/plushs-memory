using System.Collections.Generic;
using UnityEngine;

public class BattleSceneActions : MonoBehaviour
{
    public BattlePlacement nextBattlePlacement = new BattlePlacement();
    public BattlePlacement originalPlacement = new BattlePlacement();
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _mitePrefab;
    [SerializeField] private GameObject _coleoPrefab;
    [SerializeField] private GameObject _sourisPrefab;
    [SerializeField] private GameObject _moomooPrefab;

    List<(Coord, GameObject, Vector3, Coord, bool)> _enemyList = new List<(Coord coord, GameObject prefab, Vector3 rotation, Coord size, bool causeEndOfBattle)>();
    List<(Coord, GameObject)> _obstacleList = new List<(Coord coord, GameObject prefab)>();

    void SetupAnimation()
    {
        AnimationScripts.currentScene = AnimationScripts.Scenes.Menu;
        AnimationScripts.nextScene = AnimationScripts.Scenes.Battle;
        _animator.SetTrigger("StartFadeIn");
    }

    public void SetupChapter1()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        BattlePlacement wave = nextBattlePlacement;
        wave.ClearBattlePlacement();

        _enemyList.Clear();
        _enemyList.Add((new Coord(2, 1), _mitePrefab, rotation, new Coord(1, 1), true));
        _enemyList.Add((new Coord(2, -1), _mitePrefab, rotation, new Coord(1, 1), true));
        wave.AddEnemy(_enemyList);

        wave.AddMoomoo((new Coord(-2, 0), _moomooPrefab, 0));

        wave.nextWave = new BattlePlacement();
        wave = wave.nextWave;
        _enemyList.Clear();
        _enemyList.Add((new Coord(2, 1), _mitePrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(2, -1), _mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(_enemyList);
        wave.AddMoomoo((new Coord(-2, 0), _moomooPrefab, 1));

        wave.nextWave = new BattlePlacement();
        wave = wave.nextWave;
        _enemyList.Clear();
        _enemyList.Add((new Coord(2, 2), _mitePrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(2, 0), _mitePrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(2, -2), _mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(_enemyList);
        wave.AddMoomoo((new Coord(-2, 0), _moomooPrefab, 1));

        originalPlacement = nextBattlePlacement;
    }
    public void SetupChapter2()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        BattlePlacement wave = nextBattlePlacement;
        wave.ClearBattlePlacement();

        _enemyList.Clear();
        _enemyList.Add((new Coord(2, 2), _mitePrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(2, 0), _coleoPrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(2, -2), _mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(_enemyList);
        wave.AddMoomoo((new Coord(-2, 0), _moomooPrefab, 1));

        wave.nextWave = new BattlePlacement();
        wave = wave.nextWave;
        _enemyList.Clear();
        _enemyList.Add((new Coord(2, 2), _mitePrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(2, 1), _coleoPrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(2, -1), _coleoPrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(2, -2), _mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(_enemyList);
        wave.AddMoomoo((new Coord(-2, 0), _moomooPrefab, 1));

        originalPlacement = nextBattlePlacement;
    }
    public void SetupChapter3()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        BattlePlacement wave = nextBattlePlacement;
        wave.ClearBattlePlacement();

        _enemyList.Clear();
        _enemyList.Add((new Coord(1, 2), _mitePrefab, rotation, new Coord(1, 1), false));
        _enemyList.Add((new Coord(1, 0), _sourisPrefab, rotation, new Coord(2, 2), true));
        _enemyList.Add((new Coord(1, -1), _mitePrefab, rotation, new Coord(1, 1), false));
        wave.AddEnemy(_enemyList);
        wave.AddMoomoo((new Coord(-2, 0), _moomooPrefab, 1));

        originalPlacement = nextBattlePlacement;
    }
}
