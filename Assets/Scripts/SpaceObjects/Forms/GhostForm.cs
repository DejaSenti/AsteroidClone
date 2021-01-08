using UnityEngine;

public class GhostForm : SpaceForm
{
    public Vector2Int RelativeDirection;

    private void Awake()
    {
        tag = Entity.tag + Tags.GHOST;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains(Tags.GHOST))
            return;

        base.OnTriggerEnter2D(collision);
    }
}
