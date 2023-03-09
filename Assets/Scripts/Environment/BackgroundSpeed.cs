using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpeed : MonoBehaviour
{
   

    public float backgroundSpeed = -1f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(backgroundSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }

    }
}
