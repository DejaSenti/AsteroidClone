using ExtensionMethods;
using UnityEngine;

[RequireComponent(typeof(CorporealForm))]
public abstract class SpaceObject : MonoBehaviour
{
    public Rigidbody2D RB;

    public Vector2 Position { get => new Vector2(CorporealForm.transform.position.x, CorporealForm.transform.position.y); set { CorporealForm.transform.position = new Vector3(value.x, value.y, 0); } }
    public Vector2 Direction { get => this.RotationToVector2(CorporealForm.transform.rotation.eulerAngles.z); }

    public CorporealForm CorporealForm;
    public SpaceGhost[] Ghosts;

    private void Start()
    {
        RB.centerOfMass = Vector2.zero;

        PositionGhosts();
    }

    protected virtual void FixedUpdate()
    {
        PositionGhosts();

        Vector2 nextPosition = Position + RB.velocity * Time.fixedDeltaTime;

        if (Position.x >= -SpaceBoundary.Width / 2 && Position.x <= SpaceBoundary.Width / 2)
        {
            if (nextPosition.x > SpaceBoundary.Width / 2)
            {
                Position -= new Vector2(SpaceBoundary.Width, 0);
            }
            else if (nextPosition.x < -SpaceBoundary.Width / 2)
            {
                Position += new Vector2(SpaceBoundary.Width, 0);
            }
        }

        if (Position.y >= -SpaceBoundary.Height / 2 && Position.y <= SpaceBoundary.Height / 2)
        {
            if (nextPosition.y > SpaceBoundary.Height / 2)
            {
                Position -= new Vector2(0, SpaceBoundary.Height);
            }
            else if (nextPosition.y < -SpaceBoundary.Height / 2)
            {
                Position += new Vector2(0, SpaceBoundary.Height);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.GHOST_TAG)
            return;

        OnCollision(collision);
    }

    public abstract void OnCollision(Collider2D collision);

    private void PositionGhosts()
    {
        foreach (SpaceGhost ghost in Ghosts)
        {
            var ghostPosition = new Vector3(CorporealForm.transform.position.x + ghost.RelativeDirection.x * SpaceBoundary.Width,
                                            CorporealForm.transform.position.y + ghost.RelativeDirection.y * SpaceBoundary.Height,
                                            CorporealForm.transform.position.z);

            ghost.transform.SetPositionAndRotation(ghostPosition, CorporealForm.transform.rotation);
        }
    }
}