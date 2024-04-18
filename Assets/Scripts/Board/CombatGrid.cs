using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CombatGrid : MonoBehaviour
{
    [SerializeField] int maxX;
    [SerializeField] int maxY;
    public float gridCellScale = 1;

    [SerializeField] Material defaultGridMat;
    [SerializeField] Material notWalkableGridMat;
    [SerializeField] Material enemyGridMat;
    [SerializeField] Material selectedEnemyGridMat;
    [SerializeField] Material selectedGridMat;
    [SerializeField] Material pathGridMat;
    [SerializeField] Material redPathGridMat;

    Cell[,] elements;
    [SerializeField] GameObject gridPrefab;
    BattleManager battleManager;
    TurnSystem turnSystem;
    Player _player;

    //creation de la grille de Combat
    void Awake()
    {
        battleManager = BattleManager.Instance;
        turnSystem = GameObject.FindWithTag("TurnSystem").GetComponent<TurnSystem>();

        SetupGrid();
    }

    public void SetupGrid()
    {
        elements = new Cell[maxX, maxY];
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                Coord coords = Coord.ToWorldCoord(x, y, maxX, maxY);
                GameObject newCell = Instantiate(gridPrefab, new Vector3(coords.X * gridCellScale, 0.01f, coords.Y * gridCellScale), Quaternion.identity);
                newCell.transform.localScale *= gridCellScale;
                newCell.tag = "GridCell";
                Cell gridElement = new Cell { Coord = coords, GameObject = newCell };
                gridElement.GameObject.transform.parent = gameObject.transform;
                elements[x, y] = gridElement;
            }
        }

        AddMoomoo(battleManager.nextBattlePlacement.moomooCell.Item1, battleManager.nextBattlePlacement.moomooCell.Item2);

        foreach (var tuple in battleManager.nextBattlePlacement.enemyCellList)
        {
            AddEnemy(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }

        foreach (var tuple in battleManager.nextBattlePlacement.obstacleCellList)
        {
            AddObstacle(tuple.Item1, tuple.Item2);
        }
    }

    public void DestroyGrid()
    {
        foreach (Cell cell in elements)
        {
            Destroy(cell.GameObject);
        }
        elements = null;
    }
    public void RefreshGridMat()
    {
        foreach (var cell in elements)
        {
            if (cell.HasObstacle)
                cell.SetGameObjectMaterial(GetNotWalkableGridMat());
            else if (cell.HasEnemy)
                if (cell.IsSelected)
                    cell.SetGameObjectMaterial(GetSelectedEnemyGridMat());
                else
                    cell.SetGameObjectMaterial(GetEnemyGridMat());
            else
                cell.SetGameObjectMaterial(GetDefaultGridMat());
        }
    }
    public bool AddObstacle(Coord coord, GameObject obstacle)
    {
        int x = coord.X;
        int y = coord.Y;
        if (elements[x, y].HasObstacle)
            return false;

        elements[x, y].HasObstacle = true;
        elements[x, y].GameObject = obstacle;
        elements[x, y].SetGameObjectMaterial(notWalkableGridMat);
        return true;
    }

    public void AddMoomoo(Coord coord, GameObject moomooPrefabs) 
    {
        Vector3 rotation = new Vector3(0, 0, 0);
        Player moomoo = Instantiate(moomooPrefabs, new Vector3(coord.X * gridCellScale, 0.01f, coord.Y * gridCellScale) + transform.position, Quaternion.Euler(rotation)).GetComponent<Player>();
        moomoo.CurrentPos = coord;
        moomoo.transform.position = new Vector3(coord.X * gridCellScale, 0.01f, coord.Y * gridCellScale);
        moomoo.speed = 2;
        _player = moomoo;
        turnSystem.AddMoomoo(_player);
    }

    public bool AddEnemy(Coord coord, GameObject enemyPrefabs, Vector3 rotation, Coord size, bool causeEndOfBattle = false, bool canPlayAfterSpawn = true)
    {
        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                int x = Coord.ToListCoord(coord, maxX, maxY).X + i;
                int y = Coord.ToListCoord(coord, maxX, maxY).Y + j;

                if (elements[x, y].HasObstacle || elements[x, y].HasEnemy || _player.CurrentPos.Equals(coord))
                    return false;
            }
        }

        GameObject enemy = Instantiate(enemyPrefabs, new Vector3(coord.X * gridCellScale + (size.X - 1) * gridCellScale / 2, 0.01f, coord.Y * gridCellScale + (size.Y - 1) * gridCellScale / 2) + transform.position, Quaternion.Euler(rotation));

        enemy.transform.localScale *= Mathf.Sqrt(size.X * size.Y);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.CurrentPos = coord;
        enemyScript.justSpawned = !canPlayAfterSpawn;
        enemyScript.speed = 2;
        enemyScript.Size = size;
        enemyScript.causeEndOfBattle = causeEndOfBattle;
        turnSystem.AddEnemy(enemyScript);

        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                int x = Coord.ToListCoord(coord, maxX, maxY).X + i;
                int y = Coord.ToListCoord(coord, maxX, maxY).Y + j;

                elements[x, y].HasEnemy = true;
                elements[x, y].Entity = enemyScript;
                elements[x, y].SetGameObjectMaterial(enemyGridMat);
                enemyScript.occupiedCells.Add(elements[x, y]);
            }
        }
        return true;
    }

    public Cell GetGridCell(int x, int y)
    {
        Coord coord = Coord.ToListCoord(x, y, maxX, maxY);
        return elements[coord.X, coord.Y];
    }

    public Cell GetGridCell(Coord coord)
    {
        coord = Coord.ToListCoord(coord.X, coord.Y, maxX, maxY);
        return elements[coord.X, coord.Y];
    }

    public Cell[,] GetGridCells() => elements;

    public Material GetDefaultGridMat() => defaultGridMat;

    public Material GetNotWalkableGridMat() => notWalkableGridMat;

    public Material GetEnemyGridMat() => enemyGridMat;

    public Material GetSelectedEnemyGridMat() => selectedEnemyGridMat;

    public Material GetSelectedGridMat() => selectedGridMat;

    public Material GetPathGridMat() => pathGridMat;

    public Material GetRedPathGridMat() => redPathGridMat;

    public int GetMaxX() => maxX;

    public int GetMaxY() => maxY;
}

