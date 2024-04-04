using UnityEngine;

public class Cell
{
    public Coord Coord { get; set; }
    public float G { get; set; }
    public float H { get; set; }
    public float F { get; set; }
    public Cell Parent { get; set; }
    public bool HasObstacle { get; set; }
    public bool HasEnemy { get; set; }
    public bool IsSelected { get; set; }
    public GameObject GameObject { get; set; }
    public Entity Entity { get; set; }

    public void SetGameObjectMaterial(Material material)
    {
        GameObject.GetComponent<MeshRenderer>().material = material;
    }
}