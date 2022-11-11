using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBetaPlayer : MonoBehaviour
{
    public bool move = false;
    public GameObject sprites;
    public Rigidbody2D rb;
    public float speed = 20f;
    public bool isGrounded = true;
    public float forwRotation = 1.5f;
    public float backwrdRotation = -1f;
    public bool groundHit = false;
    public float jumpForce = 10f;
    public GameObject normal;
    public GameObject JumpCrouch;
    public GameObject JumpStanding;
    public float boostSpeed;
    public GameObject loseState;
    public GameObject cam;
    public GameObject looseUI;

    void Start()
    {
        normal.SetActive(true);
        JumpCrouch.SetActive(false);
        JumpStanding.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            move = true;
        }
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
        {
            move = false;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            normal.SetActive(false);
            JumpCrouch.SetActive(true);
            JumpStanding.SetActive(false);
        }
        if (!groundHit)
        {
            rb.AddForce(transform.right * speed * Time.deltaTime * 100f, ForceMode2D.Force);
        }

        if (move)
        {
            if (!isGrounded)
            {
                rb.AddTorque(forwRotation * forwRotation, ForceMode2D.Force);
                normal.SetActive(false);
                JumpCrouch.SetActive(true);
                JumpStanding.SetActive(false);
            }
            else
            {
                rb.AddForce(transform.up * jumpForce * Time.deltaTime * 100f, ForceMode2D.Force);
            }
        }
        else
        {
            if (!isGrounded)
            {
                normal.SetActive(false);
                JumpCrouch.SetActive(false);
                JumpStanding.SetActive(true);
            }
        }

        if (!move)
        {
            if (!isGrounded)
            {
                rb.AddTorque(backwrdRotation * 1 * Time.deltaTime * 100f, ForceMode2D.Force);
            }
        }
    }

    public void OnCollisionEnter2D()
    {
        isGrounded = true;
        normal.SetActive(true);
        JumpCrouch.SetActive(false);
        JumpStanding.SetActive(false);
    }

    public void OnCollisionExit2D()
    {
        isGrounded = false;
    }

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
    }
}
