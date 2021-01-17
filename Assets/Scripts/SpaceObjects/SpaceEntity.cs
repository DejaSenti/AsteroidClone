using ExtensionMethods;
using UnityEngine;

public abstract class SpaceEntity : MonoBehaviour
{
    public Rigidbody2D RB;

    public Vector2 Position { get => new Vector2(CorporealForm.transform.position.x, CorporealForm.transform.position.y); set { CorporealForm.transform.position = new Vector3(value.x, value.y, 0); } }
    public Vector2 Direction { get => this.RotationToVector2(CorporealForm.transform.rotation.eulerAngles.z); set => CorporealForm.transform.rotation = Quaternion.Euler(0, 0, this.Vector2ToRotation(value)); }

    public CorporealForm CorporealForm;
    public GhostForm[] Ghosts;

    protected virtual void Awake()
    {
        Deactivate();

        RB.centerOfMass = Vector2.zero;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        SetColliders(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        SetColliders(false);
    }

    protected virtual void Update()
    {
        Vector2 addedPosition = Vector2.zero;

        if (Position.x < -SpaceBoundary.Width / 2)
        {
            addedPosition = new Vector2(SpaceBoundary.Width, 0);
        }
        else if (Position.x > SpaceBoundary.Width / 2)
        {
            addedPosition = new Vector2(-SpaceBoundary.Width, 0);
        }

        if (Position.y < -SpaceBoundary.Height / 2)
        {
            addedPosition = new Vector2(0, SpaceBoundary.Height);
        }
        else if (Position.y > SpaceBoundary.Height / 2)
        {
            addedPosition = new Vector2(0, -SpaceBoundary.Height);
        }

        if (addedPosition == Vector2.zero)
        {
            PositionGhosts();
        }
        else
        {
            Reposition(addedPosition);
        }
    }

    private void Reposition(Vector2 addedPosition)
    {
        SetColliders(false);
        Position += addedPosition;
        PositionGhosts();
        SetColliders(true);
    }

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

    public abstract void OnCollision(Collider2D collision);

    public abstract void Terminate();
}