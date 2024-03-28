using UnityEngine;

public class CombatGrid : MonoBehaviour
{
    [SerializeField] int maxX;
    [SerializeField] int maxY;

    [SerializeField] Material defaultGridMat;
    [SerializeField] Material notWalkableGridMat;
    [SerializeField] Material enemyGridMat;
    [SerializeField] Material selectedGridMat;
    [SerializeField] Material pathGridMat;
    [SerializeField] Material redPathGridMat;

    Cell[,] elements;
    [SerializeField] GameObject gridPrefab;
    [SerializeField] GameObject enemyPrefab;

    //creation de la grille de Combat
    void Awake()
    {
        elements = new Cell[maxX, maxY];

        for (int y = 0; y < maxY ; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                Coord coords = new Coord(x - (maxX / 2), y - (maxY / 2));
                GameObject newCell = Instantiate(gridPrefab, new Vector3(coords.X, 0.01f, coords.Y), Quaternion.identity);

                Cell gridElement = new Cell { Coord = coords, GameObject = newCell };

                elements[x, y] = gridElement;
            }
        }
    }

    public void AddObstacle(Coord coord, GameObject obstacle)
    {
        int x = coord.X;
        int y = coord.Y;

        elements[x, y].IsObstacle = true;
        elements[x, y].GameObject = obstacle;
        elements[x, y].SetGameObjectMaterial(notWalkableGridMat);
    }

    public void AddEnemy(Coord coord, Entity entity)
    {
        int x = coord.X;
        int y = coord.Y;

        elements[x, y].HasEnemy = true;
        elements[x, y].Entity = entity;
        elements[x, y].SetGameObjectMaterial(enemyGridMat);
    }

    public Cell GetGridElement(int x, int y)
    { 
        return elements[x + (maxX / 2), y + (maxY / 2)];
    }

    public Cell[,] GetGridElements() => elements;

    public Material GetDefaultGridMat() => defaultGridMat;

    public Material GetNotWalkableGridMat() => notWalkableGridMat;

    public Material GetEnemyGridMat() => enemyGridMat;

    public Material GetSelectedGridMat() => selectedGridMat;

    public Material GetPathGridMat() => pathGridMat;

    public Material GetRedPathGridMat() => redPathGridMat;

    public int GetMaxX() => maxX;

    public int GetMaxY() => maxY;
}

