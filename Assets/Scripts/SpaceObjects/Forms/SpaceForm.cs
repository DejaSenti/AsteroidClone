using UnityEngine;

public abstract class SpaceForm : MonoBehaviour
{
    public Collider2D Collider;
    public SpaceEntity Entity;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (Entity.isActiveAndEnabled)
        {
            Entity.OnCollision(collision);
        }
    }
}