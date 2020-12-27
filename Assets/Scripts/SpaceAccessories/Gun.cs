using UnityEngine;

public class Gun : MonoBehaviour
{
    private const string BULLET_TAG = "Bullet";

    public SpaceObject Owner;

    public float Strength;
    public float KickBackStrength;
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

        bullet.transform.localPosition = Vector3.zero;

        bullet.Lifetime = BulletLifetime;

        var bulletVelocity = direction * Strength;

        if (Mathf.Abs(Vector2.SignedAngle(Owner.Direction, direction)) < 90)
        {
            bulletVelocity += Owner.RB.velocity;
        }

        bullet.Shoot(bulletVelocity);

        var kickBackForce = -direction * KickBackStrength;
        Owner.RB.AddForce(kickBackForce);

        cooldownTimer.StartTimer(CooldownPeriod);

        bullet.BulletDestroyedEvent.AddListener(OnBulletDestroyedEvent);
    }

    private void OnBulletDestroyedEvent(Bullet bullet)
    {
        bulletPool.Kill(bullet);

        bullet.BulletDestroyedEvent.RemoveListener(OnBulletDestroyedEvent);
    }
}