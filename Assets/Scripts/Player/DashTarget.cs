using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTarget : MonoBehaviour
{
    bool isLookingForward = true;
    void ChangeSide(bool isForward)
    {
        if (isForward && !isLookingForward)
        {
            gameObject.transform.position = new Vector2((gameObject.transform.position.x + 10.6f), gameObject.transform.position.y);
            isLookingForward = true;
        }
        else if (!isForward && isLookingForward)
        {
            gameObject.transform.position = new Vector2((gameObject.transform.position.x - 10.6f), gameObject.transform.position.y);
            isLookingForward = false;
        }
    }
}
