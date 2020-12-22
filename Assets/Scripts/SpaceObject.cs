using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    public delegate void SpaceObjectCollision(object sender, string colliderTag);
    public static event SpaceObjectCollision SpaceObjectCollisionEvent;

    public Rigidbody2D RB;
    public Collider2D Collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceObjectCollisionEvent.Invoke(this, collision.tag);
    }
}