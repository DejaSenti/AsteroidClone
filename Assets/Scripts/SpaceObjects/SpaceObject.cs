using ExtensionMethods;
using UnityEngine;

public abstract class SpaceObject : MonoBehaviour
{
    public bool IsGhost;

    public SpaceObject Ghost;

    public Rigidbody2D RB;

    public Vector2 Position { get => new Vector2(transform.position.x, transform.position.y); set { transform.position = new Vector3(value.x, value.y, 0); } }
    public Vector2 Direction { get => this.RotationToVector2(transform.rotation.eulerAngles.z); }

    public abstract void OnCollisionEnter2D(Collision2D collision);

    protected virtual void Update()
    {
        if (!IsGhost && Ghost != null)
        {
            Vector2 nextPosition = Position;

            if (RB.velocity.magnitude == 0)
            {
                //Ghost.Position = Position - new Vector2(Direction.x * SpaceBoundary.Width, Direction.y * SpaceBoundary.Height);
                return;
            }
            else
            {
                nextPosition = Position + RB.velocity * Time.deltaTime;
            }

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

            //var velocityDirection = RB.velocity.Sign();
            //Vector2 ghostPosition = Position - new Vector2(velocityDirection.x * SpaceBoundary.Width,velocityDirection.y * SpaceBoundary.Height);
            //Ghost.Position = ghostPosition;
            //Ghost.transform.rotation = transform.rotation;
        }
    }
}