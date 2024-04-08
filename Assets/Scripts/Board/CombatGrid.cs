using UnityEngine;

public class CombatGrid : MonoBehaviour
{
    [SerializeField] int maxX;
    [SerializeField] int maxY;

    [SerializeField] Material defaultGridMat;
    [SerializeField] Material notWalkableGridMat;
    [SerializeField] Material enemyGridMat;
    [SerializeField] Material selectedEnemyGridMat;
    [SerializeField] Material selectedGridMat;
    [SerializeField] Material pathGridMat;
    [SerializeField] Material redPathGridMat;

    Cell[,] elements;
    [SerializeField] GameObject gridPrefab;

    //creation de la grille de Combat
    void Awake()
    {
        elements = new Cell[maxX, maxY];

        for (int y = 0; y < maxY ; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                Coord coords = Coord.ToCenteredCoord(x, y, maxX, maxY);
                GameObject newCell = Instantiate(gridPrefab, new Vector3(coords.X, 0.01f, coords.Y), Quaternion.identity);
                newCell.tag = "GridCell";
                Cell gridElement = new Cell { Coord = coords, GameObject = newCell };
                elements[x, y] = gridElement;
            }
        }
    }

    public bool AddObstacle(Coord coord, GameObject obstacle)
    {
        int x = coord.X;
        int y = coord.Y;

        if (elements[x, y].HasEnemy)
            return true;

        elements[x, y].HasObstacle = true;
        elements[x, y].GameObject = obstacle;
        elements[x, y].SetGameObjectMaterial(notWalkableGridMat);
        return false;
    }

    public bool AddEnemy(Coord coord, GameObject enemyPrefabs)
    {
        int x = Coord.ToUncenteredCoord(coord, maxX, maxY).X;
        int y = Coord.ToUncenteredCoord(coord, maxX, maxY).Y;

        GameObject enemy = Instantiate(enemyPrefabs, new Vector3(coord.X, 0.01f, coord.Y) + transform.position, Quaternion.identity);
        Entity enemyScript = enemy.GetComponent<Entity>();
        enemyScript.name = "Enemy";
        enemyScript.CurrentPos = coord;
        enemyScript.speed = 2;

        if (elements[x, y].HasObstacle)
            return true;

        elements[x, y].HasEnemy = true;
        elements[x, y].Entity = enemyScript;
        elements[x, y].SetGameObjectMaterial(enemyGridMat);
        return false;
    }

    public Cell GetGridCell(int x, int y)
    {
        Coord coord = Coord.ToUncenteredCoord(x, y, maxX, maxY);
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

