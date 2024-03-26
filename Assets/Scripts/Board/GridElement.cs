using UnityEngine;

public class GridElement
{
    Coord coord;
    GameObject gameObject;
    bool walkable;
       
    public GridElement(Coord coordVar, GameObject gameObjectVar, bool walkableVar = true)
    {
        coord = coordVar;
        gameObject = gameObjectVar;
        walkable = walkableVar;
    }

    public Coord GetCoord()
    {
        return coord;
    }

    public Vector3 GetGameObjectPosition()
    {
        return gameObject.transform.position;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void SetGameObjectMaterial(Material material)
    {
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

    public bool getWalkable()
    {
        return walkable;
    }
}