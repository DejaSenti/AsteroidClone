using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SpaceBoundary : MonoBehaviour
{
    public RectTransform RectTransform;

    public float Width;
    public float Height;

    private void Awake()
    {
        Width = RectTransform.rect.width * RectTransform.lossyScale.z;
        Height = RectTransform.rect.height * RectTransform.lossyScale.z;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 origin = RectTransform.position;

        Gizmos.DrawLine(origin + new Vector3(-Width, -Height, 0) / 2, origin + new Vector3(-Width, Height, 0) / 2);
        Gizmos.DrawLine(origin + new Vector3(-Width, Height, 0) / 2, origin + new Vector3(Width, Height, 0) / 2);
        Gizmos.DrawLine(origin + new Vector3(Width, Height, 0) / 2, origin + new Vector3(Width, -Height, 0) / 2);
        Gizmos.DrawLine(origin + new Vector3(Width, -Height, 0) / 2, origin + new Vector3(-Width, -Height, 0) / 2);
    }
}
