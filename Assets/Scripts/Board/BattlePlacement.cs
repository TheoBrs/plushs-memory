using System.Collections.Generic;
using UnityEngine;

public class BattlePlacement
{
    public List<(Coord, GameObject, Vector3, Coord)> enemyCellList = new List<(Coord, GameObject, Vector3, Coord)>();
    public List<(Coord, GameObject)> obstacleCellList = new List<(Coord, GameObject)>();
    public Coord moomooCell;
    public BattlePlacement nextWave;
    public void AddEnemy(Coord coord, GameObject enemyPrefabs, Vector3 rotation, Coord size)
    {
        enemyCellList.Add((coord, enemyPrefabs, rotation, size));
    }

    public void AddObstacle(Coord coord, GameObject obstacle)
    {
        obstacleCellList.Add((coord, obstacle));
    }

    public void AddMoomoo(Coord coord)
    {
        moomooCell = coord;
    }

    public void ClearBattlePlacement()
    {
        enemyCellList.Clear();
        obstacleCellList.Clear();
        nextWave = null;
    }
}
