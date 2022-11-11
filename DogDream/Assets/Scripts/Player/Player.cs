using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalSpeed;
    public float jumpVelocity;
    public float turnSpeed;

    public bool isGrounded = false;

    public Transform groundCheck;
    float groundRad = 0.2f;
    public LayerMask groundLayer;

    public Rigidbody2D rb;
    private BoxCollider2D boxColl;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRad, groundLayer);
    }

    private void FixedUpdate()
    {
        DetectJump();
        PlayerSpeed();
        RotatePlayer();
    }

    void DetectJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.velocity = Vector2.up * jumpVelocity;
        }
    }

    public void RotatePlayer()
    {
        if (!isGrounded && Input.GetButton("Jump"))
        {
            transform.Rotate(0, 0, turnSpeed, Space.Self);
            if (Input.GetButtonUp("Jump"))
            {
                transform.Rotate(0, 0, -turnSpeed, Space.Self);
            }
        }
    }
    //В этом методе позже будет реализовываться ускорение
    private void PlayerSpeed()
    {
        rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
    }
}
