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
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            //Moving
            animator.SetBool("IsMoving", false);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            //Left Flip
            animator.SetInteger("Direction", -1);
            animator.SetBool("IsMoving", true);
        }else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            //Right Flip
            animator.SetInteger("Direction", 1);
            animator.SetBool("IsMoving", true);
        }

        if (Input.GetButtonDown("Jump") && !animator.GetBool("IsJumping"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
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
        }else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
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

    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("Attach : " + other.gameObject.layer);

        if(other.gameObject.layer == 8 && rigid.velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
       // Debug.Log("Detach : " + other.gameObject.layer);
    }

}
