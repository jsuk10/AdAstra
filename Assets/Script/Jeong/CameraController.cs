using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System;


public class CameraController : UnitySingleton<CameraController>
{
    [SerializeField] private Animator ani;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

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

    public override void OnCreated()
    {
    }


    public override void OnInitiate()
    {
    }
}
