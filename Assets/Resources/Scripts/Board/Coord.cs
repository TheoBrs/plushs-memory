using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
   public struct Coord
   {
       public readonly int x;
       public readonly int y;
       
       public Coord(int x, int y)
       {
           this.x = x;
           this.y = y;
       }
   }
 
}
