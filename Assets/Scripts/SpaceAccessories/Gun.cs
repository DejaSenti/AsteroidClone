using UnityEngine;

public class Gun : MonoBehaviour
{
    private const string BULLET_TAG = "Bullet";

    public SpaceObject Owner;

    public float Strength;
    public float CooldownPeriod;
    public float BulletLifetime;

    public bool IsCoolingDown { get => cooldownTimer.enabled; }

    private ObjectPool<Bullet> bulletPool;

    private Timer cooldownTimer;

    private void Awake()
    {
        cooldownTimer = GetComponent<Timer>();

        bulletPool = new ObjectPool<Bullet>();

        int poolSize = Mathf.CeilToInt(BulletLifetime / CooldownPeriod);

        bulletPool.Initialize(poolSize, transform);
    }

    public void Fire(Vector2 direction)
    {
        if (IsCoolingDown)
            return;

        direction = direction.normalized;

        var bullet = bulletPool.Spawn();

        if (bullet == null)
            return;

        bullet.tag = BULLET_TAG + Owner.tag;

        bullet.transform.position = transform.position;

        bullet.Lifetime = BulletLifetime;

        var bulletVelocity = Owner.RB.velocity + direction * Strength;
        bullet.Shoot(bulletVelocity);

        cooldownTimer.StartTimer(CooldownPeriod);
    }
}