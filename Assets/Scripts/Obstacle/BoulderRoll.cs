using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRoll : MonoBehaviour
{

    // Il faut 1 "CIRCLE COLLIDER 2D" pour mettre autour du rocher ET un "TRIGGER" en forme de rectangle à placer là ou le joueur passera
    [SerializeField] float slowDuration = 2f;
    [SerializeField] float slowAmount = 3f;
    public float speed = 1f;    // Vitesse du rocher : Valeur positive pour aller a droite, valeur négatives pour aller à gauche
    private Rigidbody2D rb;     // Le rigidbody du boulet
    private bool go;            // Booléen qui servira à déclencher 
    public PlayerMovement playerMovement;

    // ATTENTION c'est une FixedUpdate (donc pas une Update). C'est ici qu'on va faire "rouler" le boulet si "GO" est vrai
    void FixedUpdate()
    {
        if (go)
        {
            rb.AddTorque(-speed);
        }
    }

    // Si le joueur entre dans notre trigger, GO devient vrai, et on ajouter un Rigidbody2D à notre boulet pour qu'il puisse utiliser le moteur physique.
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player" && !go)
        {
            go = true;
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    void OnCollisionEnter2D(Collision2D truc)
    {
        if (truc.transform.CompareTag("Player"))
        {
            playerMovement.TakeSlow(slowAmount, slowDuration); ;
        }
    }
}