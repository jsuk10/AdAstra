using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isLanding = false;
    private void Start()
    {
        if (isLanding)
        {
            CameraController.Instance.ChangeCameraTarget(this.transform);
            animator.Play("RockLanding");
        }
        if (animator == null)
            GetComponent<Animator>();
    }

    /// <summary>
    /// ?? ?? ??
    /// </summary>
    public void RocketFire() {
        CameraController.Instance.ShakeCamera(3f, 1f);
        animator.Play("Fire");
    }

    /// <summary>
    /// ??? ?? ??? ?? ??
    /// </summary>
    public void AfterFire() {
        Debug.Log("After Fire");
    }

    public void AfterLanding() {
        Debug.Log("Landing");
        CameraController.Instance.ShakeCamera(3f,1f);
    }
}
