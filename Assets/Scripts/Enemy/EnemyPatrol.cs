using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //On déclare nos variables
    [SerializeField] float speed = 2f;
    [SerializeField, Range(0.1f, 50f)] private float limiteDroite = 1f;
    [SerializeField, Range(0.1f, 50f)] private float limiteGauche = 1f;
    [SerializeField] int damage;
    private Vector3 limiteDroitePosition;
    private Vector3 limiteGauchePosition;
    private Rigidbody2D rb;
    private float direction = 1f;

    //private bool canChangeRot = true;

    [SerializeField] PlayerHealth playerhealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        limiteDroitePosition = transform.position + new Vector3(limiteDroite, 0, 0);
        limiteGauchePosition = transform.position - new Vector3(limiteGauche, 0, 0);
    }


    void Update()
    {
        // Si l'ennemi se coince contre quelque chose (sa vitesse plus petite que 0.1 m/s) alors il se retourne
        if (Mathf.Abs(rb.velocity.x) < 0.1f)
        {
            direction = -direction;
        }

        //Si il dépasse sa limite Droite, il se retourne
        if (transform.position.x > limiteDroitePosition.x)
        {
            direction = -1f;
        }

        if (transform.position.x < limiteGauchePosition.x)
        {
            direction = 1f;
        }

        if (direction == 1f)
        {
        }

        if (direction == -1f)
        {
        }

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    /// <summary>
    ///  Si il y a une collision avec le joueur et qu'il n'est pas invincible, alors on lui envoit des dégâts, si il a un bouclier, alors on le détruit
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerhealth.TakeDamage(damage);
        }
    }

    void OnDrawGizmos() //tracés dans l'editor
    {
        if (!Application.IsPlaying(gameObject))
        {
            limiteDroitePosition = transform.position + new Vector3(limiteDroite, 0, 0);
            limiteGauchePosition = transform.position - new Vector3(limiteGauche, 0, 0);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(limiteDroitePosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawCube(limiteGauchePosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawLine(limiteDroitePosition, limiteGauchePosition);
    }
}
