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
    float groundRad = 0.5f;
    public LayerMask groundLayer;
    public Vector3 movement;
    public Rigidbody2D rb;
    public CapsuleCollider2D boxColl;

    private float preRotZ = 0f;
    private float sumRotZ = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRad, groundLayer);
        if (controlling)
        {
            DetectJump();
            RotatePlayer();
        }

        if (!isGrounded)
        {
            ChechBackFliip();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else rb.constraints = RigidbodyConstraints2D.None;
    }

    private void ChechBackFliip()
    {
            float rotZ = transform.eulerAngles.z;
            Debug.Log(sumRotZ);
            if (rotZ > preRotZ)
            {
                sumRotZ += rotZ - preRotZ;
            }
            else sumRotZ = 0;

            preRotZ = rotZ;

            if (sumRotZ > 250)
            {
                StartCoroutine(SpeedBoost(5));
            }
    }

    private IEnumerator SpeedBoost(float time)
    {

        horizontalSpeed = 6;
        yield return new WaitForSeconds(time);
        horizontalSpeed = 5;
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

        if (collision.gameObject.tag == "Obstacle")
        {
            fucked = true;
            controlling = false;

            if (!deathScreen.activeSelf)
            {
                deathScreen.SetActive(true);
            }
        }

        //StartCoroutine(SpeedBoost(10));
    }

    private void PlayerSpeed()
    {
        movement = new Vector3(horizontalSpeed, rb.velocity.y);
        if (!fucked) rb.velocity = movement;
    }

    void DetectJump()
    {
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
    }
}
