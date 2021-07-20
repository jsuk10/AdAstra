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
            GameManager.Instance.SetDirctionary("RocketLanding", this.gameObject);
            CameraController.Instance.ChangeCameraTarget(this.transform);
            animator.Play("RockLanding");
        } else{
            GameManager.Instance.SetDirctionary("Rocket", this.gameObject);
        }
        if (animator == null)
            GetComponent<Animator>();
    }

    /// <summary>
    /// ?? ?? ??
    /// </summary>
    public void RocketFire() {
        CameraController.Instance.ShakeCamera(3f, 1f);
        animator.Play("RocketFire");
    }

    /// <summary>
    /// ??? ?? ??? ?? ??
    /// </summary>
    public void AfterFire() {
        Debug.Log("After Fire");
    }

    public void AfterLanding() {
        Debug.Log("RockLanding");
        CameraController.Instance.ShakeCamera(3f,1f);
    }
}
