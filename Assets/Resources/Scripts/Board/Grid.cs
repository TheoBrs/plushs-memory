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
      void Awake()
      {
          for (int i = 0; i < maxX ; i++)
          {
              for (int j = 0; j < maxY; j++)
              {
                  Elements[i, j] = new(new (i,j), null, this);
              }
          }
          
      }
      void Update()
      {
          
      }

      public void Move(GridElement element, Coord targetCoord)
      {
          //algo Movement
      }
  }  
}

