using UnityEditor;
using UnityEngine;

public class CombatGrid : MonoBehaviour
{
    [SerializeField] int maxX;
    [SerializeField] int maxY;

    Material defaultGridMat;
    Material notWalkableGridMat;
    Material enemyGridMat;
    Material selectedGridMat;
    Material pathGridMat;
    Material redPathGridMat;

    Cell[,] elements;
    [SerializeField] GameObject gridPrefab;

    //creation de la grille de Combat
    void Awake()
    {
        defaultGridMat = Resources.Load<Material>("defaultGrid");
        notWalkableGridMat = Resources.Load<Material>("notWalkableGrid");
        enemyGridMat = Resources.Load<Material>("enemyGrid");
        selectedGridMat = Resources.Load<Material>("selectedGrid");
        pathGridMat = Resources.Load<Material>("pathGrid");
        redPathGridMat = Resources.Load<Material>("redGrid");

        elements = new Cell[maxX, maxY];

        for (int y = 0; y < maxY ; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                Coord coords = new Coord(x - (maxX / 2), y - (maxY / 2));
                GameObject newCell = Instantiate(gridPrefab, new Vector3(coords.X, 0.01f, coords.Y), Quaternion.identity);

                Cell gridElement = new Cell { Coord = coords, GameObject = newCell };

                // This is to force the cell to have an "obstacle"
                if (x == -1 + (maxX / 2) && y == -1 + (maxY / 2))
                {
                    gridElement.IsObstacle = true;
                    gridElement.SetGameObjectMaterial(notWalkableGridMat);
                }

                // This is to force the cell to have an "enemy"
                if (x == 1 + (maxX / 2) && y == 1 + (maxY / 2))
                {
                    gridElement.HasEnemy = true;
                    gridElement.SetGameObjectMaterial(enemyGridMat);
                }

                elements[x, y] = gridElement;
            }
        }
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

