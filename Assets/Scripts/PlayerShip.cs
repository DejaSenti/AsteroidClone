using UnityEngine;
using ExtensionMethods;
using System;

public class PlayerShip : SpaceObject
{
    public string ObjectTag { get => gameObject.tag; }
    public float Acceleration;
    public float AngularAcceleration;

    public Vector2 Direction { get => this.RotationToVector2(transform.rotation.eulerAngles.z); }

    public Gun Gun;
}
