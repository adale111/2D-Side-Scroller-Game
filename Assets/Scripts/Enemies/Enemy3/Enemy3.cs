using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float horizontalSpeed;
    public float fireRate = 2.5f; // time between shots in seconds
    public GameObject enemybulletPrefab;
    public Transform enemybulletSpawn;
    private float timeSinceLastShot;

    public float spreadAngle = 40;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastShot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(horizontalSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x < 6)
        {
            horizontalSpeed = 0;
        }

        if (horizontalSpeed == 0)
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= fireRate)
            {

                for (int i = 0; i < 3; i++)
                {


                    Vector3 enemybulletSpawnPosition = enemybulletSpawn.position;
                    //enemybulletSpawnPosition.x -= 0.5f; // shift the bullet spawn position slightly to the left

                    var enemyBullet = (GameObject)Instantiate(enemybulletPrefab, enemybulletSpawnPosition, enemybulletSpawn.rotation);

                    enemyBullet.transform.Rotate(0, 0, spreadAngle * (i - 1));
                    Vector2 direction = Quaternion.Euler(0, 0, enemyBullet.transform.eulerAngles.z) * Vector2.right;
                    enemyBullet.GetComponent<Rigidbody2D>().velocity = -direction * 6;


                    Destroy(enemyBullet, 6.0f);

                    timeSinceLastShot = 0f; // reset the timer
                }
            }
        }
    }
}