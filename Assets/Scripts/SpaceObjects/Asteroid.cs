using UnityEngine;

public class Asteroid : SpaceEntity
{
#pragma warning disable 0649
    [SerializeField]
    private AsteroidViewController viewController;
#pragma warning restore 0649

    public AsteroidCollisionEvent AsteroidCollisionEvent;

    public float MinSpeed;
    public float MaxSpeed;

    public int Size;
    public float Radius { get => Mathf.Pow(2, Size); }

    protected override void Awake()
    {
        base.Awake();

        if (AsteroidCollisionEvent == null)
            AsteroidCollisionEvent = new AsteroidCollisionEvent();
    }

    public void Initialize(int size, Vector2 position, Vector2 velocity)
    {
        Size = size;

        var localScale = new Vector3(Radius, Radius, 1);
        transform.localScale = localScale;

        Position = position;

        RB.velocity = velocity;

        viewController.UpdateView();
    }

    public override void OnCollision(Collider2D collision)
    {
        AsteroidCollisionEvent.Invoke(this, collision);
    }

    public override void Terminate()
    {
        AsteroidCollisionEvent.RemoveAllListeners();
    }
}

