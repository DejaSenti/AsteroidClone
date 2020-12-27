using UnityEngine;

public class Asteroid : SpaceObject
{
    public AsteroidCollisionEvent AsteroidCollisionEvent;

    public CircleCollider2D Collider;

    public float MinSpeed;
    public float MaxSpeed;

    public int Size;

    private float scale { get => Mathf.Pow(2, Size); }

    private void Awake()
    {
        if (AsteroidCollisionEvent == null)
            AsteroidCollisionEvent = new AsteroidCollisionEvent();
    }

    public void Initialize(int size, Vector2 position, Vector2 velocity)
    {
        Size = size;
        transform.localScale = new Vector3(scale, scale, 1);

        Position = position;

        RB.velocity = velocity;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (AsteroidCollisionEvent != null && isActiveAndEnabled)
            AsteroidCollisionEvent.Invoke(this, collision.gameObject.tag);
    }
}
