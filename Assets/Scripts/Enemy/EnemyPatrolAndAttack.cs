using UnityEngine;

public class EnemyFollowAndAttack : MonoBehaviour
{
    //On déclare nos variables
    [SerializeField] float speed;
    [SerializeField] float AttackBox;
    [SerializeField] float shootingRange;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float nextFireTime;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletParent;

    [SerializeField] PlayerHealth playerhealth;

    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    public bool isDead;
    private bool facingLeft = true; //bool pour savoir ou regarde l'ennemi
    [SerializeField] GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationControl();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < AttackBox && !isDead && distanceFromPlayer > shootingRange) //si la distance entre l'ennemi et le joueur est okus petite que la zone d'attaque du joueur mais plus grande que la zone de shoot
        {                                                                                       //et si l'ennemi est en vie
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time && !isDead) //sinon si l'ennemi est a portée pour tirer et n'est pas mort et peut tirer
        {
            anim.SetTrigger("shooting");
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate; //on incrémente nextFireTime pour pas que l'ennemi tire directement
        }

        FlipTowardsPlayer();
    }

    /// <summary>
    ///  Si il y a une collision avec le joueur et qu'il n'est pas invincible, alors on lui envoit des dégâts, si il a un bouclier, alors on le détruit
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerhealth.TakeDamage(10);
        }
    }


    void AnimationControl()
    {
        anim.SetBool("isDead", isDead); //on regarde continuellement la valeur de isDead pour jouer une animation si besoin
    }

    void FlipTowardsPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;
        if (playerDirection > 0 && facingLeft)
        {
            Flip();
        }
        else if (playerDirection < 0 && !facingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft; //on inverse la direction ou regarde l'ennemi
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmosSelected() //pour voir les tracés des zones d'attaques
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackBox);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}