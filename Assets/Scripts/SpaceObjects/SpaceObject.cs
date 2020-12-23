using UnityEngine;

public abstract class SpaceObject : MonoBehaviour
{
    public Rigidbody2D RB;

    public abstract void OnTriggerEnter2D(Collider2D collision);
}