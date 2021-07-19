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

    [Tooltip("움직임 자연스럽게 하는 커브")]
    [SerializeField]
    protected AnimationCurve wayPointCurve;

    [Tooltip("이동중에 멈추는지 않는지")]
    [SerializeField]
    private bool isStop = false;

    private float requiredTime;
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

        time = 0;
        CalculationRequiredTime();
        FlipSize();

        yield return new WaitForSeconds(Random.Range(0f, 1f));
        while (true)
        {
            if (isStop && 0 == Random.Range(0, 100)) {
                yield return new WaitForSeconds(0.5f);
            }
            time += Time.deltaTime;
            if (time > requiredTime)
            {
                time = 0;
                targetIndex++;
                tempStartIndex = targetIndex - 1;
                if (targetIndex >= wayPoints.Length)
                {
                    targetIndex = 0;
                    tempStartIndex = wayPoints.Length - 1;
                }
                CalculationRequiredTime();
                FlipSize();
            }

            Vector3 moveVector = this.transform.position;
            moveVector.x = Vector3.Lerp(startPosition, wayPoints[targetIndex].transform.position, wayPointCurve.Evaluate(time / requiredTime)).x;

            this.transform.position = moveVector;
            yield return null;

        }
    }

    /// <summary>
    /// 다음 웨이포인트까지 이동시간 연산함.
    /// </summary>
    void CalculationRequiredTime()
    {
        startPosition = this.gameObject.transform.position;
        CalculationDistance(wayPoints[targetIndex].transform.position);
        requiredTime = targetDistance / speed;

    }

    /// <summary>
    /// 다음 웨이포인트까지 거리 연산함.
    /// </summary>
    /// <param name="targetPosition"></param>
    public void CalculationDistance(Vector3 targetPosition)
    {
        targetDirction = (targetPosition - this.gameObject.transform.position).normalized;
        targetDistance = Vector3.Magnitude(this.gameObject.transform.position - targetPosition);
    }

    /// <summary>
    /// 뒤집기 해줌.
    /// </summary>
    public void FlipSize()
    {
        Vector3 Scale = this.transform.localScale;
        Scale.x = -Scale.x;
        this.transform.localScale = Scale;
    }

    
}
