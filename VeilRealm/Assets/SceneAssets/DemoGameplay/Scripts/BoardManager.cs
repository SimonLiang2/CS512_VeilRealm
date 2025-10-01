using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private GameObject[,] grid;
    [SerializeField] private int gridSizeRows = 9;
    [SerializeField] private int gridSizeCols = 10;

    private PieceController selectedPiece; 

    void Start()
    {
        grid = new GameObject[gridSizeRows, gridSizeCols];
    }

    public bool TryMovePiece(PieceController piece, int fromX, int fromY, int toX, int toY)
    {
        if (toX < 0 || toX >= gridSizeCols || toY < 0 || toY > gridSizeRows)
            return false;

        if (grid[toX, toY] != null)
            return false;

        grid[fromX, fromY] = null;
        grid[toX, toY] = piece.gameObject;
        Debug.Log(piece.gameObject.name + " moved to: " + toX + "," + toY);

        return true;
    }
}
