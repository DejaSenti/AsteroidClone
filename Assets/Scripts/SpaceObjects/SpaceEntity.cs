using ExtensionMethods;
using UnityEngine;

public abstract class SpaceEntity : MonoBehaviour
{
    public Rigidbody2D RB;

    public Vector2 Position { get => new Vector2(CorporealForm.transform.position.x, CorporealForm.transform.position.y); set { CorporealForm.transform.position = new Vector3(value.x, value.y, 0); } }
    public Vector2 Direction { get => this.RotationToVector2(CorporealForm.transform.rotation.eulerAngles.z); }

    public CorporealForm CorporealForm;
    public GhostForm[] Ghosts;

    private void Awake()
    {
        SetColliders(false);

        RB.centerOfMass = Vector2.zero;

        PositionGhosts();

        SetColliders(true);
    }

    public virtual void Update()
    {
        Vector2 nextPosition = Position + RB.velocity * Time.deltaTime;

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

        PositionGhosts();
    }

    public abstract void OnCollision(Collider2D collision);

    private void PositionGhosts()
    {
        foreach (GhostForm ghost in Ghosts)
        {
            var ghostPosition = new Vector3(Position.x + ghost.RelativeDirection.x * SpaceBoundary.Width,
                                            Position.y + ghost.RelativeDirection.y * SpaceBoundary.Height,
                                            0);

            ghost.transform.SetPositionAndRotation(ghostPosition, CorporealForm.transform.rotation);
        }
    }

    private void SetColliders(bool enabled)
    {
        CorporealForm.Collider.enabled = enabled;
        foreach (GhostForm ghost in Ghosts)
        {
            ghost.Collider.enabled = enabled;
        }
    }
}