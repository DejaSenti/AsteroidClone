using UnityEngine;

public abstract class SpaceForm : MonoBehaviour
{
    public Collider2D Collider;
    public SpaceEntity Entity;

    public ParticleSystem ParticleSystem;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (Entity.isActiveAndEnabled)
        {
            ParticleSystem.transform.position = transform.position;
            ParticleSystem.Play();

            Entity.OnCollision(collision);
        }
    }
}