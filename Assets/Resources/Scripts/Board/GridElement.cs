using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Board
{
   public class GridElement : Object
   {
       public Coord Coord {get; set;}
       [CanBeNull] public MonoBehaviour Component {get; set;}
       
       public GridElement(Coord coord, MonoBehaviour component)
       {
           Coord = coord;
           Component = component;
       }
   }
 
}
