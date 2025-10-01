using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

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

    [SerializeField] private int x; 
    [SerializeField] private int y;

    private float moveVal = 4.65f;


    [Header("Control Scheme")]
    [SerializeField] private ControlScheme controls;

    void Update()
    {
        if (Input.GetKeyDown(controls.up))
        {
            if (boardManager.TryMovePiece(this, x, y, x, y + 1))
            {
                y += 1;
                transform.position = new Vector3(transform.position.x, transform.position.y+moveVal, 0);    
            }
            
        }

        if (Input.GetKeyDown(controls.down))
        {
            if (boardManager.TryMovePiece(this, x, y, x, y - 1))
            {
                y -= 1;
                transform.position = new Vector3(transform.position.x, transform.position.y - moveVal, 0);
            }
        }

        if (Input.GetKeyDown(controls.left))
        {
            if (boardManager.TryMovePiece(this, x, y, x - 1, y))
            {
                x -= 1;
                transform.position = new Vector3(transform.position.x - moveVal, transform.position.y, 0);
            }
        }

        if (Input.GetKeyDown(controls.right))
        {
            if (boardManager.TryMovePiece(this, x, y, x + 1, y))
            {
                x += 1;
                transform.position = new Vector3(transform.position.x + moveVal, transform.position.y, 0);
            }
        }
    }
}
