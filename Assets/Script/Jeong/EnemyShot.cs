using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [Tooltip("생성할 총알 넣기")]
    [SerializeField]
    private GameObject bulletPrefabs;

    [Tooltip("발사시 파티클")]
    [SerializeField]
    private ParticleSystem[] shotParticle = new ParticleSystem[2];

    [Tooltip("총알 발사 주기")]
    [SerializeField]
    private float builletInterver;

    [SerializeField]
    private CapsuleCollider2D capsuleCollider;

    private Vector3 bulletPosition;


    private void Start()
    {
        if (capsuleCollider == null)
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        bulletPosition = new Vector3((float)capsuleCollider.size.x + 0.5f, (float)capsuleCollider.size.y/2, 0f);
            
        //for (int i = 0; i < shotParticle.Length; i++)
        //    shotParticle[i].Stop();
        InvokeRepeating("CreateBullet", builletInterver, builletInterver);
    }

    private void CreateBullet()
    {
        //for (int i = 0; i < shotParticle.Length; i++)
            //shotParticle[i].Play();

        //Instantiate(bulletPrefabs, shotParticle[0].gameObject.transform.position, transform.rotation);
        Instantiate(bulletPrefabs, this.transform.position + bulletPosition, transform.rotation);
    }
}