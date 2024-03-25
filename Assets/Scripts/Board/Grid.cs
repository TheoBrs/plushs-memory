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
                    GameObject newCell = Instantiate(gridPrefab, new Vector3(x, 0.01f, y), Quaternion.identity);
                    Coord coords = new Coord(x, y);
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

