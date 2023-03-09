using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundSpeed : MonoBehaviour
{


    public float foregroundSpeed = -2.5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(foregroundSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }

    }
}
