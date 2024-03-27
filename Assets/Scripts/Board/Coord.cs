using UnityEngine;

public struct Coord
{
    readonly int x;
    readonly int y;
       
    public Coord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    static public Vector3 ToVector3(Coord coord)
    {
        return new Vector3(coord.x, 0, coord.y);
    }

    static public Coord ToCoord(Vector3 vector3)
    {
        return new Coord((int)vector3.x, (int)vector3.y);
    }

    public bool Equals(Coord other)
    {
        return other.x == x && other.y == y;
    }
    
    public override string ToString()
    {
        return "x:" + x + " y:" + y;
    }
    public int GetX() { return x; }
    public int GetY() { return y; }

}