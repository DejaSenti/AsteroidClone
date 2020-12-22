using UnityEngine;
namespace ExtensionMethods
{
    public static class MathExtensions
    {
        public static Vector2 RotationToVector2(this MonoBehaviour monoBehaviour, float angle)
        {
            float angleRad = Mathf.Deg2Rad * angle;
            Vector2 result = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            return result;
        }
    }
}
