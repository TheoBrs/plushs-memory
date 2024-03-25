using UnityEngine;

public class GridElement
{
    public Coord coord;
    public GameObject gameObject;
       
    public GridElement(Coord coordVar, GameObject gameObjectVar)
    {
        coord = coordVar;
        gameObject = gameObjectVar;
    }
}