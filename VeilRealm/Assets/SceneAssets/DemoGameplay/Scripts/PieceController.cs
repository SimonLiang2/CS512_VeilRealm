using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ControlScheme
{
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
}

public class PieceController : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject spotlight;

    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private float moveVal = 4.65f;

    [Header("Control Scheme")]
    [SerializeField] private ControlScheme controls;

    private bool isSelected = false;
    private readonly List<GameObject> activeHighlights = new List<GameObject>();

    void Start()
    {
        boardManager.RegisterPiece(this, x, y);
    }

    void Update()
    {
        if (Input.GetKeyDown(controls.up))
        {
            if (boardManager.TryMovePiece(this, x, y, x, y + 1))
            {
                y += 1;
                transform.position = new Vector3(transform.position.x, transform.position.y + moveVal, 0);
                ClearHighlights();
            }
        }

        if (Input.GetKeyDown(controls.down))
        {
            if (boardManager.TryMovePiece(this, x, y, x, y - 1))
            {
                y -= 1;
                transform.position = new Vector3(transform.position.x, transform.position.y - moveVal, 0);
                ClearHighlights();
            }
        }

        if (Input.GetKeyDown(controls.left))
        {
            if (boardManager.TryMovePiece(this, x, y, x - 1, y))
            {
                x -= 1;
                transform.position = new Vector3(transform.position.x - moveVal, transform.position.y, 0);
                ClearHighlights();
            }
        }

        if (Input.GetKeyDown(controls.right))
        {
            if (boardManager.TryMovePiece(this, x, y, x + 1, y))
            {
                x += 1;
                transform.position = new Vector3(transform.position.x + moveVal, transform.position.y, 0);
                ClearHighlights();
            }
        }
    }

    private void OnMouseDown()
    {
        if (isSelected)
        {
            isSelected = false;
            ClearHighlights();
        }
        else
        {
            isSelected = true;
            ShowPossibleMoves();
        }
    }

    private void ShowPossibleMoves()
    {
        ClearHighlights();

        var moves = boardManager.GetPossibleMoves(x, y);
        foreach (var m in moves)
        {
            Vector3 worldPos = transform.position + new Vector3((m.x - x) * moveVal, (m.y - y) * moveVal, 0f);
            var hl = Instantiate(spotlight, worldPos, Quaternion.identity);
            activeHighlights.Add(hl);

            var marker = hl.GetComponent<SpotlightTarget>();
            if (marker == null) marker = hl.AddComponent<SpotlightTarget>();
            marker.Init(this, m);
        }
    }

    private void ClearHighlights()
    {
        for (int i = 0; i < activeHighlights.Count; i++)
        {
            if (activeHighlights[i] != null) Destroy(activeHighlights[i]);
        }
        activeHighlights.Clear();
    }

    public void OnSpotlightClicked(Vector2Int target)
    {
        if (boardManager.TryMovePiece(this, x, y, target.x, target.y))
        {
            int dx = target.x - x;
            int dy = target.y - y;

            x = target.x;
            y = target.y;

            transform.position = new Vector3(
                transform.position.x + dx * moveVal,
                transform.position.y + dy * moveVal,
                0f
            );

            ClearHighlights();
            isSelected = false;
        }
    }
}
