using UnityEngine;

[RequireComponent(typeof(SpaceObjectGhost))]
public abstract class SpaceObject : SpaceObjectGhost
{
    public SpaceObjectGhost SpaceObjectGhost;

    private void Awake()
    {
        if (SpaceObjectGhost == null)
        {
            SpaceObjectGhost = GetComponentInChildren<SpaceObjectGhost>();
        }
    }
}

public class SpaceObjectGhost : MonoBehaviour
{
    public Rigidbody2D RB;

    public Vector2 Position { get => new Vector2(transform.position.x, transform.position.y); set { transform.position = new Vector3(value.x, value.y, 0); } }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }
}