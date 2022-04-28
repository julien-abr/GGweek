using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerHealth.TakeDamage(damage);
    }
}
