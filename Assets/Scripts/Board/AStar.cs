using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell
{
    Coord coord;
    float G;
    float H;
    float F;
    Cell parent;
    bool walkable;

    public Coord GetCoord() { return coord; }
    public void SetCoord(Coord coord) { this.coord = coord; }
    public float GetG() { return G; }
    public void SetG(float g) { G = g; }
    public float GetH() { return H; }
    public void SetH(float h) { H = h; }
    public float GetF() { return F; }
    public void SetF(float f) { F = f; }
    public Cell GetParent() { return parent; }
    public void SetParent(Cell parent) { this.parent = parent; }
    public bool GetWalkable() { return walkable; }
    public void SetWalkable(bool walkable) { this.walkable = walkable; }

    public Cell(Coord coord)
    {
        this.coord = coord;
    }
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
            if (cell.GetCoord().GetX() == x && cell.GetCoord().GetY() == y - 1 && cell.GetWalkable())
                proposedLocations.Add(cell);
            if (cell.GetCoord().GetX() == x && cell.GetCoord().GetY() == y + 1 && cell.GetWalkable())
                proposedLocations.Add(cell);
            if (cell.GetCoord().GetX() == x - 1 && cell.GetCoord().GetY() == y && cell.GetWalkable())
                proposedLocations.Add(cell);
            if (cell.GetCoord().GetX() == x + 1 && cell.GetCoord().GetY() == y && cell.GetWalkable())
                proposedLocations.Add(cell);
        }

        // if walkable remove from list
        return proposedLocations;

    }
    public static List<Cell> FindPath(Coord startCoord, Coord destCoord, Cell[] map)
    {   
        Cell current = null;
        var start = new Cell(startCoord);
        var target = new Cell(destCoord);
        var openList = new List<Cell>();
        var closedList = new List<Cell>();
        int g = 0;

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(l => l.GetF());
            current = openList.First(l => l.GetF() == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(l => l.GetCoord().GetX() == target.GetCoord().GetX() && l.GetCoord().GetY() == target.GetCoord().GetY()) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.GetCoord().GetX(), current.GetCoord().GetY(), map);
            g++;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.GetCoord().GetX() == adjacentSquare.GetCoord().GetX()
                        && l.GetCoord().GetY() == adjacentSquare.GetCoord().GetY()) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.GetCoord().GetX() == adjacentSquare.GetCoord().GetX()
                        && l.GetCoord().GetY() == adjacentSquare.GetCoord().GetY()) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.SetG(g);
                    adjacentSquare.SetH(ComputeHScore(adjacentSquare.GetCoord(), target.GetCoord()));
                    adjacentSquare.SetF(adjacentSquare.GetG() + adjacentSquare.GetH());
                    adjacentSquare.SetParent(current);

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentSquare.GetH() < adjacentSquare.GetF())
                    {

                        adjacentSquare.SetG(g);
                        adjacentSquare.SetF(adjacentSquare.GetG() + adjacentSquare.GetH());
                        adjacentSquare.SetParent(current);
                    }
                }
            }
        }

        Cell tempCell = closedList.Last();
        closedList.Clear();
        closedList.Insert(0, tempCell);
        while (tempCell != start)
        {
            tempCell = tempCell.GetParent();
            closedList.Insert(0, tempCell);
        }
        return closedList;
    }
}
