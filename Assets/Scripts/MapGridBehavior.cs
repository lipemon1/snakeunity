using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridBehavior : MonoBehaviour
{
    [SerializeField] int Width = 20;
    [SerializeField] int Height = 15;
    [SerializeField] float FrameSeconds;
    [SerializeField] int InitialSnakeSize;

    int[,] grid;
    List<Vector2> snakeBodyPoints;
    
    bool gameIsRunning;

    public enum GridValue
    {
        Blank,
        SnakeBody,
        Apple,
        Wall
    }

    void Start()
    {
        CreateSnakeInitialPoints();
        
        grid = new int[Width, Height];
        GridRenderer.Instance.CreateGrid(Width, Height);
        PositionSnake();

        gameIsRunning = true;

        StartCoroutine(MoveSnakeCo(FrameSeconds));
    }

    void CreateSnakeInitialPoints()
    {
        snakeBodyPoints = new List<Vector2>(InitialSnakeSize);
        
        snakeBodyPoints.Add(new Vector2((int)Width / 2, (int)Height / 2));

        for (int i = 0; i < InitialSnakeSize - 1; i++)
        {
            Vector2 lastPoint = snakeBodyPoints[i];
            snakeBodyPoints.Add(new Vector2(lastPoint.x, lastPoint.y - 1));
        }
    }

    IEnumerator MoveSnakeCo(float frameRate)
    {
        while (gameIsRunning)
        {
            ResetGridValues();
            MovementSnake(PlayerInputBehavior.Instance.LastDirection());
            
            yield return new WaitForSeconds(frameRate);
        }
    }

    void PositionSnake()
    {
        foreach (Vector2 snakeBodyPoint in snakeBodyPoints)
            grid[(int) snakeBodyPoint.x, (int) snakeBodyPoint.y] = (int) GridValue.SnakeBody;
        
        GridRenderer.Instance.UpdateGrid(grid);
    }

    void ResetGridValues()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                grid[i, j] = (int) GridValue.Blank;
            }
        }
    }

    void MovementSnake(PlayerInputBehavior.Direction dir)
    {
        List<Vector2> curSnakeBody = snakeBodyPoints;
        List<Vector2> newSnakeBody = new List<Vector2>();
        
        Vector2 curHeadPos = curSnakeBody[curSnakeBody.Count - 1];

        int newWidthValue = (int) curHeadPos.x;
        int newHeightValue = (int) curHeadPos.y;
        switch (dir)
        {
            case PlayerInputBehavior.Direction.Up:
                newHeightValue = (int) (curHeadPos.y - 1);
                break;
            case PlayerInputBehavior.Direction.Down:
                newHeightValue = (int) (curHeadPos.y + 1);
                break;
            case PlayerInputBehavior.Direction.Left:
                newWidthValue = (int) (curHeadPos.x - 1);
                break;
            case PlayerInputBehavior.Direction.Right:
                newWidthValue = (int) (curHeadPos.x + 1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
        }

        for (int i = 0; i < curSnakeBody.Count; i++)
        {
            if(i != curSnakeBody.Count - 1)
                newSnakeBody.Add(curSnakeBody[i + 1]);
        }
        
        newSnakeBody.Add(new Vector2(newWidthValue, newHeightValue));

        snakeBodyPoints = newSnakeBody;
        PositionSnake();
    }
}
