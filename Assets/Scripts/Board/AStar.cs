using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell
{
    public Coord coord;
    public float G;
    public float H;
    public float F;
    public Cell parent;
}

public class AStar
{
    static float ComputeHScore(Coord start, Coord dest)
    {
        return Mathf.Abs(start.x - dest.x) + Mathf.Abs(start.y - dest.y);
    }

    static List<Cell> GetWalkableAdjacentSquares(int x, int y, Cell[] map)
    {
        List<Cell> proposedLocations = new List<Cell>();

        foreach (Cell cell in map)
        {
            if (cell.coord.x == x && cell.coord.y == y - 1)
                proposedLocations.Add(cell);
            if (cell.coord.x == x && cell.coord.y == y + 1)
                proposedLocations.Add(cell);
            if (cell.coord.x == x - 1 && cell.coord.y == y)
                proposedLocations.Add(cell);
            if (cell.coord.x == x + 1 && cell.coord.y == y)
                proposedLocations.Add(cell);
        }

        // if walkable remove from list
        return proposedLocations;

    }
    public static List<Cell> FindPath(Coord startCoord, Coord destCoord, Cell[] map)
    {   
        Cell current = null;
        var start = new Cell { coord = startCoord };
        var target = new Cell { coord = destCoord };
        var openList = new List<Cell>();
        var closedList = new List<Cell>();
        int g = 0;

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(l => l.coord.x == target.coord.x && l.coord.y == target.coord.y) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.coord.x, current.coord.y, map);
            g++;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.coord.x == adjacentSquare.coord.x
                        && l.coord.y == adjacentSquare.coord.y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.coord.x == adjacentSquare.coord.x
                        && l.coord.y == adjacentSquare.coord.y) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.coord, target.coord);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.parent = current;
                    }
                }
            }
        }

        return closedList;
    }
}
