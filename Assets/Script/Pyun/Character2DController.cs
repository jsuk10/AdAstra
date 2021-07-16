using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    public float maxSpeed = 1;
    public float jumpForce = 1;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        _rigidbody.AddForce(Vector2.right*h, ForceMode2D.Impulse);

        //Max Speed
        if(_rigidbody.velocity.x > maxSpeed)
            _rigidbody.velocity = new Vector2(maxSpeed, _rigidbody.velocity.y);
        else if(_rigidbody.velocity.x < maxSpeed*(-1))
            _rigidbody.velocity = new Vector2(maxSpeed*(-1), _rigidbody.velocity.y);


        //Stop Speed
        if(Input.GetButtonUp("Horizontal")){
            _rigidbody.velocity = new Vector2(0.5f*_rigidbody.velocity.normalized.x, _rigidbody.velocity.y);
        }

        // if(Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        // {
        //     _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        // }
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        //Landing platform
        if (_rigidbody.velocity.y < 0)
            {
            Debug.DrawRay(_rigidbody.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(_rigidbody.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if(rayHit.collider != null)
            {
                if(rayHit.distance < 0.5f)
                    anim.SetBool("isJumping", false);
            }

        }


        //Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //Animation
        if (Mathf.Abs(_rigidbody.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }
}
