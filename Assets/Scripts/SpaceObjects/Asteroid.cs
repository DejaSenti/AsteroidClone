using UnityEngine;

public class Asteroid : SpaceObject
{
    public AsteroidCollisionEvent AsteroidCollisionEvent;

    public CircleCollider2D Collider;

    public float MinSpeed;
    public float MaxSpeed;

    public int Size;

    private void Awake()
    {
        if (AsteroidCollisionEvent == null)
            AsteroidCollisionEvent = new AsteroidCollisionEvent();
    }

    public void Initialize(int size, Vector2 position, Vector2 velocity)
    {
        Size = size;

        var scale = Mathf.Pow(2, Size);

        ApplyScaleToAll(scale);

        Position = position;

        RB.velocity = velocity;
    }

    private void ApplyScaleToAll(float scale)
    {
        var localScale = new Vector3(scale, scale, 1);

        transform.localScale = localScale;

        foreach (SpaceGhost ghost in ghosts)
        {
            ghost.transform.localScale = localScale;
        }
    }

    public override void OnCollision(Collider2D collision)
    {
        if (AsteroidCollisionEvent != null && isActiveAndEnabled)
            AsteroidCollisionEvent.Invoke(this, collision);
    }
}
