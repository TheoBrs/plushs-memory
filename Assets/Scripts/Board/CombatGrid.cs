using UnityEngine;

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
    //creation de la grille de Combat
    void Awake()
    {
        battleManager = BattleManager.Instance;
        elements = new Cell[maxX, maxY];

        for (int y = 0; y < maxY ; y++)
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

        foreach (var tuple in battleManager.nextBattlePlacement.enemyCellList)
        {
            AddEnemy(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        foreach (var tuple in battleManager.nextBattlePlacement.obstacleCellList)
        {
            AddObstacle(tuple.Item1, tuple.Item2);
        }
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

    public bool AddEnemy(Coord coord, GameObject enemyPrefabs, Vector3 rotation, Coord size)
    {
        GameObject enemy = Instantiate(enemyPrefabs, new Vector3(coord.X * gridCellScale + (size.X - 1) * gridCellScale / 2, 0.01f, coord.Y * gridCellScale + (size.Y - 1) * gridCellScale / 2) + transform.position, Quaternion.Euler(rotation));

        enemy.transform.localScale *= Mathf.Sqrt(size.X * size.Y);
        Entity enemyScript = enemy.GetComponent<Entity>();
        enemyScript.CurrentPos = coord;
        enemyScript.speed = 2;
        enemyScript.Size = size;

        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                int x = Coord.ToListCoord(coord, maxX, maxY).X + i;
                int y = Coord.ToListCoord(coord, maxX, maxY).Y + j;

                if (elements[x, y].HasObstacle || elements[x, y].HasEnemy)
                    return false;
            }
        }

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

