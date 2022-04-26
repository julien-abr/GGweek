using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// J'ai tout mis dans ce script pour le moment, je m'occuperais de découper dans des scripts différents lorsque j'aurais établi les mécaniques générales :)
//
public class PlayerMovement : MonoBehaviour
{
    public DashState dashState;
    private float jump = 7.75f;
    //private string playerMode = "default";
    private bool _canJump = true;
    private float dashTimer;
    private float dashLength = 20f;

    public Vector2 savedVelocity;


    SpriteRenderer sr;
    Rigidbody2D rb;

    private float _speedMultiplier = 11f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("velocity = " + rb.velocity);
            Debug.Log("position = " + rb.position);
        }

        switch (dashState)
        {
            case DashState.Ready:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    savedVelocity = rb.velocity;
                    rb.velocity = new Vector2(rb.velocity.x * 3f, rb.velocity.y);
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                {
                    dashTimer = Time.deltaTime * 10;
                    if (dashTimer >= dashLength)
                    {
                        dashTimer = dashLength;
                        rb.velocity = savedVelocity;
                        dashState = DashState.Cooldown;
                    }
                }
                break;
            case DashState.Cooldown:
                {
                    dashTimer -= Time.deltaTime;
                    if (dashTimer <= 0)
                    {
                        dashTimer = 0;
                        dashState = DashState.Ready;
                    }
                }
                break;
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        x *= _speedMultiplier;
        if ((Input.GetAxis("Jump") > 0) && (_canJump))
        {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            _canJump = false;
        }
        rb.velocity = new Vector2(x, rb.velocity.y);
    }

    private void TurnToBird(int formTime)
    {
        sr.color = Color.cyan;
        jump = 11f  ;
        StartCoroutine(BackToHuman(formTime));
    }

    private void TurnToBoar(int formTime)
    {
        sr.color = Color.black;
        StartCoroutine(BackToHuman(formTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            _canJump = true;
            Debug.Log("replenished jump");
            _speedMultiplier = 11f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            _canJump = false;
            _speedMultiplier = 7f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BirdBonus"))
        {
            TurnToBird(5);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("BoarBonus"))
        {
            TurnToBoar(5);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator BackToHuman(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        sr.color = Color.white;
        jump = 7.75f;
    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
}