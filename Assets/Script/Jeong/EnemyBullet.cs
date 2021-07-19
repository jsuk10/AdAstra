using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float distoryTime;
    [SerializeField] private float speed;
    private Coroutine translate;

    void Start()
    {
        Invoke("Destroy", distoryTime);
        translate = StartCoroutine(Translate());
    }


    void Destroy() {
        StopCoroutine(translate);
        Destroy(this.gameObject);
    }

    virtual protected IEnumerator Translate()
    {
        while (true)
        {
            this.transform.Translate(-Vector3.left * speed * Time.deltaTime);
            yield return null;

        }
    }
}
