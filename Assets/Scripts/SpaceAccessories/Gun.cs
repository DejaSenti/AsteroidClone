using UnityEngine;

[RequireComponent(typeof(Timer))]
public class Gun : MonoBehaviour
{
    private const string BULLET_TAG = "Bullet";

    public SpaceEntity Owner;

    public float Strength;
    public float KickBackStrength;
    public float CooldownPeriod;
    public float BulletLifetime;

    public bool IsCoolingDown { get => CooldownTimer.enabled; }

    private ObjectPool<Bullet> bulletPool;

    public Timer CooldownTimer;

    private void Awake()
    {
        if (bulletPool == null)
        {
            bulletPool = new ObjectPool<Bullet>();
        }

        int poolSize = Mathf.CeilToInt(BulletLifetime / CooldownPeriod);

        bulletPool.Initialize(poolSize);
    }

    public void Fire(Vector2 direction)
    {
        if (IsCoolingDown)
            return;

        direction = direction.normalized;

        var bullet = bulletPool.Acquire();

        if (bullet == null)
            return;

        var tag = BULLET_TAG + Owner.tag;

        var position = transform.position;

        var lifetime = BulletLifetime;

        var bulletVelocity = direction * Strength;

        if (Mathf.Abs(Vector2.SignedAngle(Owner.Direction, direction)) < 90)
        {
            bulletVelocity += Owner.RB.velocity;
        }

        bullet.Shoot(tag, position, bulletVelocity, lifetime);

        var kickBackForce = -direction * KickBackStrength;
        Owner.RB.AddForce(kickBackForce);

        CooldownTimer.StartTimer(CooldownPeriod);

        bullet.BulletDestroyedEvent.AddListener(OnBulletDestroyedEvent);
    }

    private void OnBulletDestroyedEvent(Bullet bullet)
    {
        bulletPool.Release(bullet);

        CooldownTimer.ResetTimer();

        bullet.BulletDestroyedEvent.RemoveListener(OnBulletDestroyedEvent);
    }

    public void Terminate()
    {
        CooldownTimer.ResetTimer();

        var bullets = bulletPool.GetAllPooledObjects();
        foreach(Bullet bullet in bullets)
        {
            bullet.Terminate();
            bulletPool.Release(bullet);
            bullet.BulletDestroyedEvent.RemoveAllListeners();
        }

        bulletPool.Terminate();
    }
}