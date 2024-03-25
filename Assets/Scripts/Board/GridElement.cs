using UnityEngine;

namespace Board
{
    public class GridElement
    {
        public Coord coord;
        public GameObject gridElement;
       
        public GridElement(Coord coordVar, GameObject gridElementVar)
        {
            coord = coordVar;
            gridElement = gridElementVar;
        }
    }
}
