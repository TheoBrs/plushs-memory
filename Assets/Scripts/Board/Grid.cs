using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
  public class Grid : MonoBehaviour
  {
      private int maxX {get; set;}
      private int maxY{get; set;}
  
      public GridElement[,] Elements;

      //creation de la grille
      void Awake()
      {
          for (int i = 0; i < maxX ; i++)
          {
              for (int j = 0; j < maxY; j++)
              {
                  Elements[i, j] = new(new (i,j), null); //ligne buger ?
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

