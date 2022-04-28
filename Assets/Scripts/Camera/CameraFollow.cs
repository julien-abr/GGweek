using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //On déclare nos variables
    [SerializeField] GameObject player;
    [SerializeField] float timeOffset;
    [SerializeField] Vector3 posOffset;

    private Vector3 velocity;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);   //on fait en sorte que la caméra suive le joueur de facon smooth
    }
}
