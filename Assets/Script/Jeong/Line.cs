using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float massAdjustmentValue;
    public float totalDistance;
    private List<Vector2> points = new List<Vector2>();
    private int pointsCount = 0;
    public int PointsCount { get => this.pointsCount; }

    private float pointsMinDistance = 0.1f;
    private float circleColliderRadius;

    /// <summary>
    /// 각 객체가 없을 경우 할당해준다.
    /// </summary>
    private void Awake()
    {
        if (lineRenderer == null) GetComponent<LineRenderer>();
        if (edgeCollider2D == null) GetComponent<EdgeCollider2D>();
        if (rigidbody2D == null) GetComponent<Rigidbody2D>();
    }

    

    /// <summary>
    /// 포인트를 더하는 함수입니다.
    /// </summary>
    /// <param name="newPoint">새로운 포인트</param>
    public void AddPoint(Vector2 newPoint) {
        float distance = 0;
        if (pointsCount >= 1)
        {
            distance = Vector2.Distance(newPoint, GetLastPoint());
            if (distance < pointsMinDistance)
                return;
        }
        totalDistance += distance;

        //else
        points.Add(newPoint);
        pointsCount++;

        //Add Circle Collider to the Point
        CircleCollider2D circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;

        //lineRenderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);

        //Edge Collider
        if (pointsCount > 1) {
            edgeCollider2D.points = points.ToArray();
        }
    }

    /// <summary>
    /// 마지막 포인트를 얻어오는 것입니
    /// </summary>
    public Vector2 GetLastPoint() {
        return (Vector2) lineRenderer.GetPosition(pointsCount - 1);

    }

    public void SetMass() {
        rigidbody2D.mass = totalDistance / massAdjustmentValue;
    }

    public void UsePhysice(bool usePhysics) {
        rigidbody2D.isKinematic = !usePhysics;
    }

    public void SetLineColor(Gradient colorGradient) {
        lineRenderer.colorGradient = colorGradient;
    }
    public void SetPointsDistance(float distance)
    {
        pointsMinDistance = distance;
    }
    public void SetLineWidth(float width) {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        circleColliderRadius = width / 2f;
        edgeCollider2D.edgeRadius = width / 2f;
    }

}
