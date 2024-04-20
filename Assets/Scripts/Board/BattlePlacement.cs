using System.Collections.Generic;
using UnityEngine;

public class BattlePlacement
{
    public List<(Coord, GameObject, Vector3, Coord, bool)> enemyCellList = new List<(Coord, GameObject, Vector3, Coord, bool)>();
    public List<(Coord, GameObject)> obstacleCellList = new List<(Coord, GameObject)>();
    public (Coord, GameObject) moomooCell;
    public BattlePlacement nextWave;
    public void AddEnemy(List<(Coord coord, GameObject enemyPrefabs, Vector3 rotation, Coord size, bool causeEndOfBattle)> enemyList)
    {
        foreach (var enemy in enemyList)
        {
            enemyCellList.Add(enemy);
        }
    }

    public void AddObstacle(List<(Coord coord, GameObject obstacle)> obstacleList)
    {
        foreach (var obstacle in obstacleList)
        {
            obstacleCellList.Add(obstacle);
        }
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
