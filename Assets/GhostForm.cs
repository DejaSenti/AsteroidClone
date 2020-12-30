using UnityEngine;

public class GhostForm : SpaceForm
{
    public Vector2Int RelativeDirection;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.GHOST_TAG)
            return;

        base.OnTriggerEnter2D(collision);
    }
}
