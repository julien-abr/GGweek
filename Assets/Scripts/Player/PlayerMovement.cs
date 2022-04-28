using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Maintenant on déclare nos variables
    [Header("For Variables")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    private float maxSpeed;

    [Header("For GroundCheck")]
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isRunning;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask collisionLayers;

    [Header("For Sound")]

    private Rigidbody2D rb;
    private SpriteRenderer skin;
    private Animator anim;
    private Collider2D monCollider;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        skin = gameObject.GetComponent<SpriteRenderer>();
        monCollider = gameObject.GetComponent<Collider2D>();
        anim = gameObject.GetComponent<Animator>();

        rb.freezeRotation = true;

        maxSpeed = moveSpeed;
    }


    void Update()
    {
        jumpCheck();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        if (Input.GetButtonDown("Jump") && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && isGrounded)
        {
            anim.SetBool("isRunning", true);
        }
        else
            anim.SetBool("isRunning", false);

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") < 0 && isFacingRight)
        {
            Flip();
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    void jumpCheck()
    {
        if (isGrounded == true)
        {
            isJumping = true;
            anim.SetBool("jump", false);
        }
        else
        {
            isJumping = false;
            anim.SetBool("jump", true);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
