using ExtensionMethods;
using UnityEngine;
using UnityEngine.U2D;

public class AsteroidViewController : MonoBehaviour
{
    private const float FULL_CIRCLE_RAD = 2 * Mathf.PI;
    private const float MIN_RADIUS = 0.7f;
    private const float MAX_RADIUS = 1;
    private const int MIN_EDGES = 7;
    private const int MAX_EDGES = 15;

#pragma warning disable 0649
    [SerializeField]
    private SpriteShapeController[] spriteShapeControllers = new SpriteShapeController[9];
#pragma warning restore 0649

    public void UpdateView()
    {
        int numEdges = Mathf.CeilToInt(this.GetRandomInRange(MIN_EDGES, MAX_EDGES));

        Vector3[] edges = GenerateEdges(numEdges);

        foreach (SpriteShapeController spriteShapeController in spriteShapeControllers)
        {
            spriteShapeController.spline.Clear();

            for (int i = 0; i < edges.Length; i++)
            {
                spriteShapeController.spline.InsertPointAt(i, edges[i]);
            }
        }
    }

    private Vector3[] GenerateEdges(int numEdges)
    {
        float[] radii = this.GetRandomArrayInRange(MIN_RADIUS, MAX_RADIUS, numEdges);

        float maxAngle = FULL_CIRCLE_RAD / numEdges;
        float[] angles = this.GetRandomArrayInRange(0, maxAngle, numEdges);

        Vector3[] result = new Vector3[numEdges];
        for (int i = 0; i < numEdges; i++)
        {
            result[i] = radii[i] * new Vector3(Mathf.Cos(angles[i] + maxAngle * i), Mathf.Sin(angles[i] + maxAngle * i), 0);
        }

        return result;
    }
}
