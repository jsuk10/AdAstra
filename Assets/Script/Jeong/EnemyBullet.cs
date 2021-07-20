using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float distoryTime;
    [SerializeField] private float speed;
    [SerializeField] private bool isCanon;
    private Coroutine translate;

    void Start()
    {
        Invoke("Destroy", distoryTime);
        translate = StartCoroutine(Translate());
    }


    void Destroy()
    {
        StopCoroutine(translate);
        if (this.gameObject != null)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCanon)
        {
            if (collision.gameObject.tag == "Line")
            {
                PhotonNetwork.Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Player")
                return;
        }
        Destroy();
    }
}
