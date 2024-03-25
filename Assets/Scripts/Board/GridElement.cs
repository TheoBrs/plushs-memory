using UnityEngine;

namespace Board
{
    public class GridElement
    {
        Coord coord;
        GameObject gridElement;
       
        public GridElement(Coord coordVar, GameObject gridElementVar)
        {
            coord = coordVar;
            gridElement = gridElementVar;
        }
    }
}
