using UnityEngine;
namespace ExtensionMethods
{
    public static class MathExtensions
    {
        public const int FULL_CIRCLE_DEG = 360;

        public static Vector2 RotationToVector2(this MonoBehaviour monoBehaviour, float angle)
        {
            return RotationToVector2(angle);
        }

        public static float Vector2ToRotation(this MonoBehaviour monoBehaviour, Vector2 vector2)
        {
            return Vector2ToRotation(vector2);
        }

        public static Vector2 GetRandomDirection(this MonoBehaviour monoBehaviour)
        {
            float rotation = GetRandomAngle();
            Vector2 result = RotationToVector2(null, rotation);
            return result;
        }

        public static float GetRandomInRange(this MonoBehaviour monoBehaviour, float min, float max)
        {
            return GetRandomInRange(min, max);
        }

        public static float GetRandomAngle(this MonoBehaviour monoBehaviour)
        {
            return GetRandomAngle();
        }

        public static Vector2 RotateVectorByDeg(this MonoBehaviour monoBehaviour, Vector2 vector2, float angle)
        {
            return RotateVectorByDeg(vector2, angle);
        }

        private static float GetRandomAngle()
        {
            float result = GetRandomInRange(null, 0, FULL_CIRCLE_DEG);
            return result;
        }

        private static Vector2 RotateVectorByDeg(Vector2 vector2, float angle)
        {
            angle *= Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(angle);
            float sinAngle = Mathf.Sin(angle);

            Vector2 result = new Vector2(vector2.x * cosAngle - vector2.y * sinAngle, vector2.x * sinAngle + vector2.y * cosAngle);
            
            return result;
        }

        private static Vector2 RotationToVector2(float angle)
        {
            float angleRad = Mathf.Deg2Rad * angle;
            Vector2 result = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            return result;
        }

        private static float Vector2ToRotation(Vector2 vector2)
        {
            float result = Mathf.Atan2(vector2.y, vector2.x);
            return result;
        }

        private static float GetRandomInRange(float min, float max)
        {
            float result = Random.Range(min, max);
            return result;
        }
    }
}
