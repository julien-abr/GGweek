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

    [Header("For Dashing")]
    IEnumerator dashCoroutine;
    private bool isDashing;
    private bool canDash = true;
    private float direction = 1;
    private float normalGravity;
    [SerializeField] GameObject dashEffect;

    [Header("For Sound")]

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D monCollider;

    // Premi�re fonction qui aura lieu qu'une seule fois, quand on lancera le jeu
    void Start()
    {
        // On commence par r�cup�rer les composants (ceux qu'on a d�clar� au dessus)
        rb = gameObject.GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;
        monCollider = gameObject.GetComponent<Collider2D>();
        anim = gameObject.GetComponent<Animator>();

        rb.freezeRotation = true;

        maxSpeed = moveSpeed;
    }


    // Fonction principale qui aura lieu en permanence, tout au long du jeu
    void Update()
    {
        jumpCheck();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        if (Input.GetButtonDown("Jump") && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        // On test si le player coure et si c'est le cas on lance l'animation de course
        if (Input.GetAxisRaw("Horizontal") != 0 && isGrounded)
            anim.SetBool("isRunning", true);
      

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash == true)
        {
            if (dashCoroutine != null) //si il y a plusieurs coroutines de dash en même temps
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(.1f, 5);
            StartCoroutine(dashCoroutine);
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            direction = Input.GetAxisRaw("Horizontal");
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && isFacingRight)
        {
            Flip();
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) //lorsque le joueur dash
        {
            anim.SetTrigger("dash");
            rb.AddForce(new Vector2(direction * 10, 0), ForceMode2D.Impulse);
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }
    }


    void jumpCheck() //pour savoir si le joueur peut sauter
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
        isFacingRight = !isFacingRight; //on inverse la direction dans laquelle le joueur regarde
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmos() //pour les tracés dans l'editor
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        Vector2 originalVelocity = rb.velocity;
        isDashing = true;
        canDash = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.gravityScale = normalGravity;
        rb.velocity = originalVelocity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }

    public void TakeSlow(float amount, float duration)
    {
        moveSpeed -= amount;
        StartCoroutine(SlowDuration(duration));
    }
    IEnumerator SlowDuration(float dur)
    {
        yield return new WaitForSeconds(dur);
    }
}
