using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Shoots : MonoBehaviour
{
    public float horizontalSpeed;
    public float fireRate = 1.5f; // time between shots in seconds
    public GameObject enemybulletPrefab;
    public Transform enemybulletSpawn;
    private float timeSinceLastShot;
    private GameObject player;

    private Animator chargeShot;
    public GameObject pupil;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastShot = 0f;

        player = GameObject.Find("Player");

        chargeShot = pupil.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()

    {
        if (/*GetComponent<Renderer>().isVisible*/ transform.position.x < 15)
        {
            timeSinceLastShot += Time.deltaTime;

            if (timeSinceLastShot >= fireRate)
            {
                chargeShot.Play("Pupil Shoot Animation");

                StartCoroutine(WaitAndShoot());

                timeSinceLastShot = 0f; // reset the timer
            }
        }

        IEnumerator WaitAndShoot()
        {
            yield return new WaitForSeconds(1.5f);


            Vector3 enemybulletSpawnPosition = enemybulletSpawn.position;
            var enemyBullet = (GameObject)Instantiate(enemybulletPrefab, enemybulletSpawnPosition, enemybulletSpawn.rotation);

            Vector3 direction = player.transform.position - enemyBullet.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            enemyBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            enemyBullet.GetComponent<Rigidbody2D>().velocity = enemyBullet.transform.right * 6;


            Destroy(enemyBullet, 6.0f);
        }
    }
}

