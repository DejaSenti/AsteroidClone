using ExtensionMethods;
using UnityEngine;

public abstract class SpaceObject : MonoBehaviour
{
    public Rigidbody2D RB;

    public Vector2 Position { get => new Vector2(transform.position.x, transform.position.y); set { transform.position = new Vector3(value.x, value.y, 0); } }
    public Vector2 Direction { get => this.RotationToVector2(transform.rotation.eulerAngles.z); }

    public abstract void OnTriggerEnter2D(Collider2D collision);
    
    public SpaceGhost[] ghosts;

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

    private void PositionGhosts()
    {
        foreach (SpaceGhost ghost in ghosts)
        {
            var ghostPosition = new Vector3(transform.position.x + ghost.RelativeDirection.x * SpaceBoundary.Width,
                                            transform.position.y + ghost.RelativeDirection.y * SpaceBoundary.Height,
                                            transform.position.z);

            ghost.transform.SetPositionAndRotation(ghostPosition, transform.rotation);
        }
    }
}