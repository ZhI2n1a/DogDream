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
    public float bones = 0f;
    public float highScore = 0f;

    public bool fucked = false;
    public bool isGrounded = false;
    public bool move = false;
    public bool controlling = true;
    //bool groundHit = false;

    public Transform groundCheck;
    float groundRad = 0.5f;
    public LayerMask groundLayer;
    public Vector3 movement;
    public Rigidbody2D rb;
    public CapsuleCollider2D boxColl;

    private float preRotZ = 0f;
    private float sumRotZ = 0f;

    public AudioSource GroundSound;
    public AudioSource JumpSound;
    public AudioSource LandingSoound;
    public AudioSource BoneSound;

    public bool BonusSofaMode = false;
    public GameObject BonusSofaCurrent;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
        }

        if (PlayerPrefs.HasKey("BoneCount"))
        {
            bones = PlayerPrefs.GetFloat("BoneCount");
        }
    }

    void Start()
    {
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        PlayerSpeed();
        distance += rb.velocity.x * Time.fixedDeltaTime;

        if(distance > highScore)
        {
            highScore= distance;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
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
            rb.gravityScale = 2f;
            ChechBackFliip();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            LandingSoound.Play();
        }
        else
        {
            rb.gravityScale = 2.5f;
            rb.constraints = RigidbodyConstraints2D.None;
        }

        if (fucked)
        {
            LevelManager.instance.GameOver();
        }

        if (!isGrounded || fucked)
        {
            GroundSound.Play();
        }
    }

    private void ChechBackFliip()
    {
        if (Input.GetButton("Fire1") || Input.GetButton("Jump"))
        {
            if (transform.rotation.z > 0)
            {
                float rotZ = transform.eulerAngles.z;
                if (rotZ > preRotZ)
                {
                    sumRotZ += rotZ - preRotZ;
                }
                else sumRotZ = 0;

                preRotZ = rotZ;

                if (sumRotZ > 160)
                {
                    bones += 1;
                    //StartCoroutine(SpeedBoost(10));
                }
            }
        }
    }

    //private IEnumerator SpeedBoost(float time)
    //{
    //    horizontalSpeed = 8;
    //    yield return new WaitForSeconds(time);
    //    horizontalSpeed = 7;
    //}

    private IEnumerator BonusSofaTime(float time)
    {
        BonusSofaMode = true;
        horizontalSpeed = 9;
        jumpVelocity = 7;
        forwRotation = 50;
        backwRotation = 250;
        yield return new WaitForSeconds(time);
        Destroy(BonusSofaCurrent);
        BonusSofaMode = false;
        horizontalSpeed = 7;
        jumpVelocity = 10;
        forwRotation = 100;
        backwRotation = 500;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded && ((transform.eulerAngles.z >= 90.0f && transform.eulerAngles.z <= 300.0f) || (transform.eulerAngles.z >= 300.0f && transform.eulerAngles.z <= 90.0f))) 
        { 
            fucked = true;
            controlling = false;
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            if (BonusSofaMode == true)
            {
                fucked = false;
                Destroy(collision.gameObject);
            }
            else
            {
                fucked = true;
                controlling = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boner"))
        {
            bones++;
            collision.gameObject.SetActive(false);
            PlayerPrefs.SetFloat("BoneCount", bones);
            BoneSound.Play();
        }

        if (collision.gameObject.tag == "BonusSofa")
        {
            StartCoroutine(BonusSofaTime(10));
            BonusSofaCurrent = collision.gameObject;
            transform.eulerAngles = new Vector3(0, 0, 0);
            collision.gameObject.transform.SetParent(gameObject.transform);
            collision.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
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
                    JumpSound.Play();
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

    public bool GetFucked()
    {
        return fucked;
    }
}
