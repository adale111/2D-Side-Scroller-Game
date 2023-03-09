using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public GameObject background;
    public GameObject midground;
    public GameObject foreground;

    public float backgroundSpeed = -1f;
    public float midgroundSpeed = -2.5f;
    public float foregroundSpeed = -2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       

        if (background.transform.position.x < -8)
        {
            
            GameObject newBackground = Instantiate(background, new Vector3(28f, 1, 0), Quaternion.identity);

            if (background.transform.position.x < -30)
            {
                Destroy(background);
            }
            background = newBackground;
        }


       if (midground.transform.position.x < -4)
        {
            GameObject newMidground = Instantiate(midground, new Vector3(23.5f, -0.5f, 0), Quaternion.identity);

            if (midground.transform.position.x < -30)
            {
                Destroy(midground);
            }
            midground = newMidground;
        }

       
       if (foreground.transform.position.x < 0)
        {
            GameObject newForeground = Instantiate(foreground, new Vector3(25.2f, -2.5f, 0), Quaternion.identity);

            if (foreground.transform.position.x < -40)
            {
                Destroy(foreground);
            }
            foreground = newForeground;
        }

    
    }
}
