using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalSpeed;
    public float jumpVelocity;
    public float forwRotation;
    public float backwRotation;


    public bool isGrounded = false;
    public bool move = false;
    bool groundHit = false;

    public Transform groundCheck;
    float groundRad = 0.1f;
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

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isGrounded = false;
                rb.velocity = Vector2.up * jumpVelocity;
            }
            
        }

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            move = true;
        }
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
        {
            move = false;
        }
    }

    public void RotatePlayer()
    {
        if (move)
        {
            if (!isGrounded)
            {
                rb.AddTorque(backwRotation * Time.deltaTime, ForceMode2D.Force);
            }
            else
            {
                //rb.AddForce(transform.up * jumpVelocity * Time.deltaTime * 100f, ForceMode2D.Force);
            }
        }
        else
        {
            if (!isGrounded)
            {
                rb.AddTorque(forwRotation * -1 * Time.deltaTime, ForceMode2D.Force);
            }
        }
    }
    //В этом методе позже будет реализовываться ускорение
    private void PlayerSpeed()
    {
        rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boost")
        {
            rb.AddForce(transform.right * boostSpeed, ForceMode2D.Impulse);
        }
        if (collision.tag == "Ground")
        {
            groundHit = true;
            loseState.transform.position = gameObject.transform.position;
            Instantiate(loseState);
            Destroy(gameObject);
            cam.SetActive(false);
            looseUI.SetActive(true);
        }
        if (collision.tag == "Rock")
        {
            groundHit = true;
            loseState.transform.position = gameObject.transform.position;
            Instantiate(loseState);
            Destroy(gameObject);
            cam.SetActive(false);
            looseUI.SetActive(true);
        }
    }*/
}
