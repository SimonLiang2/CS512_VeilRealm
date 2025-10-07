using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private GameObject[,] grid;
    [SerializeField] private int gridSizeRows = 10;
    [SerializeField] private int gridSizeCols = 10;

    [Header("Predefined Obstacles")]
    [SerializeField] private Vector2Int[] walls =
    {
        new Vector2Int(6, 5),
        new Vector2Int(6, 4),
        new Vector2Int(3, 5),
        new Vector2Int(3, 4),
    };

    private static readonly Vector2Int[] kFourDirs = new[]
    {
        new Vector2Int(1, 0),  // right
        new Vector2Int(-1, 0), // left
        new Vector2Int(0, 1),  // up
        new Vector2Int(0, -1), // down
        };

    private HashSet<Vector2Int> wallSet;

    private PieceController selectedPiece;

    void Awake()
    {
        // x first, y second
        grid = new GameObject[gridSizeCols, gridSizeRows];

        wallSet = new HashSet<Vector2Int>(walls ?? new Vector2Int[0]);
        foreach (var w in wallSet)
        {
            if (!InBounds(w.x, w.y))
                Debug.LogWarning($"Wall {w} is out of bounds.");
        }

    }

    private bool InBounds(int x, int y) =>
        x >= 0 && x < gridSizeCols && y >= 0 && y < gridSizeRows;
    private bool IsWall(int x, int y) =>
        wallSet.Contains(new Vector2Int(x, y));
        
    private bool IsFree(int x, int y)
    {
        if (!InBounds(x, y))
            return false;

        if (IsWall(x, y))
            return false;

        if (grid[x, y] != null)
            return false;

        return true;
    }

    private void GetPossibleMovesHelper(int x, int y, List<Vector2Int> results)
    {
        results.Clear();

        if (!InBounds(x, y) || IsWall(x, y))
            return;

        foreach (var d in kFourDirs)
        {
            int nx = x + d.x;
            int ny = y + d.y;
            if (IsFree(nx, ny))
                results.Add(new Vector2Int(nx, ny));
        }
    }

    public List<Vector2Int> GetPossibleMoves(int x, int y)
    {
        var moves = new List<Vector2Int>(4);
        GetPossibleMovesHelper(x, y, moves);
        return moves;
    }

    public bool TryMovePiece(PieceController piece, int fromX, int fromY, int toX, int toY)
    {
        if (!IsFree(toX, toY))
            return false;

        if (!InBounds(fromX, fromY) || IsWall(fromX, fromY))
            return false;

        if (grid[fromX, fromY] == null)
            grid[fromX, fromY] = piece.gameObject;

        grid[fromX, fromY] = null;
        grid[toX, toY] = piece.gameObject;

        Debug.Log($"{piece.name} moved {fromX},{fromY} -> {toX},{toY}");
        return true;
    }
    
    public void RegisterPiece(PieceController piece, int x, int y)
    {
        if (!InBounds(x, y))
        {
            Debug.LogError($"RegisterPiece out of bounds: {x},{y}");
            return;
        }
        grid[x, y] = piece.gameObject;
    }
}
