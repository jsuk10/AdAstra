using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System;
using System.Collections;

public class CameraController : UnitySingleton<CameraController>
{
    [SerializeField] private Animator ani;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float correctionValue = 3f;

    private void Start()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    [ContextMenu("Tracking Player")]
    public void ChangeCameraPlayer()
    {
        ani.Play("TrackingPlayer");
    }

    [ContextMenu("Drowing")]
    public void ChangeCameraDrowing()
    {
        ani.Play("Drowing");

    }


    /// <summary>
    /// 가상 카메라의 타겟을 변경해주는 함수
    /// </summary>
    /// <param name="target"></param>
    public void ChangeCameraTarget(GameObject target) {
        ChangeCameraTarget(target.GetComponent<Transform>());
    }

    /// <summary>
    /// 가상 카메라의 타겟을 변경해주는 함수
    /// </summary>
    /// <param name="target"></param>
    public void ChangeCameraTarget(Transform target)
    {
        virtualCamera.Follow = target;
    }

    /// <summary>
    /// 카메라 흔드는 함수
    /// </summary>
    /// <param name="intensity"></param>
    /// <param name="time"></param>
    public void ShakeCamera(float intensity, float time)
    {
        StartCoroutine(IShakeCamera(intensity/ correctionValue, time));
        
    }

    /// <summary>
    /// 몇초 흔들릴지 세주는 코루틴
    /// </summary>
    /// <param name="intensity"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator IShakeCamera(float intensity, float time) {
        float timeTemp = 0;
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        while (time > timeTemp) {
            Debug.Log("IsShake");
            timeTemp += 1.0f;
            yield return new WaitForSeconds(1.0f);
        }
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        yield return null;
    }

    public override void OnCreated() { }


    public override void OnInitiate() { }
}
