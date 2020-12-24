using UnityEngine;
namespace ExtensionMethods
{
    public static class MathExtensions
    {
        private const int FULL_CIRCLE_DEG = 360;
        private const float DISTANCE_SAFETY_MOD = 1.5f;

        public static Vector2 RotationToVector2(this MonoBehaviour monoBehaviour, float angle)
        {
            float angleRad = Mathf.Deg2Rad * angle;
            Vector2 result = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            return result;
        }

        public static Vector2 GetRandomDirection(this MonoBehaviour monoBehaviour)
        {
            float rotation = GetRandomAngle();
            Vector2 result = RotationToVector2(null, rotation);
            return result;
        }

        public static float GetRandomInRange(this MonoBehaviour monoBehaviour, float min, float max)
        {
            float result = Random.Range(min, max);
            return result;
        }

        public static Vector2[] GetPositionArrayAroundLocation(this MonoBehaviour monoBehaviour, int amount, float objectRadius, Vector2 location)
        {
            if (amount == 0)
                return null;

            Vector2[] result = new Vector2[amount];

            if (amount == 1)
            {
                result[0] = location;
                return result;
            }

            float sliceAngle = FULL_CIRCLE_DEG / amount;
            float initialAngle = GetRandomAngle();

            float distance = objectRadius / Mathf.Sin(Mathf.Deg2Rad * sliceAngle / 2) * DISTANCE_SAFETY_MOD;

            for (int i = 0; i < amount; i++)
            {
                Vector2 position = location + distance * RotationToVector2(null, initialAngle + i * sliceAngle);
                result[i] = position;
            }

            return result;
        }

        public static Vector2 GetDirectionInRangeRelatedToPosition(this MonoBehaviour monoBehaviour, int amount, Vector2 centerPosition, Vector2 objectPosition)
        {
            float angleRange = FULL_CIRCLE_DEG / amount;

            Vector2 generalDirection = (objectPosition - centerPosition).normalized;

            float angle = GetRandomInRange(null, -angleRange / 2, angleRange / 2);

            Vector2 result = RotateVectorByDeg(generalDirection, angle);

            return result;
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

        public static Vector2 Sign(this Vector2 vector2)
        {
            Vector2 result = new Vector2(Mathf.Sign(vector2.x), Mathf.Sign(vector2.y));
            return result;
        }
    }
}
