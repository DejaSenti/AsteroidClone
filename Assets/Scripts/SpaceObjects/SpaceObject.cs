using UnityEngine;

public abstract class SpaceObject : MonoBehaviour
{
    public Rigidbody2D RB;

    public Vector2 Position { get => new Vector2(transform.position.x, transform.position.y); set { transform.position = new Vector3(value.x, value.y, 0); } }

    public abstract void OnCollisionEnter2D(Collision2D collision);
}