using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilMovement : MonoBehaviour
{
    private GameObject player;
    public Transform Enemy2;
    public float maxDistance = 0.09f;


    void Start()

    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        Vector3 newPosition = transform.position + (distance > maxDistance ? maxDistance * direction.normalized : direction);
        newPosition = new Vector3(
            Mathf.Clamp(newPosition.x, Enemy2.position.x - maxDistance, Enemy2.position.x + maxDistance),
            Mathf.Clamp(newPosition.y, Enemy2.position.y - maxDistance, Enemy2.position.y + maxDistance),
            transform.position.z
        );

        transform.position = newPosition;
    }
}

