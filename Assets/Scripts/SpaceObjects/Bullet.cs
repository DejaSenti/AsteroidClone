using UnityEngine;

public class Bullet : SpaceEntity
{
    public BulletDestroyedEvent BulletDestroyedEvent;

    private float lifetime;

    private Timer distanceTimer;

    protected override void Awake()
    {
        base.Awake();

        if (BulletDestroyedEvent == null)
            BulletDestroyedEvent = new BulletDestroyedEvent();

        distanceTimer = GetComponent<Timer>();
        distanceTimer.TimerElapsedEvent.AddListener(OnTimerElapsed);
    }

    public void Shoot(string tag, Vector3 position, Vector2 velocity, float lifetime)
    {
        CorporealForm.tag = tag;

        CorporealForm.transform.position = position;

        this.lifetime = lifetime;

        RB.velocity = velocity;
        distanceTimer.StartTimer(this.lifetime);
    }

    private void OnTimerElapsed()
    {
        DestroyBullet();
    }

    protected override void OnCollision(Collider2D collision)
    {
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        BulletDestroyedEvent.Invoke(this);
        Terminate();
    }

    public override void Terminate()
    {
        tag = Tags.BULLET;
        distanceTimer.ResetTimer();
        distanceTimer.TimerElapsedEvent.RemoveListener(OnTimerElapsed);
    }
}
