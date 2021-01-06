using UnityEngine;

public abstract class SpaceForm : MonoBehaviour
{
    private const int NUM_EMITTED_PARTICLES = 25;

    public Collider2D Collider;
    public SpaceEntity Entity;

    public ParticleSystem ParticleSystem;
    private ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (Entity.isActiveAndEnabled)
        {
            emitParams.position = transform.position;
            ParticleSystem.Emit(emitParams, NUM_EMITTED_PARTICLES);
            ParticleSystem.Play();

            Entity.OnCollision(collision);
        }
    }
}