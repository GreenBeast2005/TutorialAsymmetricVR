using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MindMapConnection : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

    }

    // Maintains the rendered lines position so that the connection will move with the nodes as you move it around.
    void Update()
    {
        if (pointA != null && pointB != null)
        {
            lineRenderer.SetPosition(0, pointA.position);
            lineRenderer.SetPosition(1, pointB.position);
        }
    }
}
