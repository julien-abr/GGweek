using UnityEngine;

public class DarkTrail : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;

    void Update()
    {
        Vector2 vector = new Vector2(1, 0); //on créé un vecteur avec une position x > 0 pour que la caméra avance vers l'avant
        transform.Translate(vector * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
