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

    public Coord GetCoord() => coord;

    public Vector3 GetGameObjectPosition() => gameObject.transform.position;

    public GameObject GetGameObject() => gameObject;

    public bool getWalkable() => walkable;

    public void SetGameObjectMaterial(Material material)
    {
        gameObject.GetComponent<MeshRenderer>().material = material;
    }
}