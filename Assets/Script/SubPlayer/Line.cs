using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Line : MonoBehaviourPun
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float massAdjustmentValue;
    public float totalDistance;
    private List<Vector2> points = new List<Vector2>();
    private int pointsCount = 0;
    public int PointsCount { get => this.pointsCount; }
    private bool isShake = false;

    private float pointsMinDistance = 0.1f;
    private float circleColliderRadius;

    public PhotonView photonView;

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
    [PunRPC]
    public void AddPoint(Vector2 newPoint)
    {
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
        if (pointsCount > 1)
        {
            edgeCollider2D.points = points.ToArray();
        }
    }

    /// <summary>
    /// 마지막 포인트를 얻어오는 것입니
    /// </summary>
    [PunRPC]
    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);

    }
    [PunRPC]
    public void SetMass()
    {
        rigidbody2D.mass = totalDistance / massAdjustmentValue;
    }
    [PunRPC]
    public void UsePhysice(bool usePhysics)
    {
        rigidbody2D.isKinematic = !usePhysics;
    }
    [PunRPC]
    public void SetLineColor(float r, float g, float b)
    {
        Color color = new Color(r, g, b);
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
    [PunRPC]
    public void SetPointsDistance(float distance)
    {
        pointsMinDistance = distance;
    }
    [PunRPC]
    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        circleColliderRadius = width / 2f;
        edgeCollider2D.edgeRadius = width / 2f;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Endter{other.gameObject.layer}");
        if (other.gameObject.layer == 6 && isShake == false)
        {
            CameraController.Instance.ShakeCamera(rigidbody2D.mass * rigidbody2D.velocity.y, 1f);
            isShake = true;
        }
        if (other.gameObject.layer == 8)
            Destroy(this.gameObject);
    }
}
