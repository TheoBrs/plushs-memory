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

    static public Coord ToCenteredCoord(Coord coord, int maxX, int maxY)
    {
        return new Coord(coord.X - maxX / 2, coord.Y - maxY / 2);
    }

    static public Coord ToUncenteredCoord(Coord coord, int maxX, int maxY)
    {
        return new Coord(coord.X + maxX / 2, coord.Y + maxY / 2);
    }

    static public Coord ToCenteredCoord(int x, int y, int maxX, int maxY)
    {
        return new Coord(x - maxX / 2, y - maxY / 2);
    }

    static public Coord ToUncenteredCoord(int x, int y, int maxX, int maxY)
    {
        return new Coord(x + maxX / 2, y + maxY / 2);
    }

    public bool Equals(Coord other)
    {
        return other.X == X && other.Y == Y;
    }

    public float DistanceTo(Coord coord)
    {
        var x = coord.X - X;
        var y = coord.Y - Y;

        return Mathf.Sqrt(x * x + y * y);
    }

    public char DirectionTo(Coord coord)
    {
        char direction = 'n';
        if (coord.X == X + 1 && coord.Y == Y)
            direction = 'r';
        if (coord.X == X - 1 && coord.Y == Y)
            direction = 'l';
        if (coord.X == X && coord.Y == Y + 1)
            direction = 'u';
        if (coord.X == X && coord.Y == Y - 1)
            direction = 'd';
        return direction;
    }

    public override string ToString()
    {
        return "x:" + X + " y:" + Y;
    }

}