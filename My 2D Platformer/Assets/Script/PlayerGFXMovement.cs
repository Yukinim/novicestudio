using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFXMovement : MonoBehaviour {

    public float movePower = 100f;
    public float jumpPower = 100f;

    Rigidbody2D rigid;

    Vector3 movement;
    bool isJumping = false;

    Animator animator;

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            animator.SetTrigger("doJumping");
        }
	}

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(-1, 1, 1);
        }else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if (!isJumping) return;

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
}
