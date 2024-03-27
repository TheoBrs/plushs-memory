using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell
{
    public Coord Coord { get; set; }
    public float G { get; set; }
    public float H { get; set; }
    public float F { get; set; }
    public Cell Parent { get; set; }
    public bool Walkable { get; set; }
}

public class AStar
{
    static float ComputeHScore(Coord start, Coord dest)
    {
        return Mathf.Abs(start.GetX() - dest.GetX()) + Mathf.Abs(start.GetY() - dest.GetY());
    }

    static List<Cell> GetWalkableAdjacentSquares(int x, int y, Cell[] map)
    {
        List<Cell> proposedLocations = new List<Cell>();

        foreach (Cell cell in map)
        {
            if (cell.Coord.GetX() == x && cell.Coord.GetY() == y - 1 && cell.Walkable)
                proposedLocations.Add(cell);
            if (cell.Coord.GetX() == x && cell.Coord.GetY() == y + 1 && cell.Walkable)
                proposedLocations.Add(cell);
            if (cell.Coord.GetX() == x - 1 && cell.Coord.GetY() == y && cell.Walkable)
                proposedLocations.Add(cell);
            if (cell.Coord.GetX() == x + 1 && cell.Coord.GetY() == y && cell.Walkable)
                proposedLocations.Add(cell);
        }

        // if walkable remove from list
        return proposedLocations;

    }
    public static List<Cell> FindPath(Coord startCoord, Coord destCoord, Cell[] map)
    {   
        Cell current = null;
        var start = new Cell { Coord = startCoord };
        var target = new Cell { Coord = destCoord };
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
            if (closedList.FirstOrDefault(l => l.Coord.GetX() == target.Coord.GetX() && l.Coord.GetY() == target.Coord.GetY()) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.Coord.GetX(), current.Coord.GetY(), map);
            g++;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.Coord.GetX() == adjacentSquare.Coord.GetX()
                        && l.Coord.GetY() == adjacentSquare.Coord.GetY()) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.Coord.GetX() == adjacentSquare.Coord.GetX()
                        && l.Coord.GetY() == adjacentSquare.Coord.GetY()) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G  = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.Coord, target.Coord);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

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
                        adjacentSquare.Parent = current;
                    }
                }
            }
        }

        Cell tempCell = closedList.Last();
        closedList.Clear();
        closedList.Insert(0, tempCell);
        while (tempCell != start)
        {
            tempCell = tempCell.Parent;
            closedList.Insert(0, tempCell);
        }
        return closedList;
    }
}
