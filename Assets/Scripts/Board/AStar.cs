using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    static float ComputeHScore(Coord start, Coord dest)
    {
        return Mathf.Pow(start.X - dest.X, 2) + Mathf.Pow(start.Y - dest.Y, 2);
    }

    static List<Cell> GetWalkableAdjacentSquares(int x, int y, Cell[,] map, Cell target, int maxX, int maxY)
    {
        List<Cell> proposedLocations = new List<Cell>();
        List<(int, int)> adjacentCoord = new List<(int, int)>
        {
            (-1, 0),
            (+1, 0),
            (0, -1),
            (0, +1)
        };

        foreach (var coord in adjacentCoord)
        {
            Cell cell = map[Mathf.Clamp(x + coord.Item1 + maxX / 2, 0, maxX - 1), Mathf.Clamp(y + coord.Item2 + maxY / 2, 0, maxY - 1)];
            if (!cell.HasObstacle && (!cell.HasEnemy || cell == target))
                    proposedLocations.Add(cell);
        }

        // if walkable remove from list
        return proposedLocations;

    }
    public static List<Cell> FindPath(Coord startCoord, Coord targetCoord)
    {
        CombatGrid grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
        Cell[,] map = grid.GetGridCells();
        int maxX = grid.GetMaxX();
        int maxY = grid.GetMaxY();

        Cell current = null;
        var start = map[startCoord.X + (maxX / 2), startCoord.Y + (maxY / 2)];
        var target = map[targetCoord.X + (maxX / 2), targetCoord.Y + (maxY / 2)];
        var openList = new List<Cell>();
        var closedList = new List<Cell>();

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

            var adjacentSquares = GetWalkableAdjacentSquares(current.Coord.X, current.Coord.Y, map, target, maxX, maxY);
            foreach (var adjacentSquare in adjacentSquares)
            {
                float tentative_g_score = adjacentSquare.G + current.G;
                Cell temp = current;
                while (temp != start)
                {
                    temp = temp.Parent;
                    tentative_g_score += temp.G;
                }

                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.Coord.X == adjacentSquare.Coord.X
                        && l.Coord.Y == adjacentSquare.Coord.Y) != null && tentative_g_score >= adjacentSquare.G)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.Coord.X == adjacentSquare.Coord.X
                        && l.Coord.Y == adjacentSquare.Coord.Y) == null || tentative_g_score < adjacentSquare.G)
                {
                    // compute its score, set the parent
                    adjacentSquare.G  = tentative_g_score;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.Coord, target.Coord);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
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
