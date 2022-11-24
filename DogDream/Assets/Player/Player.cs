using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float horizontalSpeed;
    public float jumpVelocity;
    public float forwRotation;
    public float backwRotation;
    public float distance = 0f;

    public bool fucked = false;
    public bool isGrounded = false;
    public bool move = false;
    public bool controlling = true;
    public GameObject deathScreen;
    //bool groundHit = false;

    public Transform groundCheck;
    float groundRad = 1f;
    public LayerMask groundLayer;
    public Vector3 movement;
    public Rigidbody2D rb;
    public BoxCollider2D boxColl;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRad, groundLayer);
        if (controlling)
        {
            DetectJump();
            RotatePlayer();
        }
    }

    private void FixedUpdate()
    {
        PlayerSpeed();
        distance += rb.velocity.x * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded && ((transform.eulerAngles.z >= 90.0f && transform.eulerAngles.z <= 270.0f) || (transform.eulerAngles.z >= 270.0f && transform.eulerAngles.z <= 90.0f))) 
        { 
            fucked = true;
            controlling = false;

            if (!deathScreen.activeSelf)
            {
                deathScreen.SetActive(true);

            }
        }

        if (fucked == false)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            fucked = true;
            controlling = false;

            if (!deathScreen.activeSelf)
            {
                deathScreen.SetActive(true);

            }
        }
    }

    //В этом методе позже будет реализовываться ускорение
    private void PlayerSpeed()
    {
        movement = new Vector3(horizontalSpeed, rb.velocity.y, rb.transform.eulerAngles.z);
        if (!fucked) rb.velocity = movement;
    }

    void DetectJump()
    {

        /*if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isGrounded = false;
                rb.velocity = Vector2.up * jumpVelocity;
            }
            
        }*/
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                }
            }
        }
        /*if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
        {
            move = false;
        }*/
    }

    public void RotatePlayer()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetButton("Fire1") || Input.GetButton("Jump"))
            {
                if (!isGrounded)
                {
                    transform.Rotate(Vector3.back * backwRotation * Time.deltaTime * (-1));
                }
            }

            if (!isGrounded)
            {
                transform.Rotate(Vector3.forward * forwRotation * Time.deltaTime * (-1));
            }
        }
        /*if (move)
        {
            if (!isGrounded)
            {
                transform.Rotate(0, 0, backwRotation / 1000, Space.Self);
                //rb.AddTorque(backwRotation * Time.deltaTime, ForceMode2D.Force);
            }
            else
            {
                isGrounded = false;
                rb.velocity = Vector2.up * jumpVelocity;
                //rb.AddForce(transform.up * jumpVelocity * Time.deltaTime * 100f, ForceMode2D.Force);
            }
        }
        else
        {
            if (!isGrounded)
            {
                transform.Rotate(0, 0, forwRotation / -1000, Space.Self);
                //rb.AddTorque(forwRotation * -1 * Time.deltaTime, ForceMode2D.Force);
            }
        }*/
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
