using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    public int Width = 23;
    public int Height = 15;
    public GameObject FinishFlag;

    public Maze GenerateMaze()
    {
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[Width, Height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {   
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeGeneratorCell { X = x, Y = y };
            }
        }
        RemoveBrokenWall(cells);
        WallsBackTracker(cells);

        Maze maze = new Maze();

        maze.cells = cells;
        maze.finishPos = PlaceMazeExit(cells);


        return maze;
    }

    //This method removes walls from MazeGeneratorCell array via "Backtracker algorithm"
    private void WallsBackTracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];

        current.isVisited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedCells = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            //Сheck neighboring cells
            if (x > 0 && !maze[x - 1, y].isVisited) unvisitedCells.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].isVisited) unvisitedCells.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].isVisited) unvisitedCells.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].isVisited) unvisitedCells.Add(maze[x, y + 1]);

            if (unvisitedCells.Count > 0)
            {
                MazeGeneratorCell choosen = unvisitedCells[UnityEngine.Random.Range(0, unvisitedCells.Count)];
                RemoveWall(current, choosen);

                choosen.isVisited = true;
                stack.Push(choosen);
                choosen.DistanceFromStart = current.DistanceFromStart + 1;
                current = choosen;
            }
            else
                current = stack.Pop();


        } while (stack.Count > 0);
    }

    //Placing exit from maze
    private Vector2Int PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, Height - 2];
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[Width - 2, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
        }

        if (furthest.X == 0) furthest.WallLeft = false;
        else if (furthest.Y == 0) furthest.WallBottom = false;
        else if (furthest.X == Width - 2) maze[furthest.X + 1, furthest.Y].WallLeft = false;
        else if (furthest.Y == Height - 2) maze[furthest.X, furthest.Y + 1].WallBottom = false;
        FinishFlag = GameObject.Find("finish");

        FinishFlag.transform.position = new Vector2(furthest.X + 0.5f, furthest.Y+ 0.5f);
        return new Vector2Int(furthest.X, furthest.Y);
    }

    //Method consider removing side-walls
    public void RemoveBrokenWall(MazeGeneratorCell[,] maze)
    {
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, Height - 1].WallLeft = false;
        }
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width - 1, y].WallBottom = false;
        }
    }

    //Remove wall method defenition
    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }
}
