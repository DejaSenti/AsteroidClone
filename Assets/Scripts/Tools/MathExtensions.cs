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

        public static Vector2 GetRandomDirection(this MonoBehaviour monoBehaviour)
        {
            float rotation = GetRandomInRange(monoBehaviour, 0, 360);
            Vector2 result = RotationToVector2(monoBehaviour, rotation);
            return result;
        }

        public static float GetRandomInRange(this MonoBehaviour monoBehaviour, float min, float max)
        {
            float result = Random.Range(min, max);
            return result;
        }
    }
}
