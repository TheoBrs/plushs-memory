using UnityEngine;

public class GridElement
{
    Coord coord;
    GameObject gameObject;
       
    public GridElement(Coord coordVar, GameObject gameObjectVar)
    {
        coord = coordVar;
        gameObject = gameObjectVar;
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
}