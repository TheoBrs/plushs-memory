using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    static float ComputeHScore(Coord start, Coord dest)
    {
        return Mathf.Abs(start.X - dest.X) + Mathf.Abs(start.Y - dest.Y);
    }

    static List<Cell> GetWalkableAdjacentSquares(int x, int y, Cell[,] map, Cell target)
    {
        List<Cell> proposedLocations = new List<Cell>();

        foreach (Cell cell in map)
        {
            if ((!cell.HasObstacle && (!cell.HasEnemy || cell == target)) && (
                (cell.Coord.X == x && cell.Coord.Y == y - 1) || 
                (cell.Coord.X == x && cell.Coord.Y == y + 1) ||
                (cell.Coord.X == x - 1 && cell.Coord.Y == y) ||
                (cell.Coord.X == x + 1 && cell.Coord.Y == y) ))
                    proposedLocations.Add(cell);
        }

        // if walkable remove from list
        return proposedLocations;

    }
    public static List<Cell> FindPath(Coord startCoord, Coord targetCoord, Cell[,] map, int maxX, int maxY)
    {   
        Cell current = null;
        var start = map[startCoord.X + (maxX / 2), startCoord.Y + (maxY / 2)];
        var target = map[targetCoord.X + (maxX / 2), targetCoord.Y + (maxY / 2)];
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
            if (closedList.FirstOrDefault(l => l.Coord.X == target.Coord.X && l.Coord.Y == target.Coord.Y) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.Coord.X, current.Coord.Y, map, target);
            g++;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.Coord.X == adjacentSquare.Coord.X
                        && l.Coord.Y == adjacentSquare.Coord.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.Coord.X == adjacentSquare.Coord.X
                        && l.Coord.Y == adjacentSquare.Coord.Y) == null)
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
