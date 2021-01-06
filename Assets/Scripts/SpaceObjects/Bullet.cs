using UnityEngine;

[RequireComponent(typeof(Timer))]
public class Bullet : SpaceEntity
{
    public BulletDestroyedEvent BulletDestroyedEvent;

    private float lifetime;

    public Timer DistanceTimer;

    protected override void Awake()
    {
        base.Awake();

        if (BulletDestroyedEvent == null)
            BulletDestroyedEvent = new BulletDestroyedEvent();
    }

    public void Shoot(string tag, Vector3 position, Vector2 velocity, float lifetime)
    {
        CorporealForm.tag = tag;

        CorporealForm.transform.position = position;

        this.lifetime = lifetime;

        RB.velocity = velocity;

        DistanceTimer.StartTimer(this.lifetime);
        DistanceTimer.TimerElapsedEvent.AddListener(OnTimerElapsed);
    }

    private void OnTimerElapsed()
    {
        DestroyBullet();
    }

    public override void OnCollision(Collider2D collision)
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
        DistanceTimer.ResetTimer();
        DistanceTimer.TimerElapsedEvent.RemoveListener(OnTimerElapsed);
        BulletDestroyedEvent.RemoveAllListeners();
    }
}
