using UnityEngine;

namespace Board
{
    public class Grid : MonoBehaviour
    {
        public int maxX;
        public int maxY;

        public GridElement[,] elements;
        public GameObject gridPrefab;

        //creation de la grille
        void Awake()
        {
            elements = new GridElement[maxX, maxY];

            for (int y = 0; y < maxY ; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    Coord coords = new Coord(x - maxX / 2, y - maxY / 2);
                    GameObject newCell = Instantiate(gridPrefab, new Vector3(coords.x, 0.01f, coords.y), Quaternion.identity);
                    GridElement gridElement = new GridElement(coords, newCell);
                    elements[x, y] = gridElement;
                }
            }
        }
        void Update()
        {
          
        }
    }  
}

