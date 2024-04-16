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

    public char Direction { get; set; }
    public void SetGameObjectMaterial(Material material)
    {
        GameObject.GetComponent<MeshRenderer>().material = material;
        GameObject.transform.rotation = Direction switch
        {
            'l' => Quaternion.Euler(0, 90, 0),
            'r' => Quaternion.Euler(0, -90, 0),
            'd' => Quaternion.Euler(0, 0, 0),
            _ => Quaternion.Euler(0, 180, 0),
        };
    }
}