using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMove : MonoBehaviour
{
    [Tooltip("이동 하는 좌표들")]
    [SerializeField]
    private GameObject[] wayPoints;
    [Tooltip("1초당 얼마나 가는가 거리/s")]
    [SerializeField]
    [Range(1, 10)]
    protected float speed = 2f;
    [Tooltip("미는 힘" )]
    [SerializeField]
    private float force = 1;


    bool isTurn = true;
    Vector3 dirction;

    private Rigidbody2D rb;

    private int targetIndex = 1;
    private int tempStartIndex = 0;

    protected Vector3 targetDirction;
    protected Vector3 startPosition;
    protected float targetDistance;
    protected float time = 0;

    /// <summary>
    /// 첫번째 위치로 이동시켜주고이동 시작
    /// </summary>
    private void Start()
    {
        if (wayPoints.Length > 0)
            this.transform.position = wayPoints[tempStartIndex].gameObject.transform.position;
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        dirction = new Vector3(1, 0, 0);
        StartCoroutine(Translate());
    }

    /// <summary>
    /// 첫번째 위치로 이동시켜줌
    /// </summary>
    private void OnEnable()
    {
        if (wayPoints.Length > 0)
            this.transform.position = wayPoints[tempStartIndex].gameObject.transform.position;
    }

    /// <summary>
    /// 이동 시켜주는 함수
    /// 물리 출돌 계산 안하고 죽으면 없애는걸로 함.
    /// </summary>
    /// <returns></returns>
    virtual protected IEnumerator Translate()
    {

        dirction = new Vector3(1,0,0);

        while (true)
        {
            if (this.transform.position.x > wayPoints[1].transform.position.x && isTurn)
            {
                FlipSize();
            }
            else if(this.transform.position.x < wayPoints[0].transform.position.x && !isTurn)
            {
                FlipSize();
            }

            rb.velocity = (dirction * force);
            yield return null;

        }
    }

    /// <summary>
    /// 뒤집기 해줌.
    /// </summary>
    public void FlipSize()
    {
        isTurn = !isTurn;
        dirction = -dirction;
        Vector3 Scale = this.transform.localScale;
        Scale.x = -Scale.x;
        this.transform.localScale = Scale;
    }

    
}
