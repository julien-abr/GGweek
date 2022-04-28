using System.Collections;
using UnityEngine;

public class bearTrap : MonoBehaviour
{
    public PlayerMovement playerMovement;
    [SerializeField] float slowDuration = 1f;
    [SerializeField] float slowAmount = 11f;
    private bool isTrapping = false;

    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "Player" && !isTrapping)
        {
            playerMovement.TakeSlow(slowAmount, slowDuration);
        }
    }


}
