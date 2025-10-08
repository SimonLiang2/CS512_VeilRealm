using UnityEngine;

public class SpotlightTarget : MonoBehaviour
{
    private PieceController owner;
    private Vector2Int target;

    public void Init(PieceController owner, Vector2Int target)
    {
        this.owner = owner;
        this.target = target;

        // Ensure it’s clickable (2D). Use BoxCollider if your project is 3D.
        if (GetComponent<Collider2D>() == null)
            gameObject.AddComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        if (owner != null)
            owner.OnSpotlightClicked(target);
    }
}
