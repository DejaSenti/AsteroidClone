using UnityEngine;
using ExtensionMethods;

public class PlayerShip : MonoBehaviour
{
    public float Acceleration;
    public float AngularAcceleration;

    public Rigidbody2D RB;
    public Collider2D Collider;

    public Vector2 Direction { get => this.RotationToVector2(transform.rotation.eulerAngles.z); }
}
