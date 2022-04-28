using UnityEngine;

public class Bullet : MonoBehaviour
{
    //On déclare nos variables
    [SerializeField] float speed = 20f;
    public Rigidbody2D rb;
    //[SerializeField] GameObject hitWallEffect;
    //[SerializeField] GameObject effect;

    [SerializeField] float damage = 1f;
    [SerializeField] float lifeTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed; //On fait avancer le projectile
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    ///  Lorsque la balle rencontre un ennemi, on lui envoit des dégâts et on joue un effet. Si ce n'est pas un ennemi on joue un effet différent
    /// </summary>
    /// <param name="truc"></param>
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player")
        {
            truc.SendMessage("TakeDamage", damage);
            //Instantiate(effect, truc.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        else if (!truc.isTrigger && truc.tag != "Player")
        {
            //Instantiate(hitWallEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("Effect");
        }
    }
}