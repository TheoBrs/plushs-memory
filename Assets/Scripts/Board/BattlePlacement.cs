using System.Collections.Generic;
using UnityEngine;

public class BattlePlacement
{
    public List<(Coord, GameObject, Vector3, Coord, bool)> enemyCellList = new List<(Coord, GameObject, Vector3, Coord, bool)>();
    public List<(Coord, GameObject)> obstacleCellList = new List<(Coord, GameObject)>();
    public (Coord, GameObject) moomooCell;
    public BattlePlacement nextWave;
    public void AddEnemy(Coord coord, GameObject enemyPrefabs, Vector3 rotation, Coord size, bool causeEndOfBattle = false)
    {
        enemyCellList.Add((coord, enemyPrefabs, rotation, size, causeEndOfBattle));
    }

    public void AddObstacle(Coord coord, GameObject obstacle)
    {
        obstacleCellList.Add((coord, obstacle));
    }

    public void AddMoomoo((Coord coord, GameObject moomooPrefab) tuple)
    {
        moomooCell = (tuple.coord, tuple.moomooPrefab);
    }

    public void ClearBattlePlacement()
    {
        enemyCellList.Clear();
        obstacleCellList.Clear();
        nextWave = null;
    }
}
