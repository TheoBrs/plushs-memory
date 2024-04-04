using UnityEngine;

public struct Coord
{
    readonly public int X { get; }
    readonly public int Y { get; }

    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }

    static public Vector3 ToVector3(Coord coord)
    {
        return new Vector3(coord.X, 0, coord.Y);
    }

    static public Coord ToCoord(Vector3 vector3)
    {
        return new Coord((int)vector3.x, (int)vector3.y);
    }

    public bool Equals(Coord other)
    {
        return other.X == X && other.Y == Y;
    }
    
    public override string ToString()
    {
        return "x:" + X + " y:" + Y;
    }

}