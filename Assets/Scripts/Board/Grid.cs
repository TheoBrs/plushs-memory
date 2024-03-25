using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

namespace Board
{
    public class Grid : MonoBehaviour
    {
        public int maxX;
        public int maxY;

        public GridElement[,] Elements;
        public GameObject gridPrefab;

        //creation de la grille
        void Awake()
        {
            Elements = new GridElement[maxX, maxY];

            for (int y = 0; y < maxY ; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    GameObject newCell = Instantiate(gridPrefab, new Vector3(x, 0.01f, y), Quaternion.identity);
                    Coord coords = new Coord(x, y);
                    GridElement gridElement = new GridElement(coords, newCell);
                    Elements[x, y] = gridElement;
                }
            }
        }
        void Update()
        {
          
        }

        public void Move(GridElement element, Coord targetCoord)
        {
          
        }
    }  
}

