using UnityEngine;

public class CombatGrid : MonoBehaviour
{
    [SerializeField] int maxX;
    [SerializeField] int maxY;

    [SerializeField] Material gridMat;
    [SerializeField] Material selectedGridMat;
    [SerializeField] Material pathGridMat;
    [SerializeField] Material redPathGridMat;

    GridElement[,] elements;
    [SerializeField] GameObject gridPrefab;

    //creation de la grille de Combat
    void Awake()
    {
        elements = new GridElement[maxX, maxY];

        for (int y = 0; y < maxY ; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                Coord coords = new Coord(x - (maxX / 2), y - (maxY / 2));
                GameObject newCell = Instantiate(gridPrefab, new Vector3(coords.x, 0.01f, coords.y), Quaternion.identity);
                bool walkable = true;

                // This is to force the cell to have an "obstacle"
                if (x == 1 && y == 1)
                    walkable = false;

                GridElement gridElement = new GridElement(coords, newCell, walkable);
                elements[x, y] = gridElement;
            }
        }
    }

    public GridElement GetGridElement(int x, int y)
    { 
        return elements[x + (maxX / 2), y + (maxY / 2)];
    }

    public GridElement[,] GetGridElements()
    {
        return elements;
    }

    public Material GetGridMat()
    {
        return gridMat;
    }

    public Material GetSelectedGridMat()
    {
        return selectedGridMat;
    }

    public Material GetPathGridMat()
    {
        return pathGridMat;
    }

    public Material GetRedPathGridMat() 
    {
        return redPathGridMat;
    }
}

